using System.Buffers;
using System.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

internal ref partial struct Lexer
{
    private void BeginToken()
    {
        _tokenBeginning = _reader.Position;
        _initialMode = _mode;
    }

    private void EndToken(bool rewind)
    {
        if (rewind)
        {
            _reader.Rewind(_tokenBeginning);
            // Restore the mode to ensure we lex the token correctly when
            // invoked again with more data.
            _mode = _initialMode;
        }
    }

    private OperationStatus LexToken(out LexerToken token)
    {
        _reader.StartTracking();

        if (!_reader.TryPeek(out var read))
        {
            token = default;
            return OperationStatus.NeedMoreData;
        }

        switch (read)
        {
            // If it starts with an A-Z/a-z character, it could either be
            // a keyword or an identifier.
            case (>= (byte)'A' and <= (byte)'Z') or
                (>= (byte)'a' and <= (byte)'z'):
                return LexKeywordOrIdentifier(out token);

            // If it starts with an underscore, it can only be an identifier
            case (byte)'_':
                return LexIdentifier(out token);

            // If it starts with a digit, it can only be a number
            case >= (byte)'0' and <= (byte)'9':
                return LexNumber(out token);

            // TODO: other punctuation tokens
            case (>= (byte)'!' and <= (byte)'/') or
                (>= (byte)':' and <= (byte)'@') or
                (>= (byte)'[' and <= (byte)'^') or
                (>= (byte)'{' and <= (byte)'~'):
                return LexPunctuation(out token);

            default:
                _reader.Advance();

                var status = _reader.TryGetString(out var invalid);
                if (status != OperationStatus.Done)
                {
                    token = new(SyntaxKind.BadToken,
                        _reader.TrackedPosition,
                        _reader.Position);
                    return status;
                }

                token = new(SyntaxKind.BadToken,
                    _reader.TrackedPosition, _reader.Position)
                {
                    StringValue = invalid
                };
                ProduceDiagnostic(read, LexError.UnexpectedCharacter);
                return OperationStatus.InvalidData;
        }
    }
}
