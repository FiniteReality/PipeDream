using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
{
    private bool LexString(byte opener)
    {
        switch (opener)
        {
            case (byte)'"':
                // Standard string
                return LexBasicString(true);
            case (byte)'{':
            {
                if (!_reader.TryPeek(out var two))
                    return Stop();

                if (two != (byte)'"')
                    return Error("Expected quote after text document opener");

                if (!LexBasicString(false))
                    return false;

                if (!_reader.TryRead(out two))
                    return Stop();

                if (two != (byte)'}')
                    return Error(
                        "Expected closing brace after text document closer");

                return Token(SyntaxKind.String, LexerMode.MiddleOfLine);
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
    private bool LexBasicString(bool produceToken)
    {
        while (true)
        {
            if (!_reader.TryAdvanceToAny(BasicStringCharactersOfInterest, false))
                return Stop();

            if (!_reader.TryRead(out var interest))
            {
                Debug.Fail("TryRead returned false?");
                return Stop();
            }

            if (interest == (byte)'"')
                // We didn't hit an escape so we're a complete string
                return !produceToken
                    || Token(SyntaxKind.String, LexerMode.MiddleOfLine);

            // We don't actually care what the escaped character is, we just
            // don't want to read it
            if (!_reader.TryRead(out _))
                return Stop();
        }
    }
}