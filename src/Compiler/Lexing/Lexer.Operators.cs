using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
{
    private bool LexEquals()
    {
        if (!_reader.TryPeek(out var doubleEquals))
            return Token(SyntaxKind.Equals, LexerMode.EndOfFile);

        if (doubleEquals == (byte)'=')
        {
            _reader.Advance(1);
            return Token(SyntaxKind.DoubleEquals, _mode);
        }

        return Token(SyntaxKind.Equals, _mode);
    }

    private bool LexDot()
    {
        if (!_reader.TryPeek(out var doubleDot))
            return Token(SyntaxKind.Dot, LexerMode.EndOfFile);

        if (doubleDot == (byte)'.')
        {
            _reader.Advance(1);
            return Token(SyntaxKind.DoubleDot, _mode);
        }

        return Token(SyntaxKind.Dot, _mode);
    }

    private bool LexAmpersand()
    {
        if (!_reader.TryPeek(out var doubleDot))
            return Token(SyntaxKind.Ampersand, LexerMode.EndOfFile);

        if (doubleDot == (byte)'&')
        {
            _reader.Advance(1);
            return Token(SyntaxKind.DoubleAmpersand, _mode);
        }

        return Token(SyntaxKind.Ampersand, _mode);
    }
}
