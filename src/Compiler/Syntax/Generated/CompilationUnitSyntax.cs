namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a compilation unit.
/// </summary>
public sealed partial record CompilationUnitSyntax(
    SyntaxList<MemberDeclarationSyntax> Members,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : SyntaxNode(
        Kind: SyntaxKind.CompilationUnit,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<CompilationUnitSyntax>
{
    static void IVisitable<CompilationUnitSyntax>.Accept<TVisitor>(CompilationUnitSyntax node, TVisitor visitor)
        => visitor.VisitCompilationUnitSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    /// <summary>
    /// Gets the members of the compilation unit.
    /// </summary>
    public SyntaxList<MemberDeclarationSyntax> Members { get; init; } = Members;
}
