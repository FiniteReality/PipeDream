using System.Collections.Immutable;

namespace PipeDream.Compiler.Parsing.Tree;

/// <summary>
/// Defines a record representing any syntax node, token or otherwise.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the parse tree.
/// </param>
public abstract record SyntaxNode(TokenSpan Span)
{
    internal static TokenSpan ComputeSpan(SyntaxNode start, SyntaxNode? end)
        => start.Span with { End = (end ?? start).Span.End };

    /// <summary>
    /// Handles visitation for the given visitor.
    /// </summary>
    /// <param name="visitor">
    /// The visitor to accept.
    /// </param>
    protected virtual void Accept(SyntaxVisitor visitor)
    { /* no-op */ }

    internal void AcceptInternal(SyntaxVisitor visitor)
        => Accept(visitor);
}
