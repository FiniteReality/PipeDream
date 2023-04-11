using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct used for lexing Dream Maker source code into syntax tokens.
/// </summary>
public ref partial struct Lexer
{
    // State shared between multiple instances
    private readonly ImmutableArray<Diagnostic>.Builder _diagnostics;

    private Reader _reader;

    private SequencePosition _tokenBeginning;
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
        Current = state.Current;
    }

    /// <summary>
    /// Gets the last valid position of the lexer.
    /// </summary>
    public SequencePosition Position => _reader.Position;

    /// <summary>
    /// Gets the current successfully parsed node.
    /// </summary>
    public SyntaxNode? Current { get; private set; }

    /// <summary>
    /// Gets the current state of the lexer, for resuming after slicing the
    /// underlying buffer.
    /// </summary>
    public LexerState State
        => new(
            diagnostics: _diagnostics,
            current: Current);

    /// <summary>
    /// Attempts to lex the next token in the stream.
    /// </summary>
    /// <returns>
    /// Returns <c>true</c> if lexing was successful.
    /// </returns>
    public bool Lex()
    {
        // If we've already produced the EOF token, we don't need to produce
        // more.
        if (_reader.IsStreamEnd &&
            Current is { Kind: SyntaxKind.EndOfFileToken })
            return false;

        BeginToken();

        SyntaxListBuilder<TriviaSyntax> leading = new();
        var status = LexTriviaList(trailing: false, ref leading);

        LexerToken token = default;
        if (status == OperationStatus.Done)
            status = LexToken(out token);
        SyntaxListBuilder<TriviaSyntax> trailing = new();
        if (status != OperationStatus.NeedMoreData)
            status = LexTriviaList(trailing: true, ref trailing);

        if (status == OperationStatus.InvalidData)
        {
            EndToken(false);
            Current = ProduceToken(token, leading.Build(), trailing.Build());
            return true;
        }
        else if (status == OperationStatus.NeedMoreData)
        {
            // This will re-lex the entire token, including trivia, which isn't
            // the best approach, but we don't have any alternative here.
            EndToken(true);
            return false;
        }
        else if (status == OperationStatus.Done)
        {
            EndToken(false);
            Current = ProduceToken(token, leading.Build(), trailing.Build());
            return true;
        }
        else
        {
            Debug.Fail("Unexpected result from LexToken");
            return false;
        }
    }

    private SyntaxNode ProduceToken(LexerToken token,
        SyntaxList<TriviaSyntax> leading,
        SyntaxList<TriviaSyntax> trailing)
        => new SyntaxToken(
            Kind: token.Kind,
            Text: token.StringValue!,
            Span: new(),
            LeadingTrivia: leading,
            TrailingTrivia: trailing);
}