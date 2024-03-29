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
/// Defines a record containing information about a preprocessor definition
/// trivia.
/// </summary>
public sealed partial record DefineDirectiveTriviaSyntax(
    SyntaxToken DefineKeyword,
    SimpleNameSyntax Name,
    SyntaxNode? Value,
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
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the name of the preprocessor variable to define.
    /// </summary>
    public SimpleNameSyntax Name { get; init; } = Name;

    /// <summary>
    /// Gets the value to be substituted in place of <see cref="Name" />,
    /// or null if there is none.
    /// </summary>
    public SyntaxNode? Value { get; init; } = Value;
}
