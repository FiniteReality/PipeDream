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
            SyntaxKind.EqualsExpression or
            SyntaxKind.DivideExpression or
            SyntaxKind.LessThanExpression or
            SyntaxKind.RightShiftExpression or
            SyntaxKind.BitwiseOrExpression or
            SyntaxKind.LogicalOrExpression or
            SyntaxKind.MultiplyExpression or
            SyntaxKind.FloatModuloExpression or
            SyntaxKind.EquivalentExpression or
            SyntaxKind.NotEqualsExpression or
            SyntaxKind.IntegerModuloExpression or
            SyntaxKind.GreaterThanOrEqualExpression or
            SyntaxKind.NotEquivalentExpression or
            SyntaxKind.InExpression or
            SyntaxKind.AddExpression or
            SyntaxKind.ExclusiveOrExpression or
            SyntaxKind.GreaterThanExpression or
            SyntaxKind.BitwiseAndExpression or
            SyntaxKind.LogicalAndExpression or
            SyntaxKind.LeftShiftExpression or
            SyntaxKind.LessThanOrEqualExpression or
            SyntaxKind.SubtractExpression or
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
            SyntaxKind.GreaterThanGreaterThanToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.SlashToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.BarToken or
            SyntaxKind.InKeyword or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.CaretToken or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.MinusToken or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.EqualsEqualsToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.PercentToken or
            SyntaxKind.PlusToken
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
