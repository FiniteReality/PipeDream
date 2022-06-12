using System.Collections.Immutable;

namespace PipeDream.Compiler.Parsing.Tree;

internal abstract record SyntaxNode(TokenSpan Span)
{
    internal static TokenSpan ComputeSpan(SyntaxNode start, SyntaxNode? end)
        => start.Span with { End = (end ?? start).Span.End };

    internal static TokenSpan ComputeSpan(SyntaxNode start,
        ImmutableArray<SyntaxNode> contents)
        => start.Span with
        {
            End = contents.Length switch
            {
                <= 0 => start.Span.End,
                > 0 => contents[^1].Span.End
            }
        };
}
