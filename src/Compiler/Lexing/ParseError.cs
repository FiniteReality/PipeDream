namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct which contains a parser error.
/// </summary>
public struct ParseError
{
    internal ParseError(Range span, string message)
    {
        Span = span;
        Message = message;
    }

    /// <summary>
    /// Gets the location where this parse error occured.
    /// </summary>
    public Range Span { get; }

    /// <summary>
    /// Gets the error message for this parse error.
    /// </summary>
    public string Message { get; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Message;
    }
}