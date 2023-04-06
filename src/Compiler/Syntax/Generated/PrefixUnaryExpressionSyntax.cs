namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a prefix unary expression.
/// </summary>
public abstract partial record PrefixUnaryExpressionSyntax(
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
            SyntaxKind.LogicalNotExpression or
            SyntaxKind.PreIncrementExpression or
            SyntaxKind.BitwiseNotExpression or
            SyntaxKind.AddressOfExpression or
            SyntaxKind.UnaryMinusExpression or
            SyntaxKind.PreDecrementExpression or
            SyntaxKind.DereferenceExpression
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
            SyntaxKind.MinusToken or
            SyntaxKind.ExclamationToken or
            SyntaxKind.TildeToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.AmpersandToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
