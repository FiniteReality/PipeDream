using System.Buffers;
using System.Diagnostics;
using System.Text;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
{
    private bool LexString(byte opener)
    {
        switch (opener)
        {
            case (byte)'"':
            {
                if (!LexBasicString(out var value))
                    return Stop();

                return Token(SyntaxKind.String, LexerMode.MiddleOfLine, value);
            }
            case (byte)'{':
            {
                if (!_reader.TryRead(out var two))
                    return Stop();

                if (two != (byte)'"')
                    return Error("Expected quote after text document opener");

                if (!LexBasicString(out var value))
                    return Stop();

                if (!_reader.TryRead(out two))
                    return Stop();

                if (two != (byte)'}')
                    return Error(
                        "Expected closing brace after text document closer");

                return Token(SyntaxKind.String, LexerMode.MiddleOfLine, value);
            }
            case (byte)'@':
            // Raw strings
            default:
                Debug.Fail("Attempted to call LexString with invalid opener");
                return false;
        }
    }

    private static ReadOnlySpan<byte> BasicStringCharactersOfInterest
        => new byte[]
        {
            (byte)'"', (byte)'\\'
        };
    private bool LexBasicString(out object? value)
    {
        value = null;
        var start = _reader.Position;
        while (true)
        {
            if (!_reader.TryAdvanceToAny(
                BasicStringCharactersOfInterest, false))
                return Stop();

            if (!_reader.TryPeek(out var interest))
                return Stop();

            // If it's the close character, then we mustn't have hit an escape
            if (interest == (byte)'"')
            {
                // Get the contents of the string, ignoring any escape
                // sequences for now.
                var region = _reader.Sequence.Slice(start, _reader.Position);
                value = GetString(region);

                // Skip the quote
                _reader.Advance(1);

                return true;
            }
            // Else, we read an escape character and need to consume it
            else if (!_reader.TryRead(out _))
                return Stop();
        }

        static string GetString(ReadOnlySequence<byte> sequence)
        {
            if (sequence.IsSingleSegment)
            {
                return Encoding.UTF8.GetString(sequence.FirstSpan);
            }
            else if (sequence.Length <= 256)
            {
                Span<byte> buffer = stackalloc byte[256];
                sequence.CopyTo(buffer);
                return Encoding.UTF8.GetString(
                    buffer[..unchecked((int)sequence.Length)]);
            }
            else
            {
                return Encoding.UTF8.GetString(sequence.ToArray());
            }
        }
    }
}
