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
        Stack<SyntaxNode?> syntaxStack,
        Stack<Rule> ruleStack)
    {
        ParseErrors = parseErrors;
        SyntaxStack = syntaxStack;
        RuleStack = ruleStack;
    }

    /// <summary>
    /// Creates an initial parser state.
    /// </summary>
    public ParserState()
    {
        ParseErrors = ImmutableArray.CreateBuilder<ParseError>(1);
        SyntaxStack = new();
        RuleStack = new();

        RuleStack.Push(Rule.CompilationUnit);
    }

    internal ImmutableArray<ParseError>.Builder ParseErrors { get; }
    internal Stack<SyntaxNode?> SyntaxStack { get; }
    internal Stack<Rule> RuleStack { get; }

    /// <summary>
    /// Gets all of the errors which occured during parsing.
    /// </summary>
    /// <remarks>
    /// This method clears the parsing error collection; if you wish to check
    /// for errors, prefer <see cref="HasErrors"/> instead.
    /// </remarks>
    public ImmutableArray<ParseError> Errors
        => ParseErrors.MoveToImmutable();

    /// <summary>
    /// Gets whether any lexing errors have occured.
    /// </summary>
    public bool HasErrors
        => ParseErrors.Count > 0;

    /// <summary>
    /// wheeee
    /// </summary>
    public void DebugPrintParseTree()
    {
        foreach (var item in SyntaxStack.Reverse())
        {
            PrintTreeItem(item);
        }

        static void PrintTreeItem(SyntaxNode? node)
        {
            Debug.WriteLine($"{node?.GetType().Name} ({node?.Span})");

            Debug.Indent();
            switch (node)
            {
                case StatementNode statement:
                    PrintTreeItem(statement.Node);
                    break;
                case BinaryExpressionNode path:
                    PrintTreeItem(path.Left);
                    PrintTreeItem(path.Right);
                    break;
            }
            Debug.Unindent();
        }
    }
}
