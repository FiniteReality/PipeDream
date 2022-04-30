using System.ComponentModel;
using System.Diagnostics;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

using static PipeDream.Compiler.Parsing.ParserMode;

namespace PipeDream.Compiler.Parsing;

public partial struct Parser
{
    private bool Decide(ParserMode state, Token lookahead)
    {
        return (state, lookahead.Kind) switch
        {
            (Initial, SyntaxKind.EndOfFile)
                => Reduce(ParserRule.CompilationUnit),
            (Initial, SyntaxKind.EndOfLine)
                => Shift(BeginLine),
            (Initial, SyntaxKind.SingleLineComment)
                => Shift(EmptyLine),
            (Initial, SyntaxKind.MultiLineComment)
                => Shift(BeginLine),

            (BeginLine, SyntaxKind.EndOfLine)
                => Shift(BeginLine),
            (BeginLine, SyntaxKind.SingleLineComment)
                => Shift(EmptyLine),
            (BeginLine, SyntaxKind.MultiLineComment)
                => Shift(BeginLine),

            // singlelinecomment [eol]
            (EmptyLine, SyntaxKind.EndOfLine)
                => Shift(CommentLine),

            // singlelinecomment eol [eol]
            (CommentLine, SyntaxKind.EndOfLine)
                => Reduce(ParserRule.Comment),
            // singlelinecomment eol [singlelinecomment]
            (CommentLine, SyntaxKind.SingleLineComment)
                => Reduce(ParserRule.Comment),
            // singlelinecomment eol [ident]
            (CommentLine, SyntaxKind.Identifier)
                => Reduce(ParserRule.Comment),
            // singlelinecomment eol [#define]
            (CommentLine, SyntaxKind.PreprocessorDefine)
                => Reduce(ParserRule.Comment),

            // [ident]
            (BeginLine, SyntaxKind.Identifier)
                => Shift(BeginExpression),

            // ident [/]
            (BeginExpression, SyntaxKind.Slash)
                => Shift(BeginCompoundExpression),
            // ident / [ident]
            (BeginCompoundExpression, SyntaxKind.Identifier)
                => Shift(EndExpression),
            // ident / ident [/]
            (EndExpression, SyntaxKind.Slash)
                => Reduce(ParserRule.Expression),
            (EndExpression, SyntaxKind.Equals)
                => Reduce(ParserRule.Expression),

            // expr [/]
            (BeginStatement, SyntaxKind.Slash)
                => Shift(BeginCompoundExpression),

            // expr [=]
            (BeginStatement, SyntaxKind.Equals)
                => Shift(BeginAssignmentStatement),

            // expr = [ident]
            (BeginAssignmentStatement, SyntaxKind.Identifier)
                => Shift(EndAssignmentStatement),
            // expr = ident [/]
            (EndAssignmentStatement, SyntaxKind.Slash)
                => Shift(BeginCompoundExpression),
            // expr = ident [eol]
            // expr = expr [eol]
            (EndAssignmentStatement, SyntaxKind.EndOfLine)
                => Reduce(ParserRule.AssignmentStatement),
            // assignment [eol]
            (EndStatement, SyntaxKind.EndOfLine)
                => Shift(EndStatementLine),
            // assignment eol [eol]
            (EndStatementLine, SyntaxKind.EndOfLine)
                => Reduce(ParserRule.Statement),
            // assignment eol [ident]
            (EndStatementLine, SyntaxKind.Identifier)
                => Reduce(ParserRule.Statement),
            // assignment eol [#define]
            (EndStatementLine, SyntaxKind.PreprocessorDefine)
                => Reduce(ParserRule.Statement),
            (EndStatementLine, SyntaxKind.SingleLineComment)
                => Reduce(ParserRule.Statement),

            // [/]
            (BeginLine, SyntaxKind.Slash)
                => Shift(BeginPathExpression),
            // / [ident]
            (BeginPathExpression, SyntaxKind.Identifier)
                => Reduce(ParserRule.PathRoot),
            // root [ident]
            (RootedPath, SyntaxKind.Identifier)
                => Shift(RootedPathLeaf),
            // root ident [/]
            (RootedPathLeaf, SyntaxKind.Slash)
                => Reduce(ParserRule.Path),


            _ => Error(ParseError.Unexpected(lookahead))
        };
    }
}