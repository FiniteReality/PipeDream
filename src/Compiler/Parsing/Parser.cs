using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Parsing;

/// <summary>
/// Defines a struct used for parsing Dream Maker source code.
/// </summary>
public partial struct Parser
{
    private readonly ImmutableArray<ParseError>.Builder _parseErrors;
    private readonly Stack<(int, SyntaxNode?)> _syntaxStack;
    private readonly Queue<Token> _tokens;

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
    /// Gets whether the parser is in an accept state.
    /// </summary>
    public bool IsAccept => _accept;

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
    {
        if (_tokens.Count == 128)
            return false;

        _tokens.Enqueue(token);
        return true;
    }


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

            _ = _syntaxStack.TryPeek(out var state);

            if (!Decide(state, lookahead))
                break;
        }

        return _accept || _parseErrors.Count == 0;
    }

    private bool Error(ParseError error)
    {
        _parseErrors.Add(error);
        return false;
    }

    private bool Accept()
    {
        _accept = true;
        return true;
    }

    private bool Shift(int state)
    {
        var token = _tokens.Dequeue();
        _syntaxStack.Push((state, token.Kind switch
        {
            SyntaxKind.Ampersand
                => new AmpersandTokenNode(token.Span),
            SyntaxKind.CloseBrace
                => new CloseBraceTokenNode(token.Span),
            SyntaxKind.CloseParenthesis
                => new CloseParenthesisTokenNode(token.Span),
            SyntaxKind.Dot
                => new DotTokenNode(token.Span),
            SyntaxKind.DoubleAmpersand
                => new DoubleAmpersandTokenNode(token.Span),
            SyntaxKind.DoubleDot
                => new DoubleDotTokenNode(token.Span),
            SyntaxKind.DoubleEquals
                => new DoubleEqualsTokenNode(token.Span),
            SyntaxKind.EndOfFile
                => new EndOfFileTokenNode(token.Span),
            SyntaxKind.EndOfLine
                => new EndOfLineTokenNode(token.Span),
            SyntaxKind.Equals
                => new EqualsTokenNode(token.Span),
            SyntaxKind.Exclamation
                => new ExclamationTokenNode(token.Span),
            SyntaxKind.Identifier
                => new IdentifierTokenNode(token.Span, (string)token.Value!),
            SyntaxKind.OpenBrace
                => new OpenBraceTokenNode(token.Span),
            SyntaxKind.OpenParenthesis
                => new OpenParenthesisTokenNode(token.Span),
            SyntaxKind.PreprocessorDefine
                => new PreprocessorDefineTokenNode(token.Span),
            SyntaxKind.PreprocessorElseIf
                => new PreprocessorElseIfTokenNode(token.Span),
            SyntaxKind.PreprocessorElse
                => new PreprocessorElseTokenNode(token.Span),
            SyntaxKind.PreprocessorEndIf
                => new PreprocessorEndIfTokenNode(token.Span),
            SyntaxKind.PreprocessorError
                => new PreprocessorErrorTokenNode(token.Span),
            SyntaxKind.PreprocessorIf
                => new PreprocessorIfTokenNode(token.Span),
            SyntaxKind.PreprocessorIfDef
                => new PreprocessorIfDefTokenNode(token.Span),
            SyntaxKind.PreprocessorIfNDef
                => new PreprocessorIfNDefTokenNode(token.Span),
            SyntaxKind.PreprocessorInclude
                => new PreprocessorIncludeTokenNode(token.Span),
            SyntaxKind.PreprocessorWarn
                => new PreprocessorWarnTokenNode(token.Span),
            SyntaxKind.Slash
                => new SlashTokenNode(token.Span),
            SyntaxKind.String
                => new StringTokenNode(token.Span, (string)token.Value!),
            _ => throw new InvalidOperationException("Unexpected syntax kind")
        }));

        return true;
    }

    private SyntaxNode? PopNode()
        => _syntaxStack.Pop().Item2;

    private bool Reduce(SyntaxNode node, Func<int, int> selector)
    {
        var (state, _) = _syntaxStack.Peek();
        _syntaxStack.Push((selector(state), node));

        return true;
    }
}
