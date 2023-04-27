namespace PipeDream.Compiler.Syntax;

internal interface IVisitable
{
    void Accept<TVisitor>(TVisitor visitor)
        where TVisitor : SyntaxVisitor;
}

internal interface IVisitable<TSelf>
    : IVisitable
    where TSelf : IVisitable<TSelf>
{
    static abstract void Accept<TVisitor>(TSelf node, TVisitor visitor)
        where TVisitor : SyntaxVisitor;
}
