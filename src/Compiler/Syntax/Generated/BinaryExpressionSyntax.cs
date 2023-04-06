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
            SyntaxKind.LessThanOrEqualExpression or
            SyntaxKind.LogicalOrExpression or
            SyntaxKind.InExpression or
            SyntaxKind.LeftShiftExpression or
            SyntaxKind.LessThanExpression or
            SyntaxKind.DivideExpression or
            SyntaxKind.SubtractExpression or
            SyntaxKind.EqualsExpression or
            SyntaxKind.BitwiseOrExpression or
            SyntaxKind.RightShiftExpression or
            SyntaxKind.NotEqualsExpression or
            SyntaxKind.ExclusiveOrExpression or
            SyntaxKind.LogicalAndExpression or
            SyntaxKind.NotEquivalentExpression or
            SyntaxKind.IntegerModuloExpression or
            SyntaxKind.FloatModuloExpression or
            SyntaxKind.MultiplyExpression or
            SyntaxKind.BitwiseAndExpression or
            SyntaxKind.GreaterThanOrEqualExpression or
            SyntaxKind.ExponentiationExpression or
            SyntaxKind.GreaterThanExpression or
            SyntaxKind.AddExpression or
            SyntaxKind.EquivalentExpression
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
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.EqualsEqualsToken or
            SyntaxKind.GreaterThanGreaterThanToken or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.PlusToken or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.CaretToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.SlashToken or
            SyntaxKind.PercentToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.BarToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.MinusToken or
            SyntaxKind.InKeyword or
            SyntaxKind.LessThanGreaterThanToken
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
