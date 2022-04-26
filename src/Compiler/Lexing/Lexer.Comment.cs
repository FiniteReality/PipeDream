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

    private bool LexComment()
    {
        if (!_reader.TryPeek(out var type))
            return Stop();

        if (type == '/')
        {
            // single-line comment
            if (!_reader.TryAdvanceToAny(NewlineTokens, false))
            {
                // comment spans to the end of the file
                return Token(SyntaxKind.SingleLineComment, LexerMode.EndOfFile);
            }

            return Token(SyntaxKind.SingleLineComment, LexerMode.EndOfLine);
        }
        else if (type == '*')
        {
            // multi-line comment
            if (SkipNestedComments(ref _reader))
            {
                return Token(SyntaxKind.MultiLineComment,
                    LexerMode.MiddleOfLine);
            }

            return Error("Incomplete multi-line comment");
        }
        else
        {
            return Token(SyntaxKind.Slash, LexerMode.MiddleOfLine);
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