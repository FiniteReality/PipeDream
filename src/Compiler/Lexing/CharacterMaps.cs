using System.Collections.Immutable;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

internal static class CharacterMaps
{
    internal static ReadOnlySpan<byte> Identifier
        => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890_"u8;

    internal static ReadOnlySpan<byte> Digit
        => "1234567890"u8;

    internal static ReadOnlySpan<byte> Whitespace
        => "\t "u8;

    internal static ReadOnlySpan<byte> LineTerminator
        => "\r\n"u8;

    internal static ReadOnlySpan<byte> MultiLineCommentCharacters
        => "/*"u8;
}
