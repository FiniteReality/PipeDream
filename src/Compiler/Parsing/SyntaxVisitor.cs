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
        BeforeVisit();
        Accept(node);
        node.AcceptInternal(this);
        AfterVisit();
    }

    /// <summary>
    /// Invoked before visiting a node.
    /// </summary>
    protected virtual void BeforeVisit()
    { }

    /// <summary>
    /// Invoked before visiting a node.
    /// </summary>
    protected virtual void AfterVisit()
    { }

    /// <summary>
    /// Processes the given node.
    /// </summary>
    /// <param name="node">
    /// The node to process.
    /// </param>
    protected abstract void Accept(SyntaxNode node);
}