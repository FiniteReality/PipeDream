namespace PipeDream.Compiler.Lexing;

[Flags]
internal enum LexerMode
{
    /// <summary>
    /// Invalid lexer mode
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// Regular tokens
    /// </summary>
    Normal = 0b1,

    /// <summary>
    /// String tokens
    /// </summary>
    String = 0b10,
    /// <summary>
    /// The string spans multiple lines
    /// </summary>
    Verbatim = 0b100,
    /// <summary>
    /// The string accepts interpolated segments
    /// </summary>
    Interpolated = 0b1000,

    /// <summary>
    /// The string represents a path to a resource.
    /// </summary>
    Resource = 0b10000
}
