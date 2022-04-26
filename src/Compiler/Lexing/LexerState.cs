using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;

namespace PipeDream.Compiler.Lexing;

internal enum LexerMode
{
    StartOfLine,
    MiddleOfLine,
    EndOfLine,
    EndOfFile,
    Error,
}

/// <summary>
/// Defines a struct used to store the lexer's state.
/// </summary>
public struct LexerState
{
    internal LexerState(
        bool beginBlock,
        int indentDepth,
        int? indentSize,
        LexerMode mode,
        ImmutableArray<ParseError>.Builder parseErrors)
    {
        BeginBlock = beginBlock;
        IndentDepth = indentDepth;
        IndentSize = indentSize;
        Mode = mode;
        ParseErrors = parseErrors;
    }

    /// <summary>
    /// Creates an initial lexer state
    /// </summary>
    public LexerState()
    {
        BeginBlock = false;
        IndentDepth = 0;
        IndentSize = null;
        Mode = LexerMode.EndOfLine;
        ParseErrors = ImmutableArray.CreateBuilder<ParseError>(1);
    }

    internal bool BeginBlock { get; }
    internal int IndentDepth { get; }
    internal int? IndentSize { get; }
    internal LexerMode Mode { get; }
    internal ImmutableArray<ParseError>.Builder ParseErrors { get; }

    /// <summary>
    /// Gets all of the errors which occured during parsing.
    /// </summary>
    /// <remarks>
    /// This method clears the parse error collection; if you wish to check for
    /// errors, prefer <see cref="HasErrors"/> instead.
    /// </remarks>
    public ImmutableArray<ParseError> Errors
        => ParseErrors.MoveToImmutable();

    /// <summary>
    /// Gets whether any parse errors have occured.
    /// </summary>
    public bool HasErrors
        => ParseErrors.Count > 0;
}