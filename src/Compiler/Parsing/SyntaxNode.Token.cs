namespace PipeDream.Compiler.Parsing.Tree;

/// <summary>
/// Defines a record containing syntactical information about an ampersand
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public sealed record AmpersandTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a close brace
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record CloseBraceTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a close
/// parenthesis token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record CloseParenthesisTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a comma token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record CommaTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a dot token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record DotTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a double
/// ampersand token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record DoubleAmpersandTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a double dot
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record DoubleDotTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a double equals
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record DoubleEqualsTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an end-of-file
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record EndOfFileTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an end-of-line
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record EndOfLineTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an equals token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record EqualsTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an exclamation
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record ExclamationTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an identifier
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
/// <param name="Name">
/// The name of this identifier.
/// </param>

public sealed record IdentifierTokenNode(TokenSpan Span, string Name)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an open brace
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record OpenBraceTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an open
/// parenthesis token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record OpenParenthesisTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// define token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorDefineTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// else-if token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorElseIfTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// else token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorElseTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// end-if token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorEndIfTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// error token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorErrorTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor if
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorIfTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// if-defined token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorIfDefTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// if-not-defined token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorIfNDefTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// include token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorIncludeTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// warn token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record PreprocessorWarnTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a semicolon
/// token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record SemicolonTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a slash token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>

public sealed record SlashTokenNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a string token.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
/// <param name="Contents">
/// The contents of the string.
/// </param>
public sealed record StringTokenNode(TokenSpan Span, string Contents)
    : SyntaxNode(Span);
