namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct which represents a syntax token.
/// </summary>
public record struct Token// : IEquatable<Token>
{
    internal Token(SyntaxKind kind, Range span)
    {
        Kind = kind;
        Span = span;
    }

    /// <summary>
    /// Gets the absolute span of this token in characters.
    /// </summary>
    public Range Span { get; }

    /// <summary>
    /// Gets the kind of syntax this token refers to.
    /// </summary>
    public SyntaxKind Kind { get; }
}