using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

internal sealed partial class Parser
{
    private async ValueTask<ExpressionSyntax?>
        ParseExpressionAsync(
            CancellationToken cancellationToken)
    {
        return await ParsePreprocessorExpressionAsync(cancellationToken)
            ?? await ParseExpressionCoreAsync(cancellationToken);
    }

    private async ValueTask<ExpressionSyntax?>
        ParseExpressionCoreAsync(CancellationToken cancellationToken)
    {
        var left = await ParsePrefixUnaryExpressionAsync(cancellationToken)
            ?? await ParseNameAndPostfixUnaryExpressionAsync(cancellationToken);

        // TODO: this should produce an error
        if (left == null)
            return null;

        // TODO: other expression types
        var result = await ParseAssignmentExpressionAsync(left, cancellationToken)
            ?? left;

        return result;
    }

    private async ValueTask<PreprocessorExpressionSyntax?>
        ParsePreprocessorExpressionAsync(
            CancellationToken cancellationToken)
    {
        var hash = await PeekAsync(SyntaxKind.HashToken, cancellationToken);
        if (hash == null)
            return null;

        var directives = await ParseDirectivesAsync(
            static (@this, token) => @this.ParseExpressionCoreAsync(token),
            cancellationToken);

        return new PreprocessorExpressionSyntax(
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: directives);
    }

    private async ValueTask<ExpressionSyntax?>
        ParseNameAndPostfixUnaryExpressionAsync(
            CancellationToken cancellationToken)
    {
        var term =
            await ParseNewExpressionAsync(cancellationToken)
            ?? await ParsePathExpressionAsync(cancellationToken)
            ?? await ParseNumericLiteralAsync(cancellationToken)
            ?? (ExpressionSyntax?)await ParseNameAsync(cancellationToken);

        if (term == null)
            return null;

        return (ExpressionSyntax?)await ParsePostfixUnaryExpressionAsync(term, cancellationToken)
            ?? term;
    }

    private async ValueTask<PrefixUnaryExpressionSyntax?>
        ParsePrefixUnaryExpressionAsync(
            CancellationToken cancellationToken)
    {
        var token = await PeekAsync(cancellationToken);
        if (token == null)
            return null;

        if (token.Kind is
            SyntaxKind.ExclamationToken or
            SyntaxKind.TildeToken or
            SyntaxKind.MinusToken or
            SyntaxKind.PlusPlusToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.AmpersandToken)
            _ = await AdvanceAsync(cancellationToken);
        else
            return null;

        var operand = await ParseExpressionAsync(cancellationToken);
        if (operand == null)
        {
            ProduceDiagnostic(ParseError.ExpectedExpression);
            return null;
        }

        return new PrefixUnaryExpressionSyntax(
            OperatorToken: token,
            Operand: operand,
            Span: default,
            LeadingTrivia: default,
            TrailingTrivia: default,
            Kind: token.Kind switch
            {
                SyntaxKind.ExclamationToken
                    => SyntaxKind.LogicalNotExpression,
                SyntaxKind.TildeToken
                    => SyntaxKind.BitwiseNotExpression,
                SyntaxKind.MinusToken
                    => SyntaxKind.UnaryMinusExpression,
                SyntaxKind.PlusPlusToken
                    => SyntaxKind.PreIncrementExpression,
                SyntaxKind.MinusMinusToken
                    => SyntaxKind.PreDecrementExpression,
                SyntaxKind.AsteriskToken
                    => SyntaxKind.DereferenceExpression,
                SyntaxKind.AmpersandToken
                    => SyntaxKind.AddressOfExpression,
                _ => UnreachableSyntaxKind(this)
            });

        [DoesNotReturn]
        static SyntaxKind UnreachableSyntaxKind(Parser @this)
        {
            Debug.Fail("Unreachable syntax kind");
            @this.ProduceDiagnostic(() => new(KnownDiagnostics.Unknown));
            return SyntaxKind.Unknown;
        }
    }

    private async ValueTask<NewExpressionSyntax?>
        ParseNewExpressionAsync(
            CancellationToken cancellationToken)
    {
        var @new = await PeekAsync(SyntaxKind.NewKeyword, cancellationToken);
        if (@new == null)
            return null;

        _ = await AdvanceAsync(cancellationToken);
        var name = await ParseNameAsync(cancellationToken);

        return new NewExpressionSyntax(
            NewKeyword: @new,
            Type: name,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }

    private async ValueTask<PathExpressionSyntax?>
        ParsePathExpressionAsync(
            CancellationToken cancellationToken)
    {
        var path = await PeekAsync(cancellationToken);
        if (path == null)
            return null;

        if (path.Kind is not
            SyntaxKind.DotToken or
            SyntaxKind.DotDotToken)
            return null;

        _ = await AdvanceAsync(cancellationToken);

        return new PathExpressionSyntax(
            PathOperator: path,
            Kind: path.Kind switch
            {
                SyntaxKind.DotToken => SyntaxKind.CurrentPathExpression,
                SyntaxKind.DotDotToken => SyntaxKind.ParentPathExpression,
                var x => Unreachable(this, x)
            },
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());

        [DoesNotReturn]
        static SyntaxKind Unreachable(Parser @this, SyntaxKind error)
        {
            @this.ProduceDiagnostic(() => new(KnownDiagnostics.Unknown));
            Debug.Fail($"Invalid syntax kind {error}");
            return SyntaxKind.Unknown;
        }
    }

    private async ValueTask<PostfixUnaryExpressionSyntax?>
        ParsePostfixUnaryExpressionAsync(
            ExpressionSyntax name,
            CancellationToken cancellationToken)
    {
        var token = await PeekAsync(cancellationToken);
        if (token == null)
            return null;

        if (token.Kind is
            SyntaxKind.PlusPlusToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.AmpersandToken)
            _ = await AdvanceAsync(cancellationToken);
        else
            return null;

        return new PostfixUnaryExpressionSyntax(
            OperatorToken: token,
            Operand: name,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new(),
            Kind: token.Kind switch
            {
                SyntaxKind.PlusPlusToken
                    => SyntaxKind.PostIncrementExpression,
                SyntaxKind.MinusMinusToken
                    => SyntaxKind.PostDecrementExpression,
                _ => UnreachableSyntaxKind(this)
            });

        [DoesNotReturn]
        static SyntaxKind UnreachableSyntaxKind(Parser @this)
        {
            Debug.Fail("Unreachable syntax kind");
            @this.ProduceDiagnostic(() => new(KnownDiagnostics.Unknown));
            return SyntaxKind.Unknown;
        }
    }

    private async ValueTask<AssignmentExpressionSyntax?>
        ParseAssignmentExpressionAsync(
            ExpressionSyntax left,
            CancellationToken cancellationToken)
    {
        var equalsToken = await PeekAsync(cancellationToken);
        if (equalsToken == null)
            return null;

        if (equalsToken.Kind is
            SyntaxKind.EqualsToken or
            SyntaxKind.PlusEqualsToken or
            SyntaxKind.MinusEqualsToken or
            SyntaxKind.AsteriskEqualsToken or
            SyntaxKind.SlashEqualsToken or
            SyntaxKind.PercentEqualsToken or
            SyntaxKind.PercentPercentEqualsToken or
            SyntaxKind.AmpersandEqualsToken or
            SyntaxKind.BarEqualsToken or
            SyntaxKind.CaretEqualsToken or
            SyntaxKind.LessThanLessThanEqualsToken or
            SyntaxKind.GreaterThanGreaterThanEqualsToken or
            SyntaxKind.ColonEqualsToken or
            SyntaxKind.AmpersandAmpersandEqualsToken or
            SyntaxKind.BarBarEqualsToken)
            _ = await AdvanceAsync(cancellationToken);
        else
            return null;

        var right = await ParseExpressionAsync(cancellationToken);
        if (right == null)
        {
            ProduceDiagnostic(ParseError.ExpectedExpression);
            return null;
        }

        return new AssignmentExpressionSyntax(
            Left: left,
            OperatorToken: equalsToken,
            Right: right,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new(),
            Kind: equalsToken.Kind switch
            {
                SyntaxKind.EqualsToken
                    => SyntaxKind.SimpleAssignmentExpression,
                SyntaxKind.PlusEqualsToken
                    => SyntaxKind.AddAssignmentExpression,
                SyntaxKind.MinusEqualsToken
                    => SyntaxKind.SubtractAssignmentExpression,
                SyntaxKind.AsteriskEqualsToken
                    => SyntaxKind.MultiplyAssignmentExpression,
                SyntaxKind.SlashEqualsToken
                    => SyntaxKind.DivideAssignmentExpression,
                SyntaxKind.PercentEqualsToken
                    => SyntaxKind.IntegerModuloAssignmentExpression,
                SyntaxKind.PercentPercentEqualsToken
                    => SyntaxKind.FloatModuloAssignmentExpression,
                SyntaxKind.AmpersandEqualsToken
                    => SyntaxKind.BitwiseAndAssignmentExpression,
                SyntaxKind.BarEqualsToken
                    => SyntaxKind.BitwiseOrAssignmentExpression,
                SyntaxKind.CaretEqualsToken
                    => SyntaxKind.ExclusiveOrAssignmentExpression,
                SyntaxKind.LessThanLessThanEqualsToken
                    => SyntaxKind.LeftShiftAssignmentExpression,
                SyntaxKind.GreaterThanGreaterThanEqualsToken
                    => SyntaxKind.RightShiftAssignmentExpression,
                SyntaxKind.ColonEqualsToken
                    => SyntaxKind.DestructureAssignmentExpression,
                SyntaxKind.AmpersandAmpersandEqualsToken
                    => SyntaxKind.LogicalAndAssignmentExpression,
                SyntaxKind.BarBarEqualsToken
                    => SyntaxKind.LogicalOrAssignmentExpression,
                _ => UnreachableSyntaxKind(this)
            });

        [DoesNotReturn]
        static SyntaxKind UnreachableSyntaxKind(Parser @this)
        {
            Debug.Fail("Unreachable syntax kind");
            @this.ProduceDiagnostic(() => new(KnownDiagnostics.Unknown));
            return SyntaxKind.Unknown;
        }
    }
}
