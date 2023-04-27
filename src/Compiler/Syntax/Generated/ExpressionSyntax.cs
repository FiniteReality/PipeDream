namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record from which expression nodes are derived.
/// </summary>
public abstract partial record ExpressionSyntax(
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : SyntaxNode(
        Kind: Kind,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<ExpressionSyntax>
{
    static void IVisitable<ExpressionSyntax>.Accept<TVisitor>(ExpressionSyntax node, TVisitor visitor)
        => visitor.VisitExpressionSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

}
