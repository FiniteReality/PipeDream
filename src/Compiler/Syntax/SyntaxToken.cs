namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a syntax token.
/// </summary>
public readonly record struct SyntaxToken(SyntaxKind Kind)
{
    /// <summary>
    /// Gets or initializes the kind of syntax this token represents.
    /// </summary>
    public SyntaxKind Kind { get; init; } = Kind;
}