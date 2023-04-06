namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a compilation unit.
/// </summary>
public abstract partial record CompilationUnitSyntax(
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxTriviaList LeadingTrivia,
    SyntaxTriviaList TrailingTrivia)
    : SyntaxNode(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.CompilationUnit
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
