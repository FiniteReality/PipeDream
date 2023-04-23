using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
{
    private async ValueTask<ExpressionSyntax?> ParseExpressionAsync(
        CancellationToken cancellationToken)
    {
        var left =
            await ParsePrefixUnaryExpressionAsync(cancellationToken)
            ?? await ParseNameAndPostfixUnaryExpressionAsync(cancellationToken);

        // TODO: this should produce an error
        if (left == null)
            return null;

        // TODO: other expression types
        return await ParseAssignmentExpressionAsync(left, cancellationToken)
            ?? left;
    }

    private async ValueTask<ExpressionSyntax?> ParseNameAndPostfixUnaryExpressionAsync(
        CancellationToken cancellationToken)
    {
        var name =
            (NameSyntax?)await ParseQualifiedNameAsync(cancellationToken)
            ?? await ParseSimpleNameAsync(cancellationToken);

        if (name == null)
            return null;

        // TODO: postfix operators
        return await ParsePostfixUnaryExpressionAsync(name, cancellationToken);
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

        var operand = await ParseExpressionAsync(cancellationToken);
        if (operand == null)
        {
            ProduceDiagnostic(ParseError.ExpectedExpression);
            return null;
        }

        return new PostfixUnaryExpressionSyntax(
            OperatorToken: token,
            Operand: operand,
            Span: default,
            LeadingTrivia: default,
            TrailingTrivia: default,
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
            LeadingTrivia: default,
            TrailingTrivia: default,
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
