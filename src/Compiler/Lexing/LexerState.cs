using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Defines a struct used for saving lexer state between multiple invocations.
/// </summary>
public readonly struct LexerState
{
    private readonly ImmutableArray<Diagnostic>.Builder? _diagnostics;
    private readonly SyntaxNode? _current;

    internal ImmutableArray<Diagnostic>.Builder? Diagnostics => _diagnostics;
    internal SyntaxNode? Current => _current;

    /// <summary>
    /// Initializes a default <see cref="LexerState" />
    /// </summary>
    public LexerState()
    {
        _diagnostics = default;
    }

    internal LexerState(
        ImmutableArray<Diagnostic>.Builder diagnostics,
        SyntaxNode? current)
    {
        _diagnostics = diagnostics;
        _current = current;
    }
}