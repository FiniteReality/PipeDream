namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing information about a path expression.
/// </summary>
public sealed partial record PathExpressionSyntax(
    SyntaxToken PathOperator,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<PathExpressionSyntax>
{
    static void IVisitable<PathExpressionSyntax>.Accept<TVisitor>(PathExpressionSyntax node, TVisitor visitor)
        => visitor.VisitPathExpressionSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.CurrentPathExpression or
            SyntaxKind.ParentPathExpression
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _pathOperator = ValidatePathOperator(PathOperator, nameof(PathOperator));

    /// <summary>
    /// Gets the token representing the operator of this path expression.
    /// </summary>
    public SyntaxToken PathOperator
    {
        get => _pathOperator;
        init => _pathOperator = ValidatePathOperator(value, nameof(PathOperator));
    }

    private static SyntaxToken ValidatePathOperator(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.DotDotToken or
            SyntaxKind.DotToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };
}
