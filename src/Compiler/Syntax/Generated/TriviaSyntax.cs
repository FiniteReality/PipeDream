namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record from which trivia syntax nodes are derived.
/// </summary>
public abstract partial record TriviaSyntax(
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : SyntaxNode(
        Kind: Kind,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<TriviaSyntax>
{
    static void IVisitable<TriviaSyntax>.Accept<TVisitor>(TriviaSyntax node, TVisitor visitor)
        => visitor.VisitTriviaSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

}
