//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing preprocessor directives which can produce
/// an expression.
/// </summary>
public sealed partial record PreprocessorExpressionSyntax(
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: SyntaxKind.PreprocessorExpression,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<PreprocessorExpressionSyntax>
{
    static void IVisitable<PreprocessorExpressionSyntax>.Accept<TVisitor>(PreprocessorExpressionSyntax node, TVisitor visitor)
        => visitor.VisitPreprocessorExpressionSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

}
