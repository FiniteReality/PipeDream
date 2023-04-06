namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a postfix unary expression.
/// </summary>
public sealed partial record PostfixUnaryExpressionSyntax(
    ExpressionSyntax Operand,
    SyntaxToken OperatorToken,
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
            SyntaxKind.PostIncrementExpression or
            SyntaxKind.PostDecrementExpression
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> representing the operand of
    /// the postfix unary expression.
    /// </summary>
    public ExpressionSyntax Operand { get; init; } = Operand;

    private SyntaxToken _operatorToken = ValidateOperatorToken(OperatorToken, nameof(OperatorToken));

    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> representing the operand of
    /// the postfix unary expression.
    /// </summary>
    public SyntaxToken OperatorToken
    {
        get => _operatorToken;
        init => _operatorToken = ValidateOperatorToken(value, nameof(OperatorToken));
    }

    private static SyntaxToken ValidateOperatorToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.PlusPlusToken or
            SyntaxKind.MinusMinusToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
