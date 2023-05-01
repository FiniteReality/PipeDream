namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing information about a string.
/// </summary>
public abstract partial record StringSyntax(
    SyntaxToken StringStartToken,
    SyntaxToken StringEndToken,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : SyntaxNode(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<StringSyntax>
{
    static void IVisitable<StringSyntax>.Accept<TVisitor>(StringSyntax node, TVisitor visitor)
        => visitor.VisitStringSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.InterpolatedString or
            SyntaxKind.LiteralString
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _stringStartToken = ValidateStringStartToken(StringStartToken, nameof(StringStartToken));

    /// <summary>
    /// Gets the start token of this string.
    /// </summary>
    public virtual SyntaxToken StringStartToken
    {
        get => _stringStartToken;
        init => _stringStartToken = ValidateStringStartToken(value, nameof(StringStartToken));
    }

    private static SyntaxToken ValidateStringStartToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.InterpolatedStringStartToken or
            SyntaxKind.InterpolatedVerbatimStringStartToken or
            SyntaxKind.RawStringStartToken or
            SyntaxKind.RawVerbatimStringStartToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _stringEndToken = ValidateStringEndToken(StringEndToken, nameof(StringEndToken));

    /// <summary>
    /// Gets the end token of this string.
    /// </summary>
    public virtual SyntaxToken StringEndToken
    {
        get => _stringEndToken;
        init => _stringEndToken = ValidateStringEndToken(value, nameof(StringEndToken));
    }

    private static SyntaxToken ValidateStringEndToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.InterpolatedStringEndToken or
            SyntaxKind.InterpolatedVerbatimStringEndToken or
            SyntaxKind.RawStringEndToken or
            SyntaxKind.RawVerbatimStringEndToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };
}
