using System.Buffers;
using PipeDream.Compiler.Syntax;

using static PipeDream.Compiler.Syntax.SyntaxKind;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
{
    private OperationStatus LexPunctuation(out LexerToken token)
    {
        // We're converting up to int here to allow us to use -1 as a sentinel
        // value. This makes the switch expression later on cleaner.
        static int Peek(ref Reader reader, int index)
            => reader.TryPeek(index, out var c) ? c : -1;

        (OperationStatus, SyntaxKind) x = (
            Peek(ref _reader, 0),
            Peek(ref _reader, 1),
            Peek(ref _reader, 2)) switch
        {
            (-1, _, _) => (OperationStatus.NeedMoreData, default),

            ('!', -1, _) => (OperationStatus.NeedMoreData, default),
            ('!', '=', _) => (default, ExclamationEqualsToken),
            ('!', _, _) => (default, ExclamationToken),

            ('#', _, _) => (default, HashToken),

            ('%', -1, _) => (OperationStatus.NeedMoreData, default),
            ('%', '=', _) => (default, PercentEqualsToken),
            ('%', '%', -1) => (OperationStatus.NeedMoreData, default),
            ('%', '%', '=') => (default, PercentPercentEqualsToken),
            ('%', '%', _) => (default, PercentPercentToken),
            ('%', _, _) => (default, PercentToken),

            ('&', -1, _) => (OperationStatus.NeedMoreData, default),
            ('&', '=', _) => (default, AmpersandEqualsToken),
            ('&', '&', -1) => (OperationStatus.NeedMoreData, default),
            ('&', '&', '=') => (default, AmpersandAmpersandEqualsToken),
            ('&', '&', _) => (default, AmpersandAmpersandToken),
            ('&', _, _) => (default, AmpersandToken),

            ('(', _, _) => (default, OpenParenthesisToken),
            (')', _, _) => (default, CloseParenthesisToken),

            ('*', -1, _) => (OperationStatus.NeedMoreData, default),
            ('*', '=', _) => (default, AsteriskEqualsToken),
            ('*', '*', _) => (default, AsteriskAsteriskToken),
            ('*', _, _) => (default, AsteriskToken),

            ('+', -1, _) => (OperationStatus.NeedMoreData, default),
            ('+', '=', _) => (default, PlusEqualsToken),
            ('+', '+', _) => (default, PlusPlusToken),
            ('+', _, _) => (default, PlusToken),

            (',', _, _) => (default, CommaToken),

            ('-', -1, _) => (OperationStatus.NeedMoreData, default),
            ('-', '=', _) => (default, MinusEqualsToken),
            ('-', '-', _) => (default, MinusMinusToken),
            ('-', _, _) => (default, MinusToken),

            ('.', -1, _) => (OperationStatus.NeedMoreData, default),
            ('.', '.', _) => (default, DotDotToken),
            ('.', _, _) => (default, DotToken),

            ('/', -1, _) => (OperationStatus.NeedMoreData, default),
            ('/', '=', _) => (default, SlashEqualsToken),
            ('/', _, _) => (default, SlashToken),

            (':', -1, _) => (OperationStatus.NeedMoreData, default),
            (':', '=', _) => (default, ColonEqualsToken),
            (':', ':', _) => (default, ColonColonToken),
            (':', _, _) => (default, ColonToken),

            ('<', -1, _) => (OperationStatus.NeedMoreData, default),
            ('<', '=', _) => (default, LessThanEqualsToken),
            ('<', '>', _) => (default, LessThanGreaterThanToken),
            ('<', '<', -1) => (OperationStatus.NeedMoreData, default),
            ('<', '<', '=') => (default, LessThanLessThanEqualsToken),
            ('<', '<', _) => (default, LessThanLessThanToken),
            ('<', _, _) => (default, LessThanToken),

            ('=', -1, _) => (OperationStatus.NeedMoreData, default),
            ('=', '=', _) => (default, EqualsEqualsToken),
            ('=', _, _) => (default, EqualsToken),

            ('>', -1, _) => (OperationStatus.NeedMoreData, default),
            ('>', '=', _) => (default, GreaterThanEqualsToken),
            ('>', '>', -1) => (OperationStatus.NeedMoreData, default),
            ('>', '>', '=') => (default, GreaterThanGreaterThanEqualsToken),
            ('>', '>', _) => (default, GreaterThanGreaterThanToken),
            ('>', _, _) => (default, GreaterThanToken),

            ('?', -1, _) => (OperationStatus.NeedMoreData, default),
            ('?', '.', _) => (default, QuestionDotToken),
            ('?', ':', _) => (default, QuestionColonToken),
            ('?', _, _) => (default, QuestionToken),

            ('[', _, _) => (default, OpenBracketToken),
            ('\\', _, _) => (default, BackslashToken),
            (']', _, _) => (default, CloseBracketToken),

            ('^', -1, _) => (OperationStatus.NeedMoreData, default),
            ('^', '=', _) => (default, CaretEqualsToken),
            ('^', _, _) => (default, CaretToken),

            ('{', _, _) => (default, OpenBraceToken),
            ('}', _, _) => (default, CloseBraceToken),

            ('~', -1, _) => (OperationStatus.NeedMoreData, default),
            ('~', '=', _) => (default, TildeEqualsToken),
            ('~', '!', _) => (default, TildeExclamationToken),
            ('~', _, _) => (default, TildeToken),

            ('"', -1, _) => (OperationStatus.NeedMoreData, default),
            ('"', '}', _) when (_mode & LexerMode.Interpolated) != 0
                => (default, InterpolatedVerbatimStringEndToken),
            ('"', '}', _) when (_mode & LexerMode.Interpolated) == 0
                => (default, RawVerbatimStringEndToken),
            ('"', >= 0, _) when (_mode & LexerMode.String) != 0
                && (_mode & LexerMode.Interpolated) != 0
                => (default, InterpolatedStringEndToken),
            ('"', >= 0, _) when (_mode & LexerMode.String) != 0
                && (_mode & LexerMode.Interpolated) == 0
                => (default, RawStringEndToken),
            ('"', >= 0, _) when (_mode & LexerMode.String) == 0
                => (default, InterpolatedStringStartToken),

            ('@', -1, _) => (OperationStatus.NeedMoreData, default),
            ('@', '{', -1) => (OperationStatus.NeedMoreData, default),
            ('@', '{', '"') => (default, RawVerbatimStringStartToken),
            ('@', '"', _) => (default, RawStringStartToken),

            _ => (OperationStatus.InvalidData, default)
        };

        var (status, kind) = x;

        if (status != OperationStatus.Done)
        {
            token = default;
            return status;
        }

        status = _reader.TryAdvance(kind switch
        {
            ExclamationToken or HashToken or PercentToken or AsteriskToken or
            PlusToken or MinusToken or DotToken or SlashToken or ColonToken or
            LessThanToken or GreaterThanToken or QuestionToken or CaretToken or
            OpenBraceToken or CloseBraceToken or TildeToken or EqualsToken or
            CommaToken or OpenBracketToken or CloseBracketToken or
            BackslashToken or OpenParenthesisToken or CloseParenthesisToken or
            InterpolatedStringStartToken or InterpolatedStringEndToken or
            RawStringEndToken
                => 1,

            ExclamationEqualsToken or PercentEqualsToken or
            PercentPercentToken or AmpersandEqualsToken or
            AmpersandAmpersandToken or AsteriskEqualsToken or
            AsteriskAsteriskToken or PlusEqualsToken or PlusPlusToken or
            MinusEqualsToken or MinusMinusToken or SlashEqualsToken or
            ColonEqualsToken or ColonColonToken or LessThanEqualsToken or
            LessThanLessThanToken or GreaterThanGreaterThanToken or
            QuestionDotToken or QuestionColonToken or CaretEqualsToken or
            TildeEqualsToken or TildeExclamationToken or DotDotToken or
            RawStringStartToken or RawVerbatimStringEndToken or
            InterpolatedVerbatimStringEndToken
                => 2,

            PercentPercentEqualsToken or AmpersandAmpersandEqualsToken or
            LessThanLessThanEqualsToken or GreaterThanGreaterThanEqualsToken or
            RawVerbatimStringStartToken
                => 3,

            var fail => throw new InvalidOperationException($"Oops! {fail}")
        });

        if (status != OperationStatus.Done)
        {
            token = default;
            return status;
        }

        status = _reader.TryGetString(out var punctuation);
        if (status != OperationStatus.Done)
        {
            token = default;
            return status;
        }

        switch (kind)
        {
            case CloseBracketToken when (_mode & LexerMode.String) != 0:
                _mode &= ~LexerMode.Normal;
                break;

            case InterpolatedVerbatimStringStartToken:
                _mode |= LexerMode.Verbatim;
                _mode |= LexerMode.Interpolated;
                _mode |= LexerMode.String;
                _mode &= ~LexerMode.Normal;
                break;

            case InterpolatedStringStartToken:
                _mode |= LexerMode.Interpolated;
                _mode |= LexerMode.String;
                _mode &= ~LexerMode.Normal;
                break;

            case RawVerbatimStringStartToken:
                _mode |= LexerMode.Verbatim;
                _mode |= LexerMode.String;
                _mode &= ~LexerMode.Normal;
                break;

            case RawStringStartToken:
                _mode |= LexerMode.String;
                _mode &= ~LexerMode.Normal;
                break;

            case InterpolatedStringEndToken:
            case InterpolatedVerbatimStringEndToken:
            case RawStringEndToken:
            case RawVerbatimStringEndToken:
                _mode &= ~LexerMode.Verbatim;
                _mode &= ~LexerMode.Interpolated;
                _mode &= ~LexerMode.String;
                break;
        }

        token = new(
            Kind: kind,
            Start: _reader.TrackedPosition,
            End: _reader.Position)
        {
            StringValue = punctuation
        };
        return OperationStatus.Done;
    }
}
