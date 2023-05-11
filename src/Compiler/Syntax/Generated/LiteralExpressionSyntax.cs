namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing information about a literal expression.
/// </summary>
public sealed partial record LiteralExpressionSyntax(
    SyntaxToken Token,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: SyntaxKind.NumericLiteralExpression,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<LiteralExpressionSyntax>
{
    static void IVisitable<LiteralExpressionSyntax>.Accept<TVisitor>(LiteralExpressionSyntax node, TVisitor visitor)
        => visitor.VisitLiteralExpressionSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private SyntaxToken _token = ValidateToken(Token, nameof(Token));

    /// <summary>
    /// Gets the token representing the value of the literal expression.
    /// </summary>
    public SyntaxToken Token
    {
        get => _token;
        init => _token = ValidateToken(value, nameof(Token));
    }

    private static SyntaxToken ValidateToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.NumericLiteralToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };
}
