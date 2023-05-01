namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record from which member declaration nodes are derived.
/// </summary>
public abstract partial record MemberDeclarationSyntax(
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : SyntaxNode(
        Kind: Kind,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<MemberDeclarationSyntax>
{
    static void IVisitable<MemberDeclarationSyntax>.Accept<TVisitor>(MemberDeclarationSyntax node, TVisitor visitor)
        => visitor.VisitMemberDeclarationSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

}
