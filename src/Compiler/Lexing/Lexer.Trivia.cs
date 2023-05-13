using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

internal ref partial struct Lexer
{
    private OperationStatus LexTriviaList(bool trailing,
        ref SyntaxListBuilder<TriviaSyntax> list)
    {
        do
        {
            var status = LexTrivia(out var trivia);

            switch (status)
            {
                case OperationStatus.InvalidData:
                    return OperationStatus.Done;
                case OperationStatus.NeedMoreData:
                    return OperationStatus.NeedMoreData;
                case OperationStatus.Done:
                    list.Add(ProduceTrivia(trivia));
                    if (trailing && trivia.Kind == SyntaxKind.EndOfLineTrivia)
                        return OperationStatus.Done;
                    break;
                default:
                    // Invalid result, this should never happen
                    Debug.Fail("Unexpected result from LexTriviaList");
                    return OperationStatus.Done;
            }
        }
        while (true);
    }

    private static SimpleTriviaSyntax ProduceTrivia(LexerToken token)
        => new(
            Kind: token.Kind,
            Text: token.StringValue!,
            Span: new(),
            LeadingTrivia: new(),
            TrailingTrivia: new());

    private OperationStatus LexTrivia(out LexerToken token)
    {
        _reader.StartTracking();

        if (!_reader.TryPeek(out var read))
        {
            token = default;
            return _reader.IsStreamEnd
                ? OperationStatus.InvalidData
                : OperationStatus.NeedMoreData;
        }

        switch (read)
        {
            // Comment
            case (byte)'/':
                return LexCommentTrivia(out token);
            // Whitespace
            case (byte)' ' or (byte)'\t':
                return LexWhitespaceTrivia(out token);
            case (byte)'\r' or (byte)'\n':
                return LexEndOfLineTrivia(out token);
        }

        // We don't need to consume anything here, as that's handled by
        // LexToken
        token = default;
        return OperationStatus.InvalidData;
    }

    private OperationStatus LexCommentTrivia(out LexerToken token)
    {
        // We've peeked at the 0th index and it's a '/'
        if (!_reader.TryPeek(1, out var next) && !_reader.IsStreamEnd)
        {
            token = default;
            return OperationStatus.NeedMoreData;
        }

        if (next == '/')
        {
            // Single-line comment
            var status = _reader.TryReadToAny(CharacterMaps.LineTerminator);

            if (status != OperationStatus.Done)
            {
                token = default;
                return status;
            }

            status = _reader.TryGetString(out var comment);
            if (status != OperationStatus.Done)
            {
                token = default;
                return status;
            }

            token = new(
                Kind: SyntaxKind.SingleLineCommentTrivia,
                Start: _reader.TrackedPosition,
                End: _reader.Position)
            {
                StringValue = comment
            };
            return OperationStatus.Done;
        }
        else if (next == '*')
        {
            var status = TryComputeDepth(ref _reader, out var depth);
            if (status != OperationStatus.Done)
            {
                token = default;
                return status;
            }

            while (depth > 0)
            {
                status = _reader.TryReadToAny(
                    CharacterMaps.MultiLineCommentCharacters);
                if (status != OperationStatus.Done)
                {
                    token = default;
                    return status;
                }

                if (!_reader.TryRead(out var cur) ||
                    !_reader.TryRead(out next))
                {
                    token = default;
                    return OperationStatus.NeedMoreData;
                }

                switch ((cur, next))
                {
                    case ((byte)'*', (byte)'/'):
                        depth--;
                        break;
                    case ((byte)'/', (byte)'*'):
                        depth++;
                        break;
                    default:
                        continue;
                }
            }

            status = _reader.TryGetString(out var comment);
            if (status != OperationStatus.Done)
            {
                token = default;
                return status;
            }

            token = new(
                Kind: SyntaxKind.MultiLineCommentTrivia,
                Start: _reader.TrackedPosition,
                End: _reader.Position)
            {
                StringValue = comment
            };
            return OperationStatus.Done;
        }

        token = default;
        return OperationStatus.InvalidData;

        static OperationStatus TryComputeDepth(ref Reader reader, out int depth)
        {
            depth = 0;
            do
            {
                if (!reader.TryPeek(out var slash) ||
                    !reader.TryPeek(1, out var asterisk))
                    return OperationStatus.NeedMoreData;

                if (slash != '/' || asterisk != '*')
                    break;

                var status = reader.TryAdvance(2);
                if (status != OperationStatus.Done)
                    return status;

                depth++;
            }
            while (true);
            return OperationStatus.Done;
        }
    }

    private OperationStatus LexWhitespaceTrivia(out LexerToken token)
    {
        var status = _reader.TryAdvancePastAny(CharacterMaps.Whitespace);
        if (status != OperationStatus.Done)
        {
            token = default;
            return status;
        }

        status = _reader.TryGetString(out var whitespace);
        if (status != OperationStatus.Done)
        {
            token = default;
            return status;
        }

        token = new(
            Kind: SyntaxKind.WhitespaceTrivia,
            Start: _reader.TrackedPosition,
            End: _reader.Position)
        {
            StringValue = whitespace
        };
        return OperationStatus.Done;
    }

    private OperationStatus LexEndOfLineTrivia(out LexerToken token)
    {
        var status = _reader.TryAdvancePastAny(CharacterMaps.LineTerminator);
        if (status != OperationStatus.Done)
        {
            token = default;
            return status;
        }

        status = _reader.TryGetString(out var terminator);
        if (status != OperationStatus.Done)
        {
            token = default;
            return status;
        }

        token = new(
            Kind: SyntaxKind.EndOfLineTrivia,
            Start: _reader.TrackedPosition,
            End: _reader.Position)
        {
            StringValue = terminator
        };
        return OperationStatus.Done;
    }
}
