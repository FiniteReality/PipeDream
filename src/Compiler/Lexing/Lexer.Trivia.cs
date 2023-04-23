using System.Buffers;
using System.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
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

    private readonly TriviaSyntax ProduceTrivia(LexerToken token)
        => new SimpleTriviaSyntax(
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
            case (byte)' ':
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

        // TODO: multi-line comments
        token = default;
        return OperationStatus.InvalidData;
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