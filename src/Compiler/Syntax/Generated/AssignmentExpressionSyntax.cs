namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing an assignment expression.
/// </summary>
public sealed partial record AssignmentExpressionSyntax(
    ExpressionSyntax Left,
    SyntaxToken OperatorToken,
    ExpressionSyntax Right,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<AssignmentExpressionSyntax>
{
    static void IVisitable<AssignmentExpressionSyntax>.Accept<TVisitor>(AssignmentExpressionSyntax node, TVisitor visitor)
        => visitor.VisitAssignmentExpressionSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.AddAssignmentExpression or
            SyntaxKind.BitwiseAndAssignmentExpression or
            SyntaxKind.BitwiseOrAssignmentExpression or
            SyntaxKind.DestructureAssignmentExpression or
            SyntaxKind.DivideAssignmentExpression or
            SyntaxKind.ExclusiveOrAssignmentExpression or
            SyntaxKind.FloatModuloAssignmentExpression or
            SyntaxKind.IntegerModuloAssignmentExpression or
            SyntaxKind.LeftShiftAssignmentExpression or
            SyntaxKind.LogicalAndAssignmentExpression or
            SyntaxKind.LogicalOrAssignmentExpression or
            SyntaxKind.MultiplyAssignmentExpression or
            SyntaxKind.RightShiftAssignmentExpression or
            SyntaxKind.SimpleAssignmentExpression or
            SyntaxKind.SubtractAssignmentExpression
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the expression on the left of the assignment operator.
    /// </summary>
    public ExpressionSyntax Left { get; init; } = Left;

    private SyntaxToken _operatorToken = ValidateOperatorToken(OperatorToken, nameof(OperatorToken));

    /// <summary>
    /// Gets the token representing the operator of the assignment expression.
    /// </summary>
    public SyntaxToken OperatorToken
    {
        get => _operatorToken;
        init => _operatorToken = ValidateOperatorToken(value, nameof(OperatorToken));
    }

    private static SyntaxToken ValidateOperatorToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.AmpersandAmpersandEqualsToken or
            SyntaxKind.AmpersandEqualsToken or
            SyntaxKind.AsteriskEqualsToken or
            SyntaxKind.BarBarEqualsToken or
            SyntaxKind.BarEqualsToken or
            SyntaxKind.CaretEqualsToken or
            SyntaxKind.ColonEqualsToken or
            SyntaxKind.EqualsToken or
            SyntaxKind.GreaterThanGreaterThanEqualsToken or
            SyntaxKind.LessThanLessThanEqualsToken or
            SyntaxKind.MinusEqualsToken or
            SyntaxKind.PercentEqualsToken or
            SyntaxKind.PercentPercentEqualsToken or
            SyntaxKind.PlusEqualsToken or
            SyntaxKind.SlashEqualsToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the expression on the right of the assignment operator.
    /// </summary>
    public ExpressionSyntax Right { get; init; } = Right;
}
