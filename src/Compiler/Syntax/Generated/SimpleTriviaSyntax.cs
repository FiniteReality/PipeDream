namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record which represents unstructured trivia.
/// </summary>
public sealed partial record SimpleTriviaSyntax(
    string Text,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : TriviaSyntax(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.EndOfLineTrivia or
            SyntaxKind.SingleLineCommentTrivia or
            SyntaxKind.WhitespaceTrivia
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the text of this trivia.
    /// </summary>
    public string Text { get; init; } = Text;
}
