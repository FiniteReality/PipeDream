namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record from which name syntax nodes are derived.
/// </summary>
public abstract partial record NameSyntax(
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: Kind,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
}
