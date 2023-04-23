using System.Collections.Immutable;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct used for saving lexer state between multiple invocations.
/// </summary>
public readonly struct LexerState
{
    private readonly SyntaxToken? _current;
    private readonly ImmutableArray<Diagnostic>.Builder? _diagnostics;
    private readonly LexerMode _mode;

    internal ImmutableArray<Diagnostic>.Builder? Diagnostics => _diagnostics;
    internal SyntaxToken? Current => _current;
    internal LexerMode Mode => _mode;

    /// <summary>
    /// Initializes a default <see cref="LexerState" />
    /// </summary>
    public LexerState()
    {
        _diagnostics = default;
        _current = default;
        _mode = LexerMode.Normal;
    }

    internal LexerState(
        ImmutableArray<Diagnostic>.Builder diagnostics,
        SyntaxToken? current,
        LexerMode mode)
    {
        _diagnostics = diagnostics;
        _current = current;
        _mode = mode;
    }
}
