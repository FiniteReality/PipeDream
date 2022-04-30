using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Internal;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Parsing;

/// <summary>
/// Defines a struct used for parsing Dream Maker source code.
/// </summary>
public partial struct Parser
{
    private readonly ImmutableArray<ParseError>.Builder _parseErrors;
    private readonly Stack<(ParserMode mode, SyntaxNode node)> _syntaxStack;
    private readonly CircularBuffer<Token> _tokens;

    private bool _accept;

    /// <summary>
    /// Creates a parser for the given parser state.
    /// </summary>
    /// <param name="state">
    /// The current parser state.
    /// </param>
    public Parser(ParserState state)
    {
        _parseErrors = state.ParseErrors;
        _syntaxStack = state.SyntaxStack;

        _tokens = new(128);
        _accept = false;
    }

    /// <summary>
    /// Gets the current state of the parser, to allow for resuming.
    /// </summary>
    public ParserState CurrentState
        => new(
            parseErrors: _parseErrors,
            syntaxStack: _syntaxStack
        );

    /// <summary>
    /// Attempts to store the token in the processing queue.
    /// </summary>
    /// <param name="token">
    /// The token to store.
    /// </param>
    /// <returns>
    /// <code>true</code> if adding the token was successful.
    /// </returns>
    public bool Queue(Token token)
        => _tokens.Enqueue(token);


    /// <summary>
    /// Attempts to parse the tokens stored in the processing queue.
    /// </summary>
    /// <returns>
    /// <code>true</code> if parsing was successful.
    /// </returns>
    public bool Parse()
    {
        while (!_accept)
        {
            if (!_tokens.TryPeek(out var lookahead))
                break;

            if (!_syntaxStack.TryPeek(out var rightmost))
                return Error(new ParseError(default, "Evaluation stack empty"));

            if (!Decide(rightmost.mode, lookahead))
                return false;
        }

        return _parseErrors.Count == 0;
    }

    private bool Accept()
    {

        return true;
    }

    private bool Error(ParseError error)
    {
        _parseErrors.Add(error);
        return false;
    }
}