namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record from which preprocessor directive nodes are derived.
/// </summary>
public abstract partial record DirectiveTriviaSyntax(
    SyntaxToken HashToken,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : TriviaSyntax(
        Kind: Kind,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private SyntaxToken _hashToken = ValidateHashToken(HashToken, nameof(HashToken));

    /// <summary>
    /// Gets the hash token used to mark this directive.
    /// </summary>
    public SyntaxToken HashToken
    {
        get => _hashToken;
        init => _hashToken = ValidateHashToken(value, nameof(HashToken));
    }

    private static SyntaxToken ValidateHashToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.HashToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
