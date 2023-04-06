namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a binary expression.
/// </summary>
public sealed partial record BinaryExpressionSyntax(
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
            SyntaxKind.AddExpression or
            SyntaxKind.BitwiseAndExpression or
            SyntaxKind.BitwiseOrExpression or
            SyntaxKind.DivideExpression or
            SyntaxKind.EqualsExpression or
            SyntaxKind.EquivalentExpression or
            SyntaxKind.ExclusiveOrExpression or
            SyntaxKind.ExponentiationExpression or
            SyntaxKind.FloatModuloExpression or
            SyntaxKind.GreaterThanExpression or
            SyntaxKind.GreaterThanOrEqualExpression or
            SyntaxKind.InExpression or
            SyntaxKind.IntegerModuloExpression or
            SyntaxKind.LeftShiftExpression or
            SyntaxKind.LessThanExpression or
            SyntaxKind.LessThanOrEqualExpression or
            SyntaxKind.LogicalAndExpression or
            SyntaxKind.LogicalOrExpression or
            SyntaxKind.MultiplyExpression or
            SyntaxKind.NotEqualsExpression or
            SyntaxKind.NotEquivalentExpression or
            SyntaxKind.RightShiftExpression or
            SyntaxKind.SubtractExpression
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
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.BarToken or
            SyntaxKind.CaretToken or
            SyntaxKind.EqualsEqualsToken or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.GreaterThanGreaterThanToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.InKeyword or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.MinusToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.PercentToken or
            SyntaxKind.PlusToken or
            SyntaxKind.SlashToken or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.TildeExclamationToken
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
