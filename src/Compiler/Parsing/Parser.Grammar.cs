using System.Linq.Expressions;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Parsing;

public partial struct Parser
{
    private bool ParseCompilationUnit(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            // [eof] -> $accept
            (null, SyntaxKind.EndOfFile)
                => Accept(),
            // [(anything)] -> statement_list
            (null, _) => Push(Rule.StatementList),

            (_, _) => Error(ParseError.Unexpected(lookahead))
        };
    }

    private bool ParseBlock(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            (_, _) => Error(ParseError.Unexpected(lookahead))
        };
    }

    private bool ParseStatementList(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            // statement_list [eof]
            (StatementListNode, SyntaxKind.EndOfFile) => Pop(),
            // statement_list [eol]
            (StatementListNode, SyntaxKind.EndOfLine) => Pop(),

            // statement [eof] -> statement_list
            (StatementNode, SyntaxKind.EndOfFile)
                => Reduce(new StatementListNode(current, null)),
            // statement [eol] -> statement_list
            (StatementNode, SyntaxKind.EndOfLine)
                => Reduce(new StatementListNode(current, null)),

            // (anything) [(anything)]
            (_, _) => Push(Rule.Statement),
        };
    }

    private bool ParseStatement(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            // statement [eof]
            (StatementNode, SyntaxKind.EndOfFile)
                => Pop(),
            // statement [eol]
            (StatementNode, SyntaxKind.EndOfLine)
                => Pop(),

            // (anything) [eof]
            (not null, SyntaxKind.EndOfFile)
                => Reduce(new StatementNode(current!)),
            // (anything) [eol]
            (not null, SyntaxKind.EndOfLine)
                => Reduce(new StatementNode(current!)),

            // (anything) [ident]
            (_, SyntaxKind.Identifier)
                => Push(Rule.Expression),

            // expr [=]
            (ExpressionNode, SyntaxKind.Equals)
                => Push(Rule.Assignment),

            (_, _) => Error(ParseError.Unexpected(lookahead))
        };
    }

    private bool ParseAssignment(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            (_, _) => Error(ParseError.Unexpected(lookahead))
        };
    }

    private bool ParseExpression(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            // (anything) [ident]
            (_, SyntaxKind.Identifier)
                => Shift(new IdentifierTokenNode(
                    lookahead.Span, (string)lookahead.Value!)),

            // ident [/]
            (IdentifierTokenNode, SyntaxKind.Slash)
                => Push(Rule.BinaryExpression),

            // bin_expr
            /*(BinaryExpressionNode, SyntaxKind.Slash)
                => Reduce(new Exp)*/

            (_, _) => Error(ParseError.Unexpected(lookahead))
        };
    }

    private bool ParseBinaryExpression(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            // ident
            //(_, SyntaxKind.Slash)

            (_, _) => Error(ParseError.Unexpected(lookahead))
        };

        /*static bool ReduceSlash(ref Parser parser)
        {
            var right = parser._syntaxStack.Pop(); // right
            var left = parser._syntaxStack.Pop(); // left

            parser._syntaxStack.Push(new BinaryExpressionNode(left!, right!));
            return true;
        }*/
    }

    private bool ParseString(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            (_, _) => Error(ParseError.Unexpected(lookahead))
        };
    }

    private bool ParseIdentifier(SyntaxNode? current, Token lookahead)
    {
        return (current, lookahead.Kind) switch
        {
            (_, _) => Error(ParseError.Unexpected(lookahead))
        };
    }
}
