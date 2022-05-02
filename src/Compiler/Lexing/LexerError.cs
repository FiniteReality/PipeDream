using PipeDream.Compiler.Parsing;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct which contains information a lexing error.
/// </summary>
public record struct LexerError
{
    internal LexerError(TokenSpan span, string message)
    {
        Span = span;
        Message = message;
    }

    /// <summary>
    /// Gets the location where this lexing error occured.
    /// </summary>
    public TokenSpan Span { get; }

    /// <summary>
    /// Gets the error message for this lexing error.
    /// </summary>
    public string Message { get; }

    /// <inheritdoc/>
    public override string ToString()
        => Message;
}
