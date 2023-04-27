namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing information about a preprocessor include
/// trivia.
/// </summary>
public sealed partial record IncludeDirectiveTriviaSyntax(
    SyntaxToken IncludeKeyword,
    StringSyntax File,
    SyntaxToken HashToken,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : DirectiveTriviaSyntax(
        HashToken: HashToken,
        Kind: SyntaxKind.IncludeDirectiveTrivia,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<IncludeDirectiveTriviaSyntax>
{
    static void IVisitable<IncludeDirectiveTriviaSyntax>.Accept<TVisitor>(IncludeDirectiveTriviaSyntax node, TVisitor visitor)
        => visitor.VisitIncludeDirectiveTriviaSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private SyntaxToken _includeKeyword = ValidateIncludeKeyword(IncludeKeyword, nameof(IncludeKeyword));

    /// <summary>
    /// Gets the include keyword for this directive.
    /// </summary>
    public SyntaxToken IncludeKeyword
    {
        get => _includeKeyword;
        init => _includeKeyword = ValidateIncludeKeyword(value, nameof(IncludeKeyword));
    }

    private static SyntaxToken ValidateIncludeKeyword(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.IncludeKeyword
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private StringSyntax _file = ValidateFile(File, nameof(File));

    /// <summary>
    /// Gets the relative path to the file to include for this directive.
    /// </summary>
    public StringSyntax File
    {
        get => _file;
        init => _file = ValidateFile(value, nameof(File));
    }

    private static StringSyntax ValidateFile(StringSyntax value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.LiteralString
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
