namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a binary expression.
/// </summary>
public abstract partial record BinaryExpressionSyntax(
    ExpressionSyntax Left,
    SyntaxToken OperatorToken,
    ExpressionSyntax Right,
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
            SyntaxKind.GreaterThanExpression or
            SyntaxKind.LessThanExpression or
            SyntaxKind.ExponentiationExpression or
            SyntaxKind.EqualsExpression or
            SyntaxKind.IntegerModuloExpression or
            SyntaxKind.InExpression or
            SyntaxKind.LessThanOrEqualExpression or
            SyntaxKind.BitwiseAndExpression or
            SyntaxKind.BitwiseOrExpression or
            SyntaxKind.FloatModuloExpression or
            SyntaxKind.ExclusiveOrExpression or
            SyntaxKind.DivideExpression or
            SyntaxKind.EquivalentExpression or
            SyntaxKind.MultiplyExpression or
            SyntaxKind.GreaterThanOrEqualExpression or
            SyntaxKind.LogicalAndExpression or
            SyntaxKind.AddExpression or
            SyntaxKind.SubtractExpression or
            SyntaxKind.RightShiftExpression or
            SyntaxKind.LogicalOrExpression or
            SyntaxKind.NotEquivalentExpression or
            SyntaxKind.LeftShiftExpression or
            SyntaxKind.NotEqualsExpression
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> node representing the
    /// expression on the left of the binary operator.
    /// </summary>
    public ExpressionSyntax Left { get; init; } = Left;

    private SyntaxToken _operatorToken = ValidateOperatorToken(OperatorToken, nameof(OperatorToken));

    /// <summary>
    /// Gets the <see cref="SyntaxToken" /> representing the kind of operator
    /// in the member access expression.
    /// </summary>
    public SyntaxToken OperatorToken
    {
        get => _operatorToken;
        init => _operatorToken = ValidateOperatorToken(value, nameof(OperatorToken));
    }

    private static SyntaxToken ValidateOperatorToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.CaretToken or
            SyntaxKind.DoubleBarToken or
            SyntaxKind.DoubleEqualsToken or
            SyntaxKind.MinusToken or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.PlusToken or
            SyntaxKind.BarToken or
            SyntaxKind.DoubleLessThanToken or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.InKeyword or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.DoubleGreaterThanToken or
            SyntaxKind.PercentToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.DoubleAmpersandToken or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.SlashToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> node representing the
    /// expression on the right of the binary operator.
    /// </summary>
    public ExpressionSyntax Right { get; init; } = Right;
}
