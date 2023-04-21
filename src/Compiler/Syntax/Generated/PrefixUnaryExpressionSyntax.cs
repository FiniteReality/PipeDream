namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a prefix unary expression.
/// </summary>
public sealed partial record PrefixUnaryExpressionSyntax(
    SyntaxToken OperatorToken,
    ExpressionSyntax Operand,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.AddressOfExpression or
            SyntaxKind.BitwiseNotExpression or
            SyntaxKind.DereferenceExpression or
            SyntaxKind.LogicalNotExpression or
            SyntaxKind.PreDecrementExpression or
            SyntaxKind.PreIncrementExpression or
            SyntaxKind.UnaryMinusExpression
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _operatorToken = ValidateOperatorToken(OperatorToken, nameof(OperatorToken));

    /// <summary>
    /// Gets the <see cref="SyntaxToken" /> representing the kind of operator
    /// of the prefix unary expression.
    /// </summary>
    public SyntaxToken OperatorToken
    {
        get => _operatorToken;
        init => _operatorToken = ValidateOperatorToken(value, nameof(OperatorToken));
    }

    private static SyntaxToken ValidateOperatorToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.AmpersandToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.ExclamationToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.MinusToken or
            SyntaxKind.PlusPlusToken or
            SyntaxKind.TildeToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> representing the operand of
    /// the prefix unary expression.
    /// </summary>
    public ExpressionSyntax Operand { get; init; } = Operand;
}
