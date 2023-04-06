namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record from which statement nodes are derived.
/// </summary>
public abstract partial record StatementSyntax(
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxTriviaList LeadingTrivia,
    SyntaxTriviaList TrailingTrivia)
    : SyntaxNode(
        Kind: Kind,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
}
