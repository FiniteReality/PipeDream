using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

using static PipeDream.Compiler.Parsing.ParserMode;
using static PipeDream.Compiler.Parsing.ParserRule;

namespace PipeDream.Compiler.Parsing;

public partial struct Parser
{
    private bool Reduce(ParserRule rule)
    {
        switch (rule)
        {
            case CompilationUnit:
            {
                var builder = ImmutableArray.CreateBuilder<SyntaxNode>(
                    _syntaxStack.Count - 1);

                while (_syntaxStack.TryPeek(out var state))
                {
                    if (state.mode == Initial)
                        break;

                    var (mode, node) = state;
                    builder.Add(node);

                    _ = _syntaxStack.Pop();
                }

                _syntaxStack.Push((Final,
                    new CompilationUnitNode(builder.MoveToImmutable())));
                return true;
            }
            case Comment:
            {
                var (_, eol) = _syntaxStack.Pop();
                var (_, comment) = _syntaxStack.Pop();

                var nextMode = _syntaxStack.Peek().mode switch
                {
                    Initial => BeginLine,
                    BeginLine => BeginLine,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                _syntaxStack.Push((nextMode,
                    new CommentNode(comment, eol)));
                return true;
            }
            case Statement:
            {
                var (_, eol) = _syntaxStack.Pop();
                var (_, stmt) = _syntaxStack.Pop();

                var nextMode = _syntaxStack.Peek().mode switch
                {
                    BeginLine => BeginLine,
                    _ => throw new InvalidOperationException("Unreachable?")
                };

                _syntaxStack.Push((nextMode,
                    new StatementNode(stmt, eol)));
                return true;
            }
            case AssignmentStatement:
            {
                var (_, right) = _syntaxStack.Pop();
                var (_, equals) = _syntaxStack.Pop();
                var (_, left) = _syntaxStack.Pop();

                var kind = equals switch
                {
                    EqualsTokenNode => SyntaxKind.Equals,
                    _ => throw new InvalidOperationException("Unreachable?")
                };

                var nextMode = _syntaxStack.Peek().mode switch
                {
                    BeginLine => EndStatement,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                _syntaxStack.Push((nextMode,
                    new AssignmentStatementNode(left, right)));
                return true;
            }
            case Expression:
            {
                var (_, right) = _syntaxStack.Pop();
                var (_, type) = _syntaxStack.Pop();
                var (_, left) = _syntaxStack.Pop();

                var kind = type switch
                {
                    SlashTokenNode => SyntaxKind.Slash,
                    _ => throw new InvalidOperationException("Unreachable?")
                };

                var nextMode = _syntaxStack.Peek().mode switch
                {
                    BeginLine => BeginStatement,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                _syntaxStack.Push((nextMode,
                    new BinaryExpressionNode(left, right, kind)));
                return true;
            }
            case PathRoot:
            {
                var (_, node) = _syntaxStack.Pop();
                Debug.Assert(node is SlashTokenNode);

                var nextMode = (_syntaxStack.Peek().mode, rule) switch
                {
                    (BeginLine, Path) => RootedPath,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                _syntaxStack.Push((nextMode, new RootPathNode(node)));
                return true;
            }
            case Path:
            {
                var (_, right) = _syntaxStack.Pop();
                var (_, left) = _syntaxStack.Pop();

                var nextMode = _syntaxStack.Peek().mode switch
                {
                    RootedPath => EndExpression,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                _syntaxStack.Push((nextMode,
                    new BinaryExpressionNode(left, right, SyntaxKind.Slash)));
                return true;
            }
        }

        // Pop however many symbols TRule needs
        // Peek top symbol for state
        // Push new symbol based on (symbol, rule)
        return false;
    }
}