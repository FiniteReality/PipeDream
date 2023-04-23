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
{
    /// <summary>
    /// Gets the parts of this qualified name.
    /// </summary>
    public SeparatedSyntaxList<NameSyntax> Parts { get; init; } = Parts;
}
