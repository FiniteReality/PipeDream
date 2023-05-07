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
                case (byte)'['
                    when !escapeNext && (_mode & LexerMode.Interpolated) != 0:
                    _mode |= LexerMode.Normal;
                    return GetToken(ref _reader, out token);


                // Quotes are always important, except when escaped.
                case (byte)'"' or (byte)'\'' when !escapeNext:
                    _mode |= LexerMode.Normal;
                    return GetToken(ref _reader, out token);

                // Backslashes are only important in interpolated strings,
                // when not escaped
                case (byte)'\\'
                    when !escapeNext && (_mode & LexerMode.Interpolated) == 0:
                    // Consume the backslash
                    _reader.Advance();
                    // Ensure we're escaping the next character
                    escapeNext = true;
                    break;

                case (byte)'\r' or (byte)'\n'
                    when (_mode & LexerMode.Verbatim) == 0:
                {
                    // newlines aren't allowed in non-verbatim strings
                    ProduceDiagnostic(LexError.UnterminatedString);
                    Debug.Fail("Unterminated string");
                    token = default;
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
    }
}
