using System.ComponentModel;
using System.Diagnostics;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

using static PipeDream.Compiler.Lexing.SyntaxKind;
using static PipeDream.Compiler.Parsing.ParserMode;
using static PipeDream.Compiler.Parsing.ParserRule;

namespace PipeDream.Compiler.Parsing;

public partial struct Parser
{
    private bool Decide(ParserMode state, Token lookahead)
    {
        return (state, lookahead.Kind) switch
        {
            // $ [eof]
            (Initial, EndOfFile)
                => Accept(),
            // $ [eol]
            (Initial, EndOfLine)
                => Shift(BeginLine),
            // $ ... eol [eol]
            (BeginLine, EndOfLine)
                => Shift(BeginLine),

            // $ [ident]
            (Initial, Identifier)
                => Shift(BeginStatement),
            // $ ... [ident]
            (BeginLine, Identifier)
                => Shift(BeginStatement),
            // $ ident [/]
            (BeginStatement, Slash)
                => Shift(BeginPath),
            // $ expr [=]
            (BeginStatement, SyntaxKind.Equals)
                => Shift(BeginAssignmentStatement),
            // $ expr = [ident]
            (BeginAssignmentStatement, Identifier)
                => Shift(BeginAssignmentStatementValue),
            // $ expr = ident [/]
            (BeginAssignmentStatementValue, Slash)
                => Shift(BeginPath),
            // $ expr = expr [eol]
            (BeginAssignmentStatementValue, EndOfLine)
                => Reduce(Assignment),
            // $ expr = [/]
            (BeginAssignmentStatement, Slash)
                => Shift(BeginAssignmentStatementValueRootedPath),
            // $ expr = / [ident]
            (BeginAssignmentStatementValueRootedPath, Identifier)
                => Shift(BeginRootedPath),

            // $ ident / [ident]
            (BeginPath, Identifier)
                => Shift(PathComponent),
            // $ ident / ident [/]
            (PathComponent, Slash)
                => Reduce(BinaryExpression),
            // $ ident / ident [=]
            (PathComponent, SyntaxKind.Equals)
                => Reduce(BinaryExpression),

            // $ [#define]
            // $ ... [#define]
            (Initial, PreprocessorDefine)
                => Shift(BeginPreprocessorDefine),
            (BeginLine, PreprocessorDefine)
                => Shift(BeginPreprocessorDefine),
            // $ #define [ident]
            (BeginPreprocessorDefine, Identifier)
                => Shift(PreprocessorDefineIdentifier),
            // $ #define ident [...]
            (PreprocessorDefineIdentifier, _)
                => Reduce(PreprocessorDefinition),
            // $ preprocessordefinition [eol]
            (PreprocessorDefineValue, EndOfLine)
                => Shift(BeginLine),
            // $ preprocessordefinition [...]
            (PreprocessorDefineValue, _)
                => Shift(PreprocessorDefineValueContinued),
            // $ preprocessordefinition ... [eol]
            (PreprocessorDefineValueContinued, EndOfLine)
                => Reduce(PreprocessorDefinitionValue),

            // [#endif]
            (BeginLine, PreprocessorEndIf)
                => Shift(BeginPreprocessorEndIf),
            (BeginPreprocessorEndIf, EndOfLine)
                => Shift(BeginLine),
            //=> Reduce(PreprocessorIfBlock),

            // $ [#error]
            (Initial, PreprocessorError)
                => Shift(BeginPreprocessorError),
            // $ [#error]
            (BeginLine, PreprocessorError)
                => Shift(BeginPreprocessorError),
            // $ [#if]
            // $ ... [#if]
            (Initial, PreprocessorIf)
                => Shift(BeginPreprocessorIf),
            (BeginLine, PreprocessorIf)
                => Shift(BeginPreprocessorIf),
            // #if ... [!]
            (BeginPreprocessorIf, Exclamation)
                => Shift(BeginUnaryExpression),
            // #if ... [&&]
            (BeginPreprocessorIfExpression, DoubleAmpersand)
                => Shift(BeginBinaryExpression),
            (BeginPreprocessorIfExpression, EndOfLine)
                => Reduce(PreprocessorIfStatement),

            // $ [#ifdef]
            // $ ... [#ifdef]
            (Initial, PreprocessorIfDef)
                => Shift(BeginPreprocessorIfDef),
            (BeginLine, PreprocessorIfDef)
                => Shift(BeginPreprocessorIfDef),
            (BeginPreprocessorIfDef, Identifier)
                => Shift(PreprocessorIfDefIdentifier),
            (PreprocessorIfDefIdentifier, EndOfLine)
                => Reduce(PreprocessorIfDefinition),

            // $ [#ifndef]
            (Initial, PreprocessorIfNDef)
                => Shift(BeginPreprocessorIfNDef),
            // $ [#ifndef]
            (BeginLine, PreprocessorIfNDef)
                => Shift(BeginPreprocessorIfNDef),

            // $ [#include]
            // $ ... [#include]
            (Initial, PreprocessorInclude)
                => Shift(BeginPreprocessorInclude),
            (BeginLine, PreprocessorInclude)
                => Shift(BeginPreprocessorInclude),
            (BeginPreprocessorInclude, SyntaxKind.String)
                => Shift(PreprocessorIncludeFile),
            (PreprocessorIncludeFile, EndOfLine)
                => Reduce(PreprocessorInclusion),


            // $ [#warn]
            (Initial, PreprocessorWarn)
                => Shift(BeginPreprocessorWarn),
            // $ [#warn]
            (BeginLine, PreprocessorWarn)
                => Shift(BeginPreprocessorWarn),

            // $ [/]
            (Initial, Slash)
                => Shift(BeginRootedPath),
            // $ / [ident]
            (BeginRootedPath, Identifier)
                => Shift(EndRootedPath),
            // $ / ident [/]
            (EndRootedPath, Slash)
                => Reduce(RootPath),

            // ! [ident]
            (BeginUnaryExpression, Identifier)
                => Shift(BeginUnaryExpressionIdentifier),
            // ! ident [(]
            (BeginUnaryExpressionIdentifier, OpenParenthesis)
                => Shift(BeginFunctionCall),
            // ! funccall [&&]
            (BeginUnaryExpression, DoubleAmpersand)
                => Reduce(UnaryExpression),
            (BeginUnaryExpression, EndOfLine)
                => Reduce(UnaryExpression),

            // ... && [!]
            (BeginBinaryExpression, Exclamation)
                => Shift(BeginUnaryExpression),
            (BeginBinaryExpression, EndOfLine)
                => Reduce(BinaryExpression),

            // ident ( [)]
            (BeginFunctionCall, CloseParenthesis)
                => Shift(EndFunctionCall),
            // ident ( ... ) [&&]
            (EndFunctionCall, DoubleAmpersand)
                => Reduce(FunctionCall),
            // ident ( ... ) [&&]
            (EndFunctionCall, EndOfLine)
                => Reduce(FunctionCall),
            // ident ( [ident]
            (BeginFunctionCall, Identifier)
                => Shift(BeginFunctionCallParameter),
            // ident ( expr [)]
            (BeginFunctionCallParameter, CloseParenthesis)
                => Shift(EndFunctionCall),

            _ => Error(ParseError.Unexpected(lookahead))
        };
    }
}
