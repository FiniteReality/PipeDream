namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing the value of a
/// <see cref="VariableDeclarationSyntax" />.
/// </summary>
/// <seealso cref="VariableDeclarationSyntax" />
public sealed partial record EqualsValueClauseSyntax(
    SyntaxToken EqualsToken,
    ExpressionSyntax Value,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : SyntaxNode(
        Kind: SyntaxKind.EqualsValueClause,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<EqualsValueClauseSyntax>
{
    static void IVisitable<EqualsValueClauseSyntax>.Accept<TVisitor>(EqualsValueClauseSyntax node, TVisitor visitor)
        => visitor.VisitEqualsValueClauseSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private SyntaxToken _equalsToken = ValidateEqualsToken(EqualsToken, nameof(EqualsToken));

    /// <summary>
    /// Gets the <c>=</c> token.
    /// </summary>
    public SyntaxToken EqualsToken
    {
        get => _equalsToken;
        init => _equalsToken = ValidateEqualsToken(value, nameof(EqualsToken));
    }

    private static SyntaxToken ValidateEqualsToken(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.EqualsToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the value assigned to the variable.
    /// </summary>
    public ExpressionSyntax Value { get; init; } = Value;
}
