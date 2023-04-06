namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct which contains information about the location of a syntax
/// token.
/// </summary>
public record struct TokenSpan
{
    internal TokenSpan(TokenPosition start, TokenPosition end)
    {
        Start = start;
        End = end;
    }

    /// <summary>
    /// Gets the start position of this token span.
    /// </summary>
    public TokenPosition Start { get; init; }

    /// <summary>
    /// Gets the end position of this token span.
    /// </summary>
    public TokenPosition End { get; init; }

    /// <summary>
    /// Gets the length in bytes of this token.
    /// </summary>
    public long LengthInBytes
        => End.ByteOffset - Start.ByteOffset;

    /// <inheritdoc/>
    public override string ToString()
        => $"{Start}:{End}";
}

/// <summary>
/// Defines a struct which contains positional information of a token.
/// </summary>
public record struct TokenPosition
{
    internal TokenPosition(int line, int column, long byteOffset)
    {
        Line = line;
        Column = column;
        ByteOffset = byteOffset;
    }

    /// <summary>
    /// Gets the line number of this token.
    /// </summary>
    public int Line { get; }

    /// <summary>
    /// Gets the column number of this token.
    /// </summary>
    public int Column { get; }

    /// <summary>
    /// Gets the byte offset from the beginning of the source file of this
    /// token.
    /// </summary>
    public long ByteOffset { get; }

    /// <inheritdoc/>
    public override string ToString()
        => $"{Line},{Column}";
}
