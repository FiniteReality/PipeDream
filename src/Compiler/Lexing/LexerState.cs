using System.Collections.Immutable;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct used for saving lexer state between multiple invocations.
/// </summary>
internal readonly struct LexerState
{
    private readonly SyntaxToken? _current;
    private readonly LexerMode _mode;

    internal SyntaxToken? Current => _current;
    internal LexerMode Mode => _mode;

    /// <summary>
    /// Initializes a default <see cref="LexerState" />
    /// </summary>
    public LexerState()
    {
        _current = default;
        _mode = LexerMode.Normal;
    }

    internal LexerState(
        SyntaxToken? current,
        LexerMode mode)
    {
        _current = current;
        _mode = mode;
    }
}
