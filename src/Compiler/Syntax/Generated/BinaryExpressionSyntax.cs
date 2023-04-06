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
            SyntaxKind.GreaterThanOrEqualExpression or
            SyntaxKind.SubtractExpression or
            SyntaxKind.AddExpression or
            SyntaxKind.NotEqualsExpression or
            SyntaxKind.RightShiftExpression or
            SyntaxKind.GreaterThanExpression or
            SyntaxKind.InExpression or
            SyntaxKind.BitwiseOrExpression or
            SyntaxKind.EqualsExpression or
            SyntaxKind.LeftShiftExpression or
            SyntaxKind.LogicalAndExpression or
            SyntaxKind.IntegerModuloExpression or
            SyntaxKind.ExclusiveOrExpression or
            SyntaxKind.LessThanExpression or
            SyntaxKind.LessThanOrEqualExpression or
            SyntaxKind.FloatModuloExpression or
            SyntaxKind.NotEquivalentExpression or
            SyntaxKind.BitwiseAndExpression or
            SyntaxKind.LogicalOrExpression or
            SyntaxKind.MultiplyExpression or
            SyntaxKind.DivideExpression or
            SyntaxKind.EquivalentExpression or
            SyntaxKind.ExponentiationExpression
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
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.PlusToken or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.CaretToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.PercentToken or
            SyntaxKind.BarToken or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.GreaterThanGreaterThanToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.InKeyword or
            SyntaxKind.SlashToken or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.MinusToken or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.EqualsEqualsToken
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
