using System.Buffers;
using System.Buffers.Text;
using System.Text;
using System.Text.Unicode;

namespace PipeDream.Compiler.Lexing;

ref partial struct Lexer
{
    private static ReadOnlySpan<byte> MultiLineOpener
        => new byte[]
        {
            (byte)'/', (byte)'*'
        };
    private static ReadOnlySpan<byte> MultiLineCloser
        => new byte[]
        {
            (byte)'*', (byte)'/'
        };
    private static ReadOnlySpan<byte> NewlineTokens
        => new byte[]
        {
            (byte)'\r', (byte)'\n'
        };

    private (bool success, bool wasComment) LexComment()
    {
        if (!_reader.TryPeek(out var type))
            return (Stop(), false);

        if (type == '/')
        {
            // single-line comment
            _ = _reader.TryAdvanceToAny(NewlineTokens, false);
            return (true, true);
        }
        else if (type == '*')
        {
            // multi-line comment
            if (SkipNestedComments(ref _reader))
            {
                _lineNumber += CountLines(
                    _reader.Sequence.Slice(_start, _reader.Position));
                return (true, true);
            }

            return (Error("Incomplete multi-line comment"), true);
        }
        else
        {
            return (Token(SyntaxKind.Slash, LexerMode.MiddleOfLine), false);
        }

        static bool SkipNestedComments(
            ref SequenceReader<byte> reader, int depth = 1)
        {
            while (depth > 0)
            {
                if (reader.TryReadTo(out ReadOnlySequence<byte> enclosed,
                    MultiLineCloser, true))
                {
                    depth--;
                }
                else
                {
                    return false;
                }

                var subreader = new SequenceReader<byte>(enclosed);
                if (subreader.TryReadTo(out ReadOnlySequence<byte> _,
                    MultiLineOpener, true))
                {
                    // enclosed looks something like this:
                    // /* ... /* ... */
                    depth++;
                }
            }

            return true;
        }
    }
}