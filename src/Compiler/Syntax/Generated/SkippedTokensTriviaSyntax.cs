namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record which represents skipped tokens in the syntax tree.
/// </summary>
public sealed partial record SkippedTokensTriviaSyntax(
    SyntaxList<SyntaxToken> Tokens,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : TriviaSyntax(
        Kind: SyntaxKind.SkippedTokensTrivia,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    /// <summary>
    /// Gets the tokens which were skipped in order to successfully parse the
    /// next token.
    /// </summary>
    public SyntaxList<SyntaxToken> Tokens { get; init; } = Tokens;
}
