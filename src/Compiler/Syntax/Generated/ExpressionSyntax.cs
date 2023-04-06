namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record from which expression nodes are derived.
/// </summary>
public abstract partial record ExpressionSyntax(
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
