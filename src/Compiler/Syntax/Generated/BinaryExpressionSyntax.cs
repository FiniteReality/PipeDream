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
            SyntaxKind.IntegerModuloExpression or
            SyntaxKind.NotEquivalentExpression or
            SyntaxKind.RightShiftExpression or
            SyntaxKind.LessThanOrEqualExpression or
            SyntaxKind.EquivalentExpression or
            SyntaxKind.AddExpression or
            SyntaxKind.ExponentiationExpression or
            SyntaxKind.GreaterThanExpression or
            SyntaxKind.InExpression or
            SyntaxKind.LogicalAndExpression or
            SyntaxKind.EqualsExpression or
            SyntaxKind.BitwiseAndExpression or
            SyntaxKind.LessThanExpression or
            SyntaxKind.FloatModuloExpression or
            SyntaxKind.DivideExpression or
            SyntaxKind.NotEqualsExpression or
            SyntaxKind.ExclusiveOrExpression or
            SyntaxKind.MultiplyExpression or
            SyntaxKind.BitwiseOrExpression or
            SyntaxKind.SubtractExpression or
            SyntaxKind.GreaterThanOrEqualExpression or
            SyntaxKind.LogicalOrExpression or
            SyntaxKind.LeftShiftExpression
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
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.PlusToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.EqualsEqualsToken or
            SyntaxKind.SlashToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.CaretToken or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.BarToken or
            SyntaxKind.InKeyword or
            SyntaxKind.PercentToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.MinusToken or
            SyntaxKind.GreaterThanGreaterThanToken
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
