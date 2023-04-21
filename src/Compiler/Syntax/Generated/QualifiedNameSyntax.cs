namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a qualified name.
/// </summary>
public sealed partial record QualifiedNameSyntax(
    SeparatedSyntaxList<SimpleNameSyntax> Parts,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : ExpressionSyntax(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.QualifiedName
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    /// <summary>
    /// Gets the parts of this qualified name.
    /// </summary>
    public SeparatedSyntaxList<SimpleNameSyntax> Parts { get; init; } = Parts;
}
