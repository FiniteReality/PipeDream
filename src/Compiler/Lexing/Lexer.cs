using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct used for lexing Dream Maker source code into syntax tokens.
/// </summary>
internal ref partial struct Lexer
{
    // State shared between multiple instances
    private readonly ImmutableArray<Diagnostic>.Builder _diagnostics;

    // Exists for the duration of this instance
    private Reader _reader;
    private LexerMode _mode;

    // Exists for the duration of Lex()
    private SequencePosition _tokenBeginning;
    private LexerMode _initialMode;
    /// <summary>
    /// Creates a lexer for the given block of input data.
    /// </summary>
    /// <param name="state">
    /// The previous state to resume lexing from.
    /// </param>
    /// <param name="sequence">
    /// The input text to parse.
    /// </param>
    /// <param name="isFinalBlock">
    /// <code>true</code> if no more data is expected to be read.
    /// </param>
    public Lexer(LexerState state, ReadOnlySequence<byte> sequence,
        bool isFinalBlock)
    {
        _reader = new(sequence, isFinalBlock);

        _diagnostics = state.Diagnostics
            ?? ImmutableArray.CreateBuilder<Diagnostic>();
        _mode = state.Mode;
        Current = state.Current;

        // In case the mode was somehow initialized to all zeros, put us in a
        // sane state.
        if (_mode == LexerMode.Invalid)
            _mode = LexerMode.Normal;
    }

    /// <summary>
    /// Gets the last valid position of the lexer.
    /// </summary>
    public SequencePosition Position => _reader.Position;

    /// <summary>
    /// Gets the current successfully parsed node.
    /// </summary>
    public SyntaxToken? Current { get; private set; }

    /// <summary>
    /// Gets the current state of the lexer, for resuming after slicing the
    /// underlying buffer.
    /// </summary>
    public LexerState State
        => new(
            diagnostics: _diagnostics,
            current: Current,
            mode: _mode);

    /// <summary>
    /// Attempts to lex the next token in the stream.
    /// </summary>
    /// <returns>
    /// Returns <c>true</c> if lexing was successful.
    /// </returns>
    public OperationStatus Lex()
    {
        // If we're at the end of the stream, that means the last call must
        // have read right to the end.
        if (_reader.IsStreamEnd)
        {
            // If we've already produced the EOF token, we don't need to
            // produce more.
            if (Current is { Kind: SyntaxKind.EndOfFileToken })
                return OperationStatus.InvalidData;

            Current = ProduceToken(
                token: new(
                    Kind: SyntaxKind.EndOfFileToken,
                    Start: _reader.Position,
                    End: _reader.Position)
                {
                    StringValue = ""
                },
                leading: new(),
                trailing: new());
            return OperationStatus.Done;
        }

        return LexCore();
    }

    private OperationStatus LexCore()
    {
        BeginToken();
        SyntaxListBuilder<TriviaSyntax> leading = default;
        SyntaxListBuilder<TriviaSyntax> trailing = default;
        var status = OperationStatus.Done;
        if ((_mode & LexerMode.Normal) != 0)
            status = LexTriviaList(trailing: false, ref leading);

        LexerToken token = default;
        if (status == OperationStatus.Done)
        {
            if ((_mode & LexerMode.Normal) != 0)
                status = LexToken(out token);
            else if ((_mode & LexerMode.String) != 0)
                status = LexStringText(out token);
            else
                Debug.Fail($"Unknown mode {_mode}");
        }

        if ((_mode & LexerMode.Normal) != 0
            && status != OperationStatus.NeedMoreData)
            status = LexTriviaList(trailing: true, ref trailing);

        if (status == OperationStatus.InvalidData)
        {
            EndToken(false);
            Current = ProduceToken(token, leading.Build(), trailing.Build());
            return OperationStatus.InvalidData;
        }
        else if (status == OperationStatus.NeedMoreData && _reader.IsStreamEnd)
        {
            // The file is either empty, or consists of only trivia.
            Current = ProduceToken(new(
                Kind: SyntaxKind.EndOfFileToken,
                Start: _reader.Position,
                End: _reader.Position
            ), leading.Build(), trailing.Build());
            return OperationStatus.Done;
        }
        else if (status == OperationStatus.NeedMoreData)
        {
            // This will re-lex the entire token, including trivia, which isn't
            // the best approach, but we don't have any alternative here.
            EndToken(true);
            return OperationStatus.NeedMoreData;
        }
        else if (status == OperationStatus.Done)
        {
            EndToken(false);
            Current = ProduceToken(token, leading.Build(), trailing.Build());
            return OperationStatus.Done;
        }
        else
        {
            Debug.Fail("Unexpected result from LexToken");
            return OperationStatus.InvalidData;
        }
    }

    private static SyntaxToken ProduceToken(LexerToken token,
        SyntaxList<TriviaSyntax> leading,
        SyntaxList<TriviaSyntax> trailing)
        => new(
            Kind: token.Kind,
            Text: token.StringValue!,
            Span: new(),
            LeadingTrivia: leading,
            TrailingTrivia: trailing);
}
