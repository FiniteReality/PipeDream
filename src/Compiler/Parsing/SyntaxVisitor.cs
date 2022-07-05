namespace PipeDream.Compiler.Parsing.Tree;

/// <summary>
/// Defines a type for visiting syntax nodes.
/// </summary>
public abstract class SyntaxVisitor
{
    /// <summary>
    /// Visits the given node.
    /// </summary>
    /// <param name="node">
    /// The node to visit.
    /// </param>
    public void Visit(SyntaxNode node)
    {
        BeforeVisit(node);
        Accept(node);
        node.AcceptInternal(this);
        AfterVisit(node);
    }

    /// <summary>
    /// Invoked before visiting a node.
    /// </summary>
    /// <param name="node">
    /// The node about to be visited.
    /// </param>
    protected virtual void BeforeVisit(SyntaxNode node)
    { }

    /// <summary>
    /// Invoked before visiting a node.
    /// </summary>
    /// <param name="node">
    /// The node that has just been visited.
    /// </param>
    protected virtual void AfterVisit(SyntaxNode node)
    { }

    /// <summary>
    /// Processes the given node.
    /// </summary>
    /// <param name="node">
    /// The node to process.
    /// </param>
    protected abstract void Accept(SyntaxNode node);
}