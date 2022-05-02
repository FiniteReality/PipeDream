using System.Collections.Immutable;
using System.Diagnostics;
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
            case Assignment:
            {
                var (_, right) = _syntaxStack.Pop();
                var (_, equals) = _syntaxStack.Pop();
                var (_, left) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    Initial => BeginLine,
                    BeginLine => BeginLine,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode,
                    new AssignmentNode(left, equals, right));
            }
            case BinaryExpression:
            {
                var (_, right) = _syntaxStack.Pop();
                var (_, op) = _syntaxStack.Pop();
                var (_, left) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    Initial => BeginStatement,
                    BeginLine => BeginStatement,
                    BeginAssignmentStatement => BeginAssignmentStatementValue,
                    BeginPreprocessorIf => BeginPreprocessorIfExpression,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode,
                    new BinaryOperationNode(left, op, right));
            }
            case FunctionCall:
            {
                var parameters = ImmutableArray.CreateBuilder<SyntaxNode>();

                var (_, closeParenthesis) = _syntaxStack.Pop();

                while (_syntaxStack.TryPeek(out var state)
                    && state.mode == BeginFunctionCallParameter)
                {
                    parameters.Add(state.node);
                    _ = _syntaxStack.Pop();
                }

                var (_, openParenthesis) = _syntaxStack.Pop();
                var (_, expr) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    BeginUnaryExpression => BeginUnaryExpression,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode, new FunctionCallNode(
                    expr, openParenthesis, parameters.ToImmutable(), closeParenthesis));
            }
            case PreprocessorDefinition:
            {
                var (_, ident) = _syntaxStack.Pop();
                var (_, define) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    Initial => PreprocessorDefineValue,
                    BeginLine => PreprocessorDefineValue,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode,
                    new PreprocessorDefinitionNode(define, ident));
            }
            case PreprocessorDefinitionValue:
            {
                var content = ImmutableArray.CreateBuilder<SyntaxNode>();

                while (_syntaxStack.TryPeek(out var state)
                    && state.mode == PreprocessorDefineValueContinued)
                {
                    content.Add(state.node);
                    _ = _syntaxStack.Pop();
                }

                var (_, definition) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    Initial => BeginLine,
                    BeginLine => BeginLine,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode,
                    new ValuedPreprocessorDefinitionNode(definition,
                        content.ToImmutable()));
            }
            case PreprocessorInclusion:
            {
                var (_, file) = _syntaxStack.Pop();
                var (_, include) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    Initial => BeginLine,
                    BeginLine => BeginLine,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode,
                    new PreprocessorInclusionNode(include, file));
            }
            case PreprocessorIfDefinition:
            {
                var (_, ident) = _syntaxStack.Pop();
                var (_, ifdef) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    Initial => BeginLine,
                    BeginLine => BeginLine,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode,
                    new PreprocessorIfDefinitionNode(ident, ifdef));
            }
            case PreprocessorIfStatement:
            {
                var (_, expr) = _syntaxStack.Pop();
                var (_, hashIf) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    Initial => BeginLine,
                    BeginLine => BeginLine,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode,
                    new PreprocessorIfNode(hashIf, expr));
            }
            case PreprocessorIfBlock:
            {
                //var (_, endif) = _syntaxStack.Pop();
                Debug.Fail("Unimplemented");
                return false;
            }
            case RootPath:
            {
                var (_, path) = _syntaxStack.Pop();
                var (_, slash) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    Initial => BeginStatement,
                    BeginAssignmentStatement => BeginAssignmentStatementValue,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode, new RootPathNode(slash, path));
            }
            case UnaryExpression:
            {
                var (_, expr) = _syntaxStack.Pop();
                var (_, op) = _syntaxStack.Pop();

                var mode = _syntaxStack.Peek().mode switch
                {
                    BeginBinaryExpression => BeginBinaryExpression,
                    BeginPreprocessorIf => BeginPreprocessorIfExpression,
                    _ => throw new InvalidOperationException("Unreachable")
                };

                return Push(mode, new UnaryOperationNode(op, expr));
            }
        }

        // Pop however many symbols TRule needs
        // Peek top symbol for state
        // Push new symbol based on (symbol, rule)
        return false;
    }
}
