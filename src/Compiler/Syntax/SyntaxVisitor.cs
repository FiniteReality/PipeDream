using System.Diagnostics;

namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a class used for visiting syntax nodes.
/// </summary>
public abstract partial class SyntaxVisitor
{
    /// <summary>
    /// Visits the given syntax node.
    /// </summary>
    /// <param name="node">
    /// The node to visit.
    /// </param>
    /// <typeparam name="T">
    /// The type of node to visit.
    /// </typeparam>
    public void Visit<T>(T node)
        where T : SyntaxNode
    {
        Debug.Assert(node is IVisitable);
        ((IVisitable)node).Accept(this);
    }

    internal void VisitNode<TNode>(TNode node)
        where TNode : SyntaxNode, IVisitable<TNode>
    {
        BeforeVisit(node);
        TNode.Accept(node, this);
        AfterVisit(node);
    }

    private void VisitSyntaxList<TNode>(SyntaxList<TNode> list)
        where TNode : SyntaxNode, IVisitable<TNode>
    {
        foreach (var item in list)
        {
            Visit(item);
        }
    }

    private void VisitSeparatedSyntaxList<TNode>(
        SeparatedSyntaxList<TNode> list)
        where TNode : SyntaxNode, IVisitable<TNode>
    {
        for (int i = 0; i < list.Count; i++)
        {
            Visit(list[i]);

            if (i < list.SeparatorCount)
                VisitNode(list.GetSeparator(i));
        }
    }

    /// <summary>
    /// Called just before visiting a syntax node.
    /// </summary>
    /// <param name="node">
    /// The node that is about to be visited.
    /// </param>
    protected virtual void BeforeVisit(SyntaxNode node)
    { }

    /// <summary>
    /// Called just after visiting a syntax node.
    /// </summary>
    /// <param name="node">
    /// The node that was just visited.
    /// </param>
    protected virtual void AfterVisit(SyntaxNode node)
    { }
}
