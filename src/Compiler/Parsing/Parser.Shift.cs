using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Internal;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Parsing;

public partial struct Parser
{
    private bool Shift(ParserMode mode)
    {
        if (!_tokens.TryPeek(out var token))
            return false;

        if (token.Kind == SyntaxKind.EndOfFile)
        {
            _parseErrors.Add(
                new ParseError(token.Span,
                    "Expected token, got EOF"));
            return false;
        }

        _ = _tokens.TryDequeue(out _);

        SyntaxNode? node = token.Kind switch
        {
            SyntaxKind.Ampersand
                => new AmpersandTokenNode(token.Span),
            SyntaxKind.CloseBrace
                => new CloseBraceTokenNode(token.Span),
            SyntaxKind.CloseParenthesis
                => new CloseParenthesisTokenNode(token.Span),
            SyntaxKind.Dot
                => new DotTokenNode(token.Span),
            SyntaxKind.DoubleAmpersand
                => new DoubleAmpersandTokenNode(token.Span),
            SyntaxKind.DoubleDot
                => new DoubleDotTokenNode(token.Span),
            SyntaxKind.DoubleEquals
                => new DoubleEqualsTokenNode(token.Span),
            SyntaxKind.EndOfFile
                => new EndOfFileTokenNode(token.Span),
            SyntaxKind.EndOfLine
                => new EndOfLineTokenNode(token.Span),
            SyntaxKind.Equals
                => new EqualsTokenNode(token.Span),
            SyntaxKind.Exclamation
                => new ExclamationTokenNode(token.Span),
            SyntaxKind.Identifier
                => new IdentifierTokenNode(token.Span, (string)token.Value!),
            SyntaxKind.MultiLineComment
                => new MultiLineCommentTokenNode(token.Span),
            SyntaxKind.OpenBrace
                => new OpenBraceTokenNode(token.Span),
            SyntaxKind.OpenParenthesis
                => new OpenParenthesisTokenNode(token.Span),
            SyntaxKind.PreprocessorDefine
                => new PreprocessorDefineTokenNode(token.Span),
            SyntaxKind.PreprocessorElseIf
                => new PreprocessorElseIfTokenNode(token.Span),
            SyntaxKind.PreprocessorElse
                => new PreprocessorElseTokenNode(token.Span),
            SyntaxKind.PreprocessorEndIf
                => new PreprocessorEndIfTokenNode(token.Span),
            SyntaxKind.PreprocessorError
                => new PreprocessorErrorTokenNode(token.Span),
            SyntaxKind.PreprocessorIf
                => new PreprocessorIfTokenNode(token.Span),
            SyntaxKind.PreprocessorIfDef
                => new PreprocessorIfDefTokenNode(token.Span),
            SyntaxKind.PreprocessorIfNDef
                => new PreprocessorIfNDefTokenNode(token.Span),
            SyntaxKind.PreprocessorInclude
                => new PreprocessorIncludeTokenNode(token.Span),
            SyntaxKind.PreprocessorWarn
                => new PreprocessorWarnTokenNode(token.Span),
            SyntaxKind.SingleLineComment
                => new SingleLineCommentTokenNode(token.Span),
            SyntaxKind.Slash
                => new SlashTokenNode(token.Span),
            SyntaxKind.String
                => new StringTokenNode(token.Span, (string)token.Value!),
            _ => throw new InvalidOperationException("Unknown syntax node")
        };

        if (node != null)
            _syntaxStack.Push((mode, node));

        return true;
    }
}