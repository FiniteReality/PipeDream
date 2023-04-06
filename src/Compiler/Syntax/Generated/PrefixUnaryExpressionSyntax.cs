namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a prefix unary expression.
/// </summary>
public sealed partial record PrefixUnaryExpressionSyntax(
    SyntaxToken OperatorToken,
    ExpressionSyntax Operand,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxTriviaList LeadingTrivia,
    SyntaxTriviaList TrailingTrivia)
    : ExpressionSyntax(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.PreDecrementExpression or
            SyntaxKind.AddressOfExpression or
            SyntaxKind.PreIncrementExpression or
            SyntaxKind.UnaryMinusExpression or
            SyntaxKind.DereferenceExpression or
            SyntaxKind.BitwiseNotExpression or
            SyntaxKind.LogicalNotExpression
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
    /// <summary>
    /// Gets the <see cref="SyntaxToken" /> representing the kind of operator
    /// of the prefix unary expression.
    /// </summary>
    public SyntaxToken OperatorToken { get; init; } = OperatorToken;

    private ExpressionSyntax _operand = ValidateOperand(Operand, nameof(Operand));

    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> representing the operand of
    /// the prefix unary expression.
    /// </summary>
    public ExpressionSyntax Operand
    {
        get => _operand;
        init => _operand = ValidateOperand(value, nameof(Operand));
    }

    private static ExpressionSyntax ValidateOperand(ExpressionSyntax value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.PlusPlusToken or
            SyntaxKind.TildeToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.MinusToken or
            SyntaxKind.ExclamationToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
