namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a <c>new</c> expression.
/// </summary>
public sealed partial record NewExpressionSyntax(
    SyntaxToken NewKeyword,
    NameSyntax? Type,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: SyntaxKind.NewExpression,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<NewExpressionSyntax>
{
    static void IVisitable<NewExpressionSyntax>.Accept<TVisitor>(NewExpressionSyntax node, TVisitor visitor)
        => visitor.VisitNewExpressionSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

    private SyntaxToken _newKeyword = ValidateNewKeyword(NewKeyword, nameof(NewKeyword));

    /// <summary>
    /// Gets the <c>new</c> keyword.
    /// </summary>
    public SyntaxToken NewKeyword
    {
        get => _newKeyword;
        init => _newKeyword = ValidateNewKeyword(value, nameof(NewKeyword));
    }

    private static SyntaxToken ValidateNewKeyword(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.NewKeyword
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the type to instantiate.
    /// </summary>
    public NameSyntax? Type { get; init; } = Type;
}
