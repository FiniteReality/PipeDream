using System.Collections.Immutable;
using PipeDream.Compiler.Lexing;

namespace PipeDream.Compiler.Parsing.Tree;

internal sealed record CompilationUnitNode(
    ImmutableArray<SyntaxNode> Children)
    : SyntaxNode(ComputeSpan(Children))
{
    private static TokenSpan ComputeSpan(ImmutableArray<SyntaxNode> children)
        => children.Length switch
        {
            0 => default,
            1 => children[0].Span,
            _ => children[0].Span with { End = children[^1].Span.End }
        };
}

internal sealed record CommentNode(SyntaxNode Contents, SyntaxNode LineEnd)
    : SyntaxNode(Contents.Span with { End = LineEnd.Span.End });

internal sealed record RootPathNode(SyntaxNode Slash)
    : SyntaxNode(Slash.Span);

internal sealed record BinaryExpressionNode(
    SyntaxNode Left, SyntaxNode Right, SyntaxKind Kind)
    : SyntaxNode(Left.Span with { End = Right.Span.End });

internal sealed record StatementNode(
    SyntaxNode Statement, SyntaxNode LineEnd)
    : SyntaxNode(Statement.Span with { End = LineEnd.Span.End });

internal sealed record AssignmentStatementNode(
    SyntaxNode Left, SyntaxNode Right)
    : SyntaxNode(Left.Span with { End = Right.Span.End });