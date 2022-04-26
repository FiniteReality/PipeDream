using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct which represents a lexer
/// </summary>
public ref partial struct Lexer
{
    private readonly bool _isFinalBlock;
    private readonly ImmutableArray<ParseError>.Builder _parseErrors;
    private SequenceReader<byte> _reader;

    // Lexer state
    private LexerMode _mode;

    // Scope tracking
    private bool _beginBlock = false;
    private int _indentDepth = 0;
    private int? _indentSize = null;

    // Current token information
    private Token _lastValidToken;
    private SequencePosition _start;

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
        _parseErrors = state.ParseErrors;
        _reader = new(input);

        _mode = state.Mode;

        _beginBlock = state.BeginBlock;
        _indentDepth = state.IndentDepth;
        _indentSize = state.IndentSize;
        
        _start = input.Start;
        _lastValidToken = default;
    }

    /// <summary>
    /// Gets the current state of the lexer, to allow for resuming.
    /// </summary>
    public LexerState CurrentState
        => new(
            parseErrors: _parseErrors,

            mode: _mode,

            beginBlock: _beginBlock,
            indentDepth: _indentDepth,
            indentSize: _indentSize);

    /// <summary>
    /// Gets the current token that was successfully parsed.
    /// </summary>
    public Token CurrentToken
        => _lastValidToken;

    /// <summary>
    /// Gets whether the lexer has reached the end of the input stream.
    /// </summary>
    public bool End
        => _lastValidToken.Kind == SyntaxKind.EndOfFile;

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
    }

    private void PushIndentation()
    {
        _beginBlock = true;
    }

    private Range GetCurrentSpan()
        // TODO: this shouldn't need to cast in the first place
        => new(
            Index.FromStart(
                checked((int)_reader.Sequence.GetOffset(_start))),
            Index.FromStart(
                checked((int)_reader.Sequence.GetOffset(_reader.Position))));

    private bool Stop(string errorMessage = "Incomplete token")
    {
        if (_isFinalBlock)
        {
            _mode = LexerMode.EndOfFile;
            _parseErrors.Add(
                new ParseError(GetCurrentSpan(), errorMessage));
        }

        var startOffset = _reader.Sequence.GetOffset(_start);
        var currentOffset = _reader.Sequence.GetOffset(_reader.Position);
        _reader.Rewind(currentOffset - startOffset);

        return false;
    }

    private bool Token(SyntaxKind kind, LexerMode mode)
    {
        _mode = mode;
        _lastValidToken = new Token(kind, GetCurrentSpan());
        return true;
    }

    private bool Error(string message)
    {
        _mode = LexerMode.Error;
        _parseErrors.Add(
            new ParseError(GetCurrentSpan(), message));

        return false;
    }
}