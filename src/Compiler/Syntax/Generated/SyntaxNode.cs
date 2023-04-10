namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record from which all syntax nodes are derived.
/// </summary>
public abstract partial record SyntaxNode(
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
{
    /// <summary>
    /// Gets the kind of this syntax node.
    /// </summary>
    public SyntaxKind Kind { get; init; } = Kind;

    /// <summary>
    /// Gets the absolute span of this node, not including trivia.
    /// </summary>
    public SyntaxSpan Span { get; init; } = Span;

    /// <summary>
    /// Gets the list of trivia that appears before this node in the source
    /// code.
    /// </summary>
    public SyntaxList<TriviaSyntax> LeadingTrivia { get; init; } = LeadingTrivia;

    /// <summary>
    /// Gets the list of trivia that appears before this node in the source
    /// code.
    /// </summary>
    public SyntaxList<TriviaSyntax> TrailingTrivia { get; init; } = TrailingTrivia;
}
