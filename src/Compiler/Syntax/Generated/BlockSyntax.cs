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

/// <Summary>
/// Defines a record representing a block of code.
/// </Summary>
public sealed partial record BlockSyntax(
    SyntaxToken OpenBraceToken,
    SyntaxList<StatementSyntax> Statements,
    SyntaxToken CloseBraceToken,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : StatementSyntax(
        Kind: SyntaxKind.Block,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<BlockSyntax>
{
    static void IVisitable<BlockSyntax>.Accept<TVisitor>(BlockSyntax node, TVisitor visitor)
        => visitor.VisitBlockSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private SyntaxToken _openBraceToken = ValidateOpenBraceToken(OpenBraceToken, nameof(OpenBraceToken));

    /// <summary>
    /// Gets the <see cref="SyntaxToken" /> representing the open brace for
    /// this block.
    /// </summary>
    public SyntaxToken OpenBraceToken
    {
        get => _openBraceToken;
        init => _openBraceToken = ValidateOpenBraceToken(value, nameof(OpenBraceToken));
    }

    private static SyntaxToken ValidateOpenBraceToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.OpenBraceToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the <see cref="SyntaxList{T}" /> of <see cref="StatementSyntax" />
    /// contained in this block.
    /// </summary>
    public SyntaxList<StatementSyntax> Statements { get; init; } = Statements;

    private SyntaxToken _closeBraceToken = ValidateCloseBraceToken(CloseBraceToken, nameof(CloseBraceToken));

    /// <summary>
    /// Gets the <see cref="SyntaxToken" /> representing the close brace for
    /// this block.
    /// </summary>
    public SyntaxToken CloseBraceToken
    {
        get => _closeBraceToken;
        init => _closeBraceToken = ValidateCloseBraceToken(value, nameof(CloseBraceToken));
    }

    private static SyntaxToken ValidateCloseBraceToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.CloseBraceToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };
}
