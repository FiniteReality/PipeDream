namespace PipeDream.Compiler.Lexing;

[Flags]
internal enum LexerMode
{
    Invalid = 0,

    // Regular tokens
    Normal = 0b1,

    // String tokens
    String = 0b10,
    Verbatim = 0b100,
    Interpolated = 0b1000,
}