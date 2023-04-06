using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct used for lexing Dream Maker source code into syntax tokens.
/// </summary>
public ref partial struct Lexer
{
    private readonly bool _isFinalBlock;
    private readonly ImmutableArray<LexerError>.Builder _lexErrors;
    private SequenceReader<byte> _reader;

    // Lexer state
    private LexerMode _mode;

    // Scope tracking
    private bool _beginBlock = false;
    private int _indentDepth = 0;
    private int? _indentSize = null;

    // Line tracking
    private int _lineNumber;
    private long _lineStartOffset;

    // Current token information
    private SyntaxToken? _lastValidToken;
    private SequencePosition _start;
    private TokenPosition _tokenStart;

    /// <summary>
    /// Creates a lexer for the given block of input data.
    /// </summary>
    /// <param name="input">
    /// The input text to parse, in UTF-8.
    /// </param>
    /// <param name="isFinalBlock">
    /// <code>true</code> if no more data is expected to be read.
    /// </param>
    /// <param name="state">
    /// The state to initialise the lexer in.
    /// </param>
    public Lexer(ReadOnlySequence<byte> input, bool isFinalBlock,
        LexerState state)
    {
        _isFinalBlock = isFinalBlock;
        _lexErrors = state.LexErrors;
        _reader = new(input);

        _mode = state.Mode;

        _beginBlock = state.BeginBlock;
        _indentDepth = state.IndentDepth;
        _indentSize = state.IndentSize;

        _lineNumber = state.LineNumber;
        _lineStartOffset = state.LineStartOffset;

        _start = default;
        _lastValidToken = default;
        _tokenStart = default;
    }

    /// <summary>
    /// Gets the current state of the lexer, to allow for resuming.
    /// </summary>
    public LexerState CurrentState
        => new(
            lexErrors: _lexErrors,

            mode: _mode,

            beginBlock: _beginBlock,
            indentDepth: _indentDepth,
            indentSize: _indentSize,

            lineNumber: _lineNumber,
            lineStartOffset: _lineStartOffset);

    /// <summary>
    /// Gets the current token that was successfully parsed.
    /// </summary>
    public SyntaxToken? CurrentToken
        => _lastValidToken;

    /// <summary>
    /// Gets the current read position.
    /// </summary>
    public SequencePosition Position
        => _start;

    private long ReadWhitespace()
    {
        return _reader.AdvancePastAny(
            (byte)' ', (byte)'\t');
    }

    private void Start()
    {
        _start = _reader.Position;
        var offset = _reader.Sequence.GetOffset(_reader.Position);
        _tokenStart = new(_lineNumber,
            unchecked((int)(offset - _lineStartOffset)),
            offset);
    }

    private void PushIndentation()
    {
        _beginBlock = true;
    }

    private static ReadOnlySpan<byte> CarriageReturnLineFeed
        => new byte[]
        {
            (byte)'\r', (byte)'\n'
        };
    private int CountLines(ReadOnlySequence<byte> block)
    {
        if (block.IsSingleSegment)
            return CountLinesFast(block.FirstSpan);
        else if (block.Length < 256)
        {
            Span<byte> buffer = (stackalloc byte[256])[
                ..unchecked((int)block.Length)];

            block.CopyTo(buffer);

            return CountLinesFast(buffer);
        }
        else
        {
            var count = 0;
            var start = block.Start;
            while (block.TryGet(ref start, out var buffer, true))
                count += CountLinesFast(buffer.Span);

            return count;
        }

        static int CountLinesFast(ReadOnlySpan<byte> span)
        {
            if (span.IsEmpty)
                return 0;

            int count = 0;
            while (true)
            {
                var crlf = span.IndexOf(CarriageReturnLineFeed);
                var lf = span.IndexOf((byte)'\n');

                var next = (crlf, lf) switch
                {
                    // prefer crlf to lf if possible as crlf contains lf
                    ( >= 0, >= 0) => crlf < lf ? crlf : lf,
                    ( >= 0, < 0) => crlf,
                    ( < 0, >= 0) => lf,
                    ( < 0, < 0) => -1
                };

                if (next < 0)
                    return count;

                count++;
                span = span[(next + 1)..];
            }
        }
    }

    private TokenSpan GetCurrentSpan()
    {
        var offset = _reader.Sequence.GetOffset(_reader.Position);
        var end = new TokenPosition(_lineNumber,
            unchecked((int)(offset - _lineStartOffset)),
            offset);

        return new(_tokenStart, end);
    }

    private bool Stop(string errorMessage = "Incomplete token")
    {
        if (_isFinalBlock)
        {
            _mode = LexerMode.EndOfFile;
            _lexErrors.Add(
                new LexerError(GetCurrentSpan(), errorMessage));
        }

        var startOffset = _reader.Sequence.GetOffset(_start);
        var currentOffset = _reader.Sequence.GetOffset(_reader.Position);
        if ((currentOffset - startOffset) > 0)
            _reader.Rewind(currentOffset - startOffset);

        return false;
    }

    private bool Token(SyntaxKind kind, LexerMode mode)
    {
        _mode = mode;
        Debug.Assert(kind.GetGroup() is
            SyntaxGroup.Punctuation or
            SyntaxGroup.CompoundPunctuation);
        _lastValidToken = new SyntaxToken(kind, default, default, default);
        return true;
    }

    private bool Token<T>(SyntaxKind kind, LexerMode mode, T value)
    {
        _mode = mode;
        Debug.Assert(kind.GetGroup() is
            SyntaxGroup.Punctuation or
            SyntaxGroup.CompoundPunctuation);
        _lastValidToken = new SyntaxToken(kind, default, default, default);
        return true;
    }

    private bool Error(string message)
    {
        _mode = LexerMode.Error;
        _lexErrors.Add(
            new LexerError(GetCurrentSpan(), message));

        return false;
    }
}
