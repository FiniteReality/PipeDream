namespace PipeDream.Compiler.Lexing;

/// <summary>
/// Indicates the kind of token a <see cref="Token"/> is.
/// </summary>
public enum SyntaxKind
{
    /// <summary>
    /// Represents a <code>&amp;</code> token.
    /// </summary>
    Ampersand,

    /// <summary>
    /// Represents a <code>}</code> token.
    /// </summary>
    CloseBrace,

    /// <summary>
    /// Represents a <code>)</code> token.
    /// </summary>
    CloseParenthesis,

    /// <summary>
    /// Represents a <code>#define</code> token.
    /// </summary>
    PreprocessorDefine,

    /// <summary>
    /// Represents a <code>.</code> token.
    /// </summary>
    Dot,

    /// <summary>
    /// Represents a <code>&amp;&amp;</code> token.
    /// </summary>
    DoubleAmpersand,

    /// <summary>
    /// Represents a <code>..</code> token.
    /// </summary>
    DoubleDot,

    /// <summary>
    /// Represents a <code>==</code> token.
    /// </summary>
    DoubleEquals,

    /// <summary>
    /// Represents a <code>"</code> token.
    /// </summary>
    DoubleQuote,

    /// <summary>
    /// Represents an end-of-file token.
    /// </summary>
    EndOfFile,

    /// <summary>
    /// Represents an end-of-line token.
    /// </summary>
    EndOfLine,

    /// <summary>
    /// Represents a <code>=</code> token.
    /// </summary>
    Equals,

    /// <summary>
    /// Represents a <code>!</code> token.
    /// </summary>
    Exclamation,

    /// <summary>
    /// Represents a <code>#</code> token.
    /// </summary>
    Hash,

    /// <summary>
    /// Represents a <code>#elif</code> token.
    /// </summary>
    PreprocessorElseIf,

    /// <summary>
    /// Represents a <code>#else</code> token.
    /// </summary>
    PreprocessorElse,

    /// <summary>
    /// Represents a <code>#endif</code> token.
    /// </summary>
    PreprocessorEndIf,
    /// <summary>
    /// Represents a <code>#error</code> token.
    /// </summary>
    PreprocessorError,

    /// <summary>
    /// Represents a <code>#if</code> token.
    /// </summary>
    PreprocessorIf,

    /// <summary>
    /// Represents a <code>#ifdef</code> token.
    /// </summary>
    PreprocessorIfDef,

    /// <summary>
    /// Represents a <code>#ifndef</code> token.
    /// </summary>
    PreprocessorIfNDef,

    /// <summary>
    /// Represents a <code>#warn</code> token.
    /// </summary>
    PreprocessorWarn,

    /// <summary>
    /// Represents an identifier token.
    /// </summary>
    Identifier,

    /// <summary>
    /// Represents a <code>#include</code> token.
    /// </summary>
    PreprocessorInclude,

    /// <summary>
    /// Represents a <code>{</code> token.
    /// </summary>
    OpenBrace,

    /// <summary>
    /// Represents a <code>(</code> token.
    /// </summary>
    OpenParenthesis,

    /// <summary>
    /// Represents a <code>/</code> token.
    /// </summary>
    Slash,

    /// <summary>
    /// Represents a string token.
    /// </summary>
    String,

    /// <summary>
    /// Represents a multi-line comment token.
    /// </summary>
    MultiLineComment,

    /// <summary>
    /// Represents a single-line comment token.
    /// </summary>
    SingleLineComment,
}