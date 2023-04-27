namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing information about a preprocessor definition
/// trivia.
/// </summary>
public sealed partial record DefineDirectiveTriviaSyntax(
    SyntaxToken DefineKeyword,
    SyntaxToken Name,
    SyntaxNode Value,
    SyntaxToken HashToken,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : DirectiveTriviaSyntax(
        HashToken: HashToken,
        Kind: SyntaxKind.DefineDirectiveTrivia,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<DefineDirectiveTriviaSyntax>
{
    static void IVisitable<DefineDirectiveTriviaSyntax>.Accept<TVisitor>(DefineDirectiveTriviaSyntax node, TVisitor visitor)
        => visitor.VisitDefineDirectiveTriviaSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private SyntaxToken _defineKeyword = ValidateDefineKeyword(DefineKeyword, nameof(DefineKeyword));

    /// <summary>
    /// Gets the define keyword for this directive.
    /// </summary>
    public SyntaxToken DefineKeyword
    {
        get => _defineKeyword;
        init => _defineKeyword = ValidateDefineKeyword(value, nameof(DefineKeyword));
    }

    private static SyntaxToken ValidateDefineKeyword(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.DefineKeyword
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _name = ValidateName(Name, nameof(Name));

    /// <summary>
    /// Gets the name of the preprocessor variable to define.
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

    /// <summary>
    /// Gets the value to be substituted in place of <see cref="Name" />.
    /// </summary>
    public SyntaxNode Value { get; init; } = Value;
}
