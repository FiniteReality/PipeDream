namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a rooted name.
/// </summary>
public sealed partial record RootedNameSyntax(
    SyntaxToken PathRootToken,
    SyntaxToken Name,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : NameSyntax(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.RootedName
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _pathRootToken = ValidatePathRootToken(PathRootToken, nameof(PathRootToken));

    /// <summary>
    /// Gets the syntax token representing the path root token.
    /// </summary>
    public SyntaxToken PathRootToken
    {
        get => _pathRootToken;
        init => _pathRootToken = ValidatePathRootToken(value, nameof(PathRootToken));
    }

    private static SyntaxToken ValidatePathRootToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.SlashToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _name = ValidateName(Name, nameof(Name));

    /// <summary>
    /// Gets the syntax token representing the identifier of this name.
    /// </summary>
    public SyntaxToken Name
    {
        get => _name;
        init => _name = ValidateName(value, nameof(Name));
    }

    private static SyntaxToken ValidateName(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.IdentifierToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
