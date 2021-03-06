using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Parsing;

/// <summary>
/// Defines a struct used to store the state of a <see cref="Parser"/>.
/// </summary>
public struct ParserState
{
    internal ParserState(
        ImmutableArray<ParseError>.Builder parseErrors,
        Stack<(int, SyntaxNode?)> syntaxStack)
    {
        ParseErrors = parseErrors;
        SyntaxStack = syntaxStack;
    }

    /// <summary>
    /// Creates an initial parser state.
    /// </summary>
    public ParserState()
    {
        ParseErrors = ImmutableArray.CreateBuilder<ParseError>(1);
        SyntaxStack = new();
        SyntaxStack.Push((0, null));
    }

    internal ImmutableArray<ParseError>.Builder ParseErrors { get; }
    internal Stack<(int, SyntaxNode?)> SyntaxStack { get; }

    /// <summary>
    /// Gets the most recent parse tree from the parser.
    /// </summary>
    public SyntaxNode? ParseTree
        => SyntaxStack.TryPeek(out var state)
            ? state.Item2
            : null;

    /// <summary>
    /// Gets the compilation unit, if parsing completed successfully.
    /// Otherwise, <c>null</c>.
    /// </summary>
    public CompilationUnitNode? CompilationUnit
        => ParseTree as CompilationUnitNode;

    /// <summary>
    /// Gets all of the errors which occured during parsing.
    /// </summary>
    /// <remarks>
    /// This method clears the parsing error collection; if you wish to check
    /// for errors, prefer <see cref="HasErrors"/> instead.
    /// </remarks>
    public ImmutableArray<ParseError> Errors
        => ParseErrors.Capacity == ParseErrors.Count
            ? ParseErrors.MoveToImmutable()
            : ParseErrors.ToImmutable();

    /// <summary>
    /// Gets whether any lexing errors have occured.
    /// </summary>
    public bool HasErrors
        => ParseErrors.Count > 0;
}
