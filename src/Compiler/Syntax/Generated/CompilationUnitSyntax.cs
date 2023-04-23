namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a compilation unit.
/// </summary>
public sealed partial record CompilationUnitSyntax(
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : SyntaxNode(
        Kind: SyntaxKind.CompilationUnit,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
}
