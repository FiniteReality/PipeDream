namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a compilation unit.
/// </summary>
public sealed partial record CompilationUnitSyntax(
    SyntaxList<MemberDeclarationSyntax> Members,
    SyntaxToken EndOfFileToken,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : SyntaxNode(
        Kind: SyntaxKind.CompilationUnit,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<CompilationUnitSyntax>
{
    static void IVisitable<CompilationUnitSyntax>.Accept<TVisitor>(CompilationUnitSyntax node, TVisitor visitor)
        => visitor.VisitCompilationUnitSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    /// <summary>
    /// Gets the members of the compilation unit.
    /// </summary>
    public SyntaxList<MemberDeclarationSyntax> Members { get; init; } = Members;

    private SyntaxToken _endOfFileToken = ValidateEndOfFileToken(EndOfFileToken, nameof(EndOfFileToken));

    /// <summary>
    /// Gets the end of file token.
    /// </summary>
    public SyntaxToken EndOfFileToken
    {
        get => _endOfFileToken;
        init => _endOfFileToken = ValidateEndOfFileToken(value, nameof(EndOfFileToken));
    }

    private static SyntaxToken ValidateEndOfFileToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.EndOfFileToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };
}
