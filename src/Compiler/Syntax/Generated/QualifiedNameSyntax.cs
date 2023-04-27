namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a qualified name.
/// </summary>
public sealed partial record QualifiedNameSyntax(
    SeparatedSyntaxList<NameSyntax> Parts,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : NameSyntax(
        Kind: SyntaxKind.QualifiedName,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<QualifiedNameSyntax>
{
    static void IVisitable<QualifiedNameSyntax>.Accept<TVisitor>(QualifiedNameSyntax node, TVisitor visitor)
        => visitor.VisitQualifiedNameSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    /// <summary>
    /// Gets the parts of this qualified name.
    /// </summary>
    public SeparatedSyntaxList<NameSyntax> Parts { get; init; } = Parts;
}
