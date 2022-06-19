namespace PipeDream.Compiler.Parsing.Tree;

internal sealed record AmpersandTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record CloseBraceTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record CloseParenthesisTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record CommaTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record DotTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record DoubleAmpersandTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record DoubleDotTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record DoubleEqualsTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record EndOfFileTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record EndOfLineTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record EqualsTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record ExclamationTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record IdentifierTokenNode(TokenSpan Span, string Name)
    : SyntaxNode(Span);

internal sealed record MultiLineCommentTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record OpenBraceTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record OpenParenthesisTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorDefineTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorElseIfTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorElseTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorEndIfTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorErrorTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorIfTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorIfDefTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorIfNDefTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorIncludeTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record PreprocessorWarnTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record SemicolonTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record SingleLineCommentTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record SlashTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record StringTokenNode(TokenSpan Span, string Contents)
    : SyntaxNode(Span);
