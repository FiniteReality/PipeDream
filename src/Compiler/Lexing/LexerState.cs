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
/// Defines a struct used to store the state of a <see cref="Lexer"/>.
/// </summary>
public struct LexerState
{
    internal LexerState(
        bool beginBlock,
        int indentDepth,
        int? indentSize,
        int lineNumber,
        long lineStartOffset,
        LexerMode mode,
        ImmutableArray<LexerError>.Builder parseErrors)
    {
        BeginBlock = beginBlock;
        IndentDepth = indentDepth;
        IndentSize = indentSize;
        LineNumber = lineNumber;
        LineStartOffset = lineStartOffset;
        Mode = mode;
        LexErrors = parseErrors;
    }

    /// <summary>
    /// Creates an initial lexer state
    /// </summary>
    public LexerState()
    {
        BeginBlock = false;
        IndentDepth = 0;
        IndentSize = null;
        LineNumber = 1;
        LineStartOffset = 0;
        Mode = LexerMode.EndOfLine;
        LexErrors = ImmutableArray.CreateBuilder<LexerError>(1);
    }

    internal bool BeginBlock { get; }
    internal int IndentDepth { get; }
    internal int? IndentSize { get; }
    internal int LineNumber { get; }
    internal long LineStartOffset { get; }
    internal LexerMode Mode { get; }
    internal ImmutableArray<LexerError>.Builder LexErrors { get; }

    /// <summary>
    /// Gets all of the errors which occured during lexing.
    /// </summary>
    /// <remarks>
    /// This method clears the lexing error collection; if you wish to check for
    /// errors, prefer <see cref="HasErrors"/> instead.
    /// </remarks>
    public ImmutableArray<LexerError> Errors
        => LexErrors.MoveToImmutable();

    /// <summary>
    /// Gets whether any lexing errors have occured.
    /// </summary>
    public bool HasErrors
        => LexErrors.Count > 0;
}