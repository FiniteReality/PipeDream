using PipeDream.Compiler.Lexing;

namespace PipeDream.Compiler.Parsing;

/// <summary>
/// Defines a struct which contains information about a parse error.
/// </summary>
public record struct ParseError
{
    internal ParseError(TokenSpan span, string message)
    {
        Span = span;
        Message = message;
    }

    /// <summary>
    /// Gets the location where this parsing error occured.
    /// </summary>
    public TokenSpan Span { get; }

    /// <summary>
    /// Gets the error message for this parsing error.
    /// </summary>
    public string Message { get; }

    /// <inheritdoc/>
    public override string ToString()
        => Message;

    internal static ParseError Unexpected(Token token)
        => new(token.Span, $"Unexpected token {token.Kind}");

    internal static ParseError Expected(Token token, SyntaxKind kind)
        => new(token.Span, $"Expected {kind}, got {token.Kind}");
}
