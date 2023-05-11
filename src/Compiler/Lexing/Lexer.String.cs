using System.Buffers;
using System.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
{
    private OperationStatus LexStringText(out LexerToken token)
    {
        _reader.StartTracking();

        bool escapeNext = false;
        while (_reader.TryPeek(out var read))
        {
            switch (read)
            {
                // Open brackets are only important in interpolated strings,
                // when not escaped.
                case (byte)'[' when !escapeNext
                    && (_mode & LexerMode.Interpolated) != 0:
                    _mode |= LexerMode.Normal;
                    return GetToken(ref _reader, out token);


                // Double quotes are only important in non-resource strings
                case (byte)'"' when !escapeNext
                    && (_mode & LexerMode.Resource) == 0
                    && (_mode & LexerMode.Verbatim) == 0:
                    _mode |= LexerMode.Normal;
                    return GetToken(ref _reader, out token);

                // In verbatim strings we only care if the quote is followed by
                // a close brace.
                case (byte)'"' when !escapeNext
                    && (_mode & LexerMode.Resource) == 0:
                {
                    if (!_reader.TryPeek(1, out var next))
                    {
                        token = default;
                        return OperationStatus.NeedMoreData;
                    }
                    else if (next != '}')
                        goto default;

                    _mode |= LexerMode.Normal;
                    return GetToken(ref _reader, out token);
                }

                // Single quotes are only important in resource strings
                case (byte)'\'' when !escapeNext
                    && (_mode & LexerMode.Resource) != 0:
                    _mode |= LexerMode.Normal;
                    return GetToken(ref _reader, out token);

                // Backslashes are only important in interpolated strings,
                // when not escaped
                case (byte)'\\' when !escapeNext
                    && (_mode & LexerMode.Interpolated) != 0:
                    // Consume the backslash
                    _reader.Advance();
                    // Ensure we're escaping the next character
                    escapeNext = true;
                    break;

                // If we're escaping a carriage return, we also want to escape
                // the newline that's coming up next
                case (byte)'\r' when escapeNext
                    && (_mode & LexerMode.Verbatim) == 0:
                {
                    if (!_reader.TryPeek(1, out var newline))
                    {
                        token = default;
                        return OperationStatus.NeedMoreData;
                    }
                    // If we're a lone CR, just handle it normally.
                    else if (newline != '\n')
                        goto default;

                    // Otherwise, consume both of them safely.
                    _reader.Advance();
                    _reader.Advance();
                    escapeNext = false;
                    break;
                }

                case (byte)'\r' or (byte)'\n' when !escapeNext
                    && (_mode & LexerMode.Verbatim) == 0:
                {
                    // newlines aren't allowed in non-verbatim strings
                    ProduceDiagnostic(LexError.UnterminatedString);
                    Debug.Fail("womp womp");
                    token = new(
                        Start: _reader.TrackedPosition,
                        End: _reader.Position,
                        Kind: LineEndQuoteKind(_mode)
                    );
                    return OperationStatus.InvalidData;
                }

                default:
                    // Consume the character
                    _reader.Advance();
                    // Ensure we're not escaping the next character
                    escapeNext = false;
                    break;
            }
        }

        // Could we produce multiple StringTextTokens here? It might be an
        // option for very large strings...
        token = default;
        return OperationStatus.NeedMoreData;

        static OperationStatus GetToken(
            ref Reader reader,
            out LexerToken token)
        {
            var status = reader.TryGetString(out var text);
            if (status != OperationStatus.Done)
            {
                Debug.Assert(status != OperationStatus.InvalidData, "LexString gettoken fail");
                token = default;
                return status;
            }

            token = new(
                Kind: SyntaxKind.StringTextToken,
                Start: reader.TrackedPosition,
                End: reader.Position)
            {
                StringValue = text
            };
            return OperationStatus.Done;
        }

        static SyntaxKind LineEndQuoteKind(LexerMode mode)
        {
            // We don't need to handle verbatim strings here as those span
            // multiple lines - the line terminator gets consumed as part of the
            // string.
            if ((mode & LexerMode.Interpolated) != 0)
                return SyntaxKind.InterpolatedStringEndToken;
            else if ((mode & LexerMode.Resource) != 0)
                return SyntaxKind.ResourceStringEndToken;
            else
                return SyntaxKind.RawStringEndToken;
        }
    }
}
