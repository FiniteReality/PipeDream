namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a variable declaration.
/// </summary>
public sealed partial record VariableDeclarationSyntax(
    SyntaxToken VarKeyword,
    NameSyntax Identifier,
    EqualsValueClauseSyntax? Initializer,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : MemberDeclarationSyntax(
        Kind: SyntaxKind.VariableDeclaration,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<VariableDeclarationSyntax>
{
    static void IVisitable<VariableDeclarationSyntax>.Accept<TVisitor>(VariableDeclarationSyntax node, TVisitor visitor)
        => visitor.VisitVariableDeclarationSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private SyntaxToken _varKeyword = ValidateVarKeyword(VarKeyword, nameof(VarKeyword));

    /// <summary>
    /// Gets the <c>var</c> keyword.
    /// </summary>
    public SyntaxToken VarKeyword
    {
        get => _varKeyword;
        init => _varKeyword = ValidateVarKeyword(value, nameof(VarKeyword));
    }

    private static SyntaxToken ValidateVarKeyword(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.VarKeyword
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the name of this variable.
    /// </summary>
    public NameSyntax Identifier { get; init; } = Identifier;

    /// <summary>
    /// Gets the value of this variable or null if there is none.
    /// </summary>
    public EqualsValueClauseSyntax? Initializer { get; init; } = Initializer;
}
