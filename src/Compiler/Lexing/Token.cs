using PipeDream.Compiler.Parsing;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct which represents a syntax token.
/// </summary>
public record struct Token// : IEquatable<Token>
{
    internal Token(SyntaxKind kind, TokenSpan span)
    {
        Kind = kind;
        Span = span;
        IsLiteral = false;
        Value = null;
    }

    internal Token(SyntaxKind kind, TokenSpan span, object? value)
    {
        Kind = kind;
        Span = span;
        IsLiteral = true;
        Value = value;
    }

    /// <summary>
    /// Gets the location of this token in the source file.
    /// </summary>
    public TokenSpan Span { get; }

    /// <summary>
    /// Gets the kind of syntax this token refers to.
    /// </summary>
    public SyntaxKind Kind { get; }

    /// <summary>
    /// Gets whether this token represents a literal value.
    /// </summary>
    /// <remarks>
    /// If <code>true</code>, the <see cref="Value"/> of this node contains the
    /// value of the literal.
    /// </remarks>
    public bool IsLiteral { get; }

    /// <summary>
    /// Gets the value of this token.
    /// </summary>
    public object? Value { get; }
}
