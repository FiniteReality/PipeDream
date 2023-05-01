namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a ternary expression.
/// </summary>
public sealed partial record ConditionalExpressionSyntax(
    ExpressionSyntax Condition,
    SyntaxToken QuestionToken,
    ExpressionSyntax WhenTrue,
    SyntaxToken ColonToken,
    ExpressionSyntax WhenFalse,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: SyntaxKind.ConditionalExpression,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<ConditionalExpressionSyntax>
{
    static void IVisitable<ConditionalExpressionSyntax>.Accept<TVisitor>(ConditionalExpressionSyntax node, TVisitor visitor)
        => visitor.VisitConditionalExpressionSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> node representing the
    /// condition of the conditional expression.
    /// </summary>
    public ExpressionSyntax Condition { get; init; } = Condition;

    private SyntaxToken _questionToken = ValidateQuestionToken(QuestionToken, nameof(QuestionToken));

    /// <summary>
    /// Gets the <see cref="SyntaxToken" /> representing the question mark.
    /// </summary>
    public SyntaxToken QuestionToken
    {
        get => _questionToken;
        init => _questionToken = ValidateQuestionToken(value, nameof(QuestionToken));
    }

    private static SyntaxToken ValidateQuestionToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.QuestionToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> node representing the
    /// expression to be evaluated when the condition is true.
    /// </summary>
    public ExpressionSyntax WhenTrue { get; init; } = WhenTrue;

    private SyntaxToken _colonToken = ValidateColonToken(ColonToken, nameof(ColonToken));

    /// <summary>
    /// Gets the <see cref="SyntaxToken" /> representing the colon.
    /// </summary>
    public SyntaxToken ColonToken
    {
        get => _colonToken;
        init => _colonToken = ValidateColonToken(value, nameof(ColonToken));
    }

    private static SyntaxToken ValidateColonToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.ColonToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the <see cref="ExpressionSyntax" /> node representing the
    /// expression to be evaluated when the condition is false.
    /// </summary>
    public ExpressionSyntax WhenFalse { get; init; } = WhenFalse;
}
