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
        var left = await ParsePrefixUnaryExpressionSyntaxAsync(cancellationToken)
            ?? await ParseTermAsync(cancellationToken);
    }

    private async ValueTask<PrefixUnaryExpressionSyntax?>
        ParsePrefixUnaryExpressionSyntaxAsync(
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

    private async ValueTask<AssignmentExpressionSyntax?>
        ParseAssignmentExpressionAsync(
            ExpressionSyntax left,
            CancellationToken cancellationToken)
    {
        var equalsToken = await PeekAsync(cancellationToken);
        if (equalsToken == null)
            return null;

        switch (equalsToken.Kind)
        {
            case SyntaxKind.EqualsToken:
            case SyntaxKind.PlusEqualsToken:
            case SyntaxKind.MinusEqualsToken:
            case SyntaxKind.AsteriskEqualsToken:
            case SyntaxKind.SlashEqualsToken:
            case SyntaxKind.PercentEqualsToken:
            case SyntaxKind.PercentPercentEqualsToken:
            case SyntaxKind.AmpersandEqualsToken:
            case SyntaxKind.BarEqualsToken:
            case SyntaxKind.CaretEqualsToken:
            case SyntaxKind.LessThanLessThanEqualsToken:
            case SyntaxKind.GreaterThanGreaterThanEqualsToken:
            case SyntaxKind.ColonEqualsToken:
            case SyntaxKind.AmpersandAmpersandEqualsToken:
            case SyntaxKind.BarBarEqualsToken:
                _ = await AdvanceAsync(cancellationToken);
                break;
            default:
                return null;
        }

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