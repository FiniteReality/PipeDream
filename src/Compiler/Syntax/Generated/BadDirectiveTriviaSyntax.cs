namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing information about an invalid preprocessor
/// definition trivia.
/// </summary>
public sealed partial record BadDirectiveTriviaSyntax(
    NameSyntax? Name,
    SyntaxToken HashToken,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : DirectiveTriviaSyntax(
        HashToken: HashToken,
        Kind: SyntaxKind.BadDirectiveTrivia,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<BadDirectiveTriviaSyntax>
{
    static void IVisitable<BadDirectiveTriviaSyntax>.Accept<TVisitor>(BadDirectiveTriviaSyntax node, TVisitor visitor)
        => visitor.VisitBadDirectiveTriviaSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    /// <summary>
    /// Gets the name of this directive, or null if there was none.
    /// </summary>
    public NameSyntax? Name { get; init; } = Name;
}