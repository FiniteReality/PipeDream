namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing information about a string with no
/// interpolated sections.
/// </summary>
public abstract partial record LiteralStringSyntax(
    SyntaxToken StringStartToken,
    SyntaxToken Text,
    SyntaxToken StringEndToken,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : StringSyntax(
        StringStartToken: ValidateStringStartToken(StringStartToken, nameof(StringStartToken)),
        StringEndToken: ValidateStringEndToken(StringEndToken, nameof(StringEndToken)),
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.LiteralString
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the start token of this string.
    /// </summary>
    public override SyntaxToken StringStartToken
    {
        get => base.StringStartToken;
        init => base.StringStartToken = ValidateStringStartToken(value, nameof(StringStartToken));
    }

    private static SyntaxToken ValidateStringStartToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.InterpolatedStringStartToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _text = ValidateText(Text, nameof(Text));

    /// <summary>
    /// Gets the text of this string.
    /// </summary>
    public SyntaxToken Text
    {
        get => _text;
        init => _text = ValidateText(value, nameof(Text));
    }

    private static SyntaxToken ValidateText(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.StringTextToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the end token of this string.
    /// </summary>
    public override SyntaxToken StringEndToken
    {
        get => base.StringEndToken;
        init => base.StringEndToken = ValidateStringEndToken(value, nameof(StringEndToken));
    }

    private static SyntaxToken ValidateStringEndToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.InterpolatedStringEndToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
