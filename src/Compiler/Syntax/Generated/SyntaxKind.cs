namespace PipeDream.Compiler.Syntax;


/// <summary>
/// Defines an enum containing all possible syntax kinds.
/// </summary>
public enum SyntaxKind
{
    // Group: Block
    /// <summary>
    /// An entire compilation unit.
    /// </summary>
    /// <seealso cref="CompilationUnitSyntax" />
    CompilationUnit,

    // Group: CompoundPunctuation
    /// <summary>
    /// Represents the <c>&amp;&amp;=</c> token.
    /// </summary>
    AmpersandAmpersandEqualsToken,

    /// <summary>
    /// Represents the <c>&amp;&amp;</c> token.
    /// </summary>
    AmpersandAmpersandToken,

    /// <summary>
    /// Represents the <c>&amp;=</c> token.
    /// </summary>
    AmpersandEqualsToken,

    /// <summary>
    /// Represents the <c>**</c> token.
    /// </summary>
    AsteriskAsteriskToken,

    /// <summary>
    /// Represents the <c>*=</c> token.
    /// </summary>
    AsteriskEqualsToken,

    /// <summary>
    /// Represents the <c>||=</c> token.
    /// </summary>
    BarBarEqualsToken,

    /// <summary>
    /// Represents the <c>||</c> token.
    /// </summary>
    BarBarToken,

    /// <summary>
    /// Represents the <c>|=</c> token.
    /// </summary>
    BarEqualsToken,

    /// <summary>
    /// Represents the <c>^=</c> token.
    /// </summary>
    CaretEqualsToken,

    /// <summary>
    /// Represents the <c>::</c> token.
    /// </summary>
    ColonColonToken,

    /// <summary>
    /// Represents the <c>:=</c> token.
    /// </summary>
    ColonEqualsToken,

    /// <summary>
    /// Represents the <c>.</c> token.
    /// </summary>
    DotDotToken,

    /// <summary>
    /// Represents the <c>==</c> token.
    /// </summary>
    EqualsEqualsToken,

    /// <summary>
    /// Represents the <c>!=</c> token.
    /// </summary>
    ExclamationEqualsToken,

    /// <summary>
    /// Represents the <c>&gt;=</c> token.
    /// </summary>
    GreaterThanEqualsToken,

    /// <summary>
    /// Represents the <c>&gt;&gt;=</c> token.
    /// </summary>
    GreaterThanGreaterThanEqualsToken,

    /// <summary>
    /// Represents the <c>&gt;&gt;</c> token.
    /// </summary>
    GreaterThanGreaterThanToken,

    /// <summary>
    /// Represents the <c>&lt;=</c> token.
    /// </summary>
    LessThanEqualsToken,

    /// <summary>
    /// Represents the <c>&lt;&gt;</c> token.
    /// </summary>
    LessThanGreaterThanToken,

    /// <summary>
    /// Represents the <c>&lt;&lt;=</c> token.
    /// </summary>
    LessThanLessThanEqualsToken,

    /// <summary>
    /// Represents the <c>&lt;&lt;</c> token.
    /// </summary>
    LessThanLessThanToken,

    /// <summary>
    /// Represents the <c>-=</c> token.
    /// </summary>
    MinusEqualsToken,

    /// <summary>
    /// Represents the <c>--</c> token.
    /// </summary>
    MinusMinusToken,

    /// <summary>
    /// Represents the <c>%=</c> token.
    /// </summary>
    PercentEqualsToken,

    /// <summary>
    /// Represents the <c>%%=</c> token.
    /// </summary>
    PercentPercentEqualsToken,

    /// <summary>
    /// Represents the <c>%%</c> token.
    /// </summary>
    PercentPercentToken,

    /// <summary>
    /// Represents the <c>+=</c> token.
    /// </summary>
    PlusEqualsToken,

    /// <summary>
    /// Represents the <c>++</c> token.
    /// </summary>
    PlusPlusToken,

    /// <summary>
    /// Represents the <c>?:</c> token.
    /// </summary>
    QuestionColonToken,

    /// <summary>
    /// Represents the <c>?.</c> token.
    /// </summary>
    QuestionDotToken,

    /// <summary>
    /// Represents the <c>/=</c> token.
    /// </summary>
    SlashEqualsToken,

    /// <summary>
    /// Represents the <c>~=</c> token.
    /// </summary>
    TildeEqualsToken,

    /// <summary>
    /// Represents the <c>~!</c> token.
    /// </summary>
    TildeExclamationToken,

    // Group: ContextualKeyword
    /// <summary>
    /// Represents the <c>Area</c> keyword.
    /// </summary>
    AreaKeyword,

    /// <summary>
    /// Represents the <c>Atom</c> keyword.
    /// </summary>
    AtomKeyword,

    /// <summary>
    /// Represents the <c>Client</c> keyword.
    /// </summary>
    ClientKeyword,

    /// <summary>
    /// Represents the <c>const</c> keyword.
    /// </summary>
    ConstKeyword,

    /// <summary>
    /// Represents the <c>Database</c> keyword.
    /// </summary>
    DatabaseKeyword,

    /// <summary>
    /// Represents the <c>Datum</c> keyword.
    /// </summary>
    DatumKeyword,

    /// <summary>
    /// Represents the <c>final</c> keyword.
    /// </summary>
    FinalKeyword,

    /// <summary>
    /// Represents the <c>global</c> keyword.
    /// </summary>
    GlobalKeyword,

    /// <summary>
    /// Represents the <c>Icon</c> keyword.
    /// </summary>
    IconKeyword,

    /// <summary>
    /// Represents the <c>Image</c> keyword.
    /// </summary>
    ImageKeyword,

    /// <summary>
    /// Represents the <c>List</c> keyword.
    /// </summary>
    ListKeyword,

    /// <summary>
    /// Represents the <c>Matrix</c> keyword.
    /// </summary>
    MatrixKeyword,

    /// <summary>
    /// Represents the <c>Mob</c> keyword.
    /// </summary>
    MobKeyword,

    /// <summary>
    /// Represents the <c>MutableAppearance</c> keyword.
    /// </summary>
    MutableAppearanceKeyword,

    /// <summary>
    /// Represents the <c>Obj</c> keyword.
    /// </summary>
    ObjKeyword,

    /// <summary>
    /// Represents the <c>operator</c> keyword.
    /// </summary>
    OperatorKeyword,

    /// <summary>
    /// Represents the <c>Proc</c> keyword.
    /// </summary>
    ProcKeyword,

    /// <summary>
    /// Represents the <c>Regex</c> keyword.
    /// </summary>
    RegexKeyword,

    /// <summary>
    /// Represents the <c>Savefile</c> keyword.
    /// </summary>
    SavefileKeyword,

    /// <summary>
    /// Represents the <c>Sound</c> keyword.
    /// </summary>
    SoundKeyword,

    /// <summary>
    /// Represents the <c>Text</c> keyword.
    /// </summary>
    TextKeyword,

    /// <summary>
    /// Represents the <c>tmp</c> keyword.
    /// </summary>
    TmpKeyword,

    /// <summary>
    /// Represents the <c>Turf</c> keyword.
    /// </summary>
    TurfKeyword,

    /// <summary>
    /// Represents the <c>verb</c> keyword.
    /// </summary>
    VerbKeyword,

    /// <summary>
    /// Represents the <c>World</c> keyword.
    /// </summary>
    WorldKeyword,

    // Group: Expression
    /// <summary>
    /// An addition <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    AddExpression,

    /// <summary>
    /// An address-of <see cref="PrefixUnaryExpressionSyntax" />.
    /// </summary>
    AddressOfExpression,

    /// <summary>
    /// A bitwise and <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    BitwiseAndExpression,

    /// <summary>
    /// A bitwise not <see cref="PrefixUnaryExpressionSyntax" />.
    /// </summary>
    BitwiseNotExpression,

    /// <summary>
    /// A bitwise or <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    BitwiseOrExpression,

    /// <summary>
    /// A ternary <see cref="ConditionalExpressionSyntax" />.
    /// </summary>
    ConditionalExpression,

    /// <summary>
    /// A dereference <see cref="PrefixUnaryExpressionSyntax" />.
    /// </summary>
    DereferenceExpression,

    /// <summary>
    /// A divide <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    DivideExpression,

    /// <summary>
    /// An equality <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    EqualsExpression,

    /// <summary>
    /// An equivalency <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    EquivalentExpression,

    /// <summary>
    /// An exclusive or <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    ExclusiveOrExpression,

    /// <summary>
    /// An exponentiation <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    ExponentiationExpression,

    /// <summary>
    /// A float modulo <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    FloatModuloExpression,

    /// <summary>
    /// A greater-than <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    GreaterThanExpression,

    /// <summary>
    /// A greater-than-or-equal <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    GreaterThanOrEqualExpression,

    /// <summary>
    /// A <c>in</c><see cref="BinaryExpressionSyntax" />.
    /// </summary>
    InExpression,

    /// <summary>
    /// An integer modulo <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    IntegerModuloExpression,

    /// <summary>
    /// A left-shift <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    LeftShiftExpression,

    /// <summary>
    /// A less-than <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    LessThanExpression,

    /// <summary>
    /// A less-than-or-equal <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    LessThanOrEqualExpression,

    /// <summary>
    /// A logical and <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    LogicalAndExpression,

    /// <summary>
    /// A logical not <see cref="PrefixUnaryExpressionSyntax" />.
    /// </summary>
    LogicalNotExpression,

    /// <summary>
    /// A logical or <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    LogicalOrExpression,

    /// <summary>
    /// A multiply <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    MultiplyExpression,

    /// <summary>
    /// An inequality <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    NotEqualsExpression,

    /// <summary>
    /// An inequivalency <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    NotEquivalentExpression,

    /// <summary>
    /// A post-decrement <see cref="PostfixUnaryExpressionSyntax" />.
    /// </summary>
    PostDecrementExpression,

    /// <summary>
    /// A post-increment <see cref="PostfixUnaryExpressionSyntax" />.
    /// </summary>
    PostIncrementExpression,

    /// <summary>
    /// A pre-decrement <see cref="PrefixUnaryExpressionSyntax" />.
    /// </summary>
    PreDecrementExpression,

    /// <summary>
    /// A pre-increment <see cref="PrefixUnaryExpressionSyntax" />.
    /// </summary>
    PreIncrementExpression,

    /// <summary>
    /// A right-shift <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    RightShiftExpression,

    /// <summary>
    /// A subtraction <see cref="BinaryExpressionSyntax" />.
    /// </summary>
    SubtractExpression,

    /// <summary>
    /// A unary minus <see cref="PrefixUnaryExpressionSyntax" />.
    /// </summary>
    UnaryMinusExpression,

    // Group: File
    /// <summary>
    /// The end of a file.
    /// </summary>
    EndOfFileToken,

    // Group: Invalid
    /// <summary>
    /// Represents an invalid token.
    /// </summary>
    BadToken,

    // Group: Keyword
    /// <summary>
    /// Represents the <c>as</c> keyword.
    /// </summary>
    AsKeyword,

    /// <summary>
    /// Represents the <c>break</c> keyword.
    /// </summary>
    BreakKeyword,

    /// <summary>
    /// Represents the <c>call</c> keyword.
    /// </summary>
    CallKeyword,

    /// <summary>
    /// Represents the <c>catch</c> keyword.
    /// </summary>
    CatchKeyword,

    /// <summary>
    /// Represents the <c>continue</c> keyword.
    /// </summary>
    ContinueKeyword,

    /// <summary>
    /// Represents the <c>del</c> keyword.
    /// </summary>
    DelKeyword,

    /// <summary>
    /// Represents the <c>do</c> keyword.
    /// </summary>
    DoKeyword,

    /// <summary>
    /// Represents the <c>else</c> keyword.
    /// </summary>
    ElseKeyword,

    /// <summary>
    /// Represents the <c>for</c> keyword.
    /// </summary>
    ForKeyword,

    /// <summary>
    /// Represents the <c>goto</c> keyword.
    /// </summary>
    GotoKeyword,

    /// <summary>
    /// Represents the <c>if</c> keyword.
    /// </summary>
    IfKeyword,

    /// <summary>
    /// Represents the <c>in</c> keyword.
    /// </summary>
    InKeyword,

    /// <summary>
    /// Represents the <c>new</c> keyword.
    /// </summary>
    NewKeyword,

    /// <summary>
    /// Represents the <c>null</c> keyword.
    /// </summary>
    NullKeyword,

    /// <summary>
    /// Represents the <c>return</c> keyword.
    /// </summary>
    ReturnKeyword,

    /// <summary>
    /// Represents the <c>set</c> keyword.
    /// </summary>
    SetKeyword,

    /// <summary>
    /// Represents the <c>sleep</c> keyword.
    /// </summary>
    SleepKeyword,

    /// <summary>
    /// Represents the <c>spawn</c> keyword.
    /// </summary>
    SpawnKeyword,

    /// <summary>
    /// Represents the <c>step</c> keyword.
    /// </summary>
    StepKeyword,

    /// <summary>
    /// Represents the <c>switch</c> keyword.
    /// </summary>
    SwitchKeyword,

    /// <summary>
    /// Represents the <c>throw</c> keyword.
    /// </summary>
    ThrowKeyword,

    /// <summary>
    /// Represents the <c>to</c> keyword.
    /// </summary>
    ToKeyword,

    /// <summary>
    /// Represents the <c>try</c> keyword.
    /// </summary>
    TryKeyword,

    /// <summary>
    /// Represents the <c>var</c> keyword.
    /// </summary>
    VarKeyword,

    /// <summary>
    /// Represents the <c>while</c> keyword.
    /// </summary>
    WhileKeyword,

    // Group: Numeric
    /// <summary>
    /// Represents a number in source code.
    /// </summary>
    NumberToken,

    // Group: PreprocessorKeyword
    /// <summary>
    /// Represents the <c>define</c> keyword.
    /// </summary>
    DefineKeyword,

    /// <summary>
    /// Represents the <c>elif</c> keyword.
    /// </summary>
    ElifKeyword,

    /// <summary>
    /// Represents the <c>endif</c> keyword.
    /// </summary>
    EndIfKeyword,

    /// <summary>
    /// Represents the <c>error</c> keyword.
    /// </summary>
    ErrorKeyword,

    /// <summary>
    /// Represents the <c>ifdef</c> keyword.
    /// </summary>
    IfDefKeyword,

    /// <summary>
    /// Represents the <c>ifndef</c> keyword.
    /// </summary>
    IfNDefKeyword,

    /// <summary>
    /// Represents the <c>include</c> keyword.
    /// </summary>
    IncludeKeyword,

    /// <summary>
    /// Represents the <c>pipedream</c> keyword.
    /// </summary>
    PipeDreamKeyword,

    /// <summary>
    /// Represents the <c>pragma</c> keyword.
    /// </summary>
    PragmaKeyword,

    /// <summary>
    /// Represents the <c>undef</c> keyword.
    /// </summary>
    UndefKeyword,

    /// <summary>
    /// Represents the <c>warn</c> keyword.
    /// </summary>
    WarnKeyword,

    // Group: Punctuation
    /// <summary>
    /// Represents the <c>&amp;</c> token.
    /// </summary>
    AmpersandToken,

    /// <summary>
    /// Represents the <c>*</c> token.
    /// </summary>
    AsteriskToken,

    /// <summary>
    /// Represents the <c>\</c> token.
    /// </summary>
    BackslashToken,

    /// <summary>
    /// Represents the <c>|</c> token.
    /// </summary>
    BarToken,

    /// <summary>
    /// Represents the <c>^</c> token.
    /// </summary>
    CaretToken,

    /// <summary>
    /// Represents the <c>}</c> token.
    /// </summary>
    CloseBraceToken,

    /// <summary>
    /// Represents the <c>]</c> token.
    /// </summary>
    CloseBracketToken,

    /// <summary>
    /// Represents the <c>)</c> token.
    /// </summary>
    CloseParenthesisToken,

    /// <summary>
    /// Represents the <c>:</c> token.
    /// </summary>
    ColonToken,

    /// <summary>
    /// Represents the <c>,</c> token.
    /// </summary>
    CommaToken,

    /// <summary>
    /// Represents the <c>.</c> token.
    /// </summary>
    DotToken,

    /// <summary>
    /// Represents the <c>=</c> token.
    /// </summary>
    EqualsToken,

    /// <summary>
    /// Represents the <c>!</c> token.
    /// </summary>
    ExclamationToken,

    /// <summary>
    /// Represents the <c>&gt;</c> token.
    /// </summary>
    GreaterThanToken,

    /// <summary>
    /// Represents the <c>#</c> token.
    /// </summary>
    HashToken,

    /// <summary>
    /// Represents the <c>&lt;</c> token.
    /// </summary>
    LessThanToken,

    /// <summary>
    /// Represents the <c>-</c> token.
    /// </summary>
    MinusToken,

    /// <summary>
    /// Represents the <c>{</c> token.
    /// </summary>
    OpenBraceToken,

    /// <summary>
    /// Represents the <c>[</c> token.
    /// </summary>
    OpenBracketToken,

    /// <summary>
    /// Represents the <c>(</c> token.
    /// </summary>
    OpenParenthesisToken,

    /// <summary>
    /// Represents the <c>%</c> token.
    /// </summary>
    PercentToken,

    /// <summary>
    /// Represents the <c>+</c> token.
    /// </summary>
    PlusToken,

    /// <summary>
    /// Represents the <c>?</c> token.
    /// </summary>
    QuestionToken,

    /// <summary>
    /// Represents the <c>/</c> token.
    /// </summary>
    SlashToken,

    /// <summary>
    /// Represents the <c>~</c> token.
    /// </summary>
    TildeToken,

    // Group: Statement
    /// <summary>
    /// A <see cref="BlockSyntax" /></summary>
    Block,

    // Group: String
    /// <summary>
    /// Represents the <c>"</c> token at the end of a string.
    /// </summary>
    InterpolatedStringEndToken,

    /// <summary>
    /// Represents the <c>"</c> token at the beginning of a string.
    /// </summary>
    InterpolatedStringStartToken,

    /// <summary>
    /// Represents the <c>"}</c> token at the end of a string.
    /// </summary>
    InterpolatedVerbatimStringEndToken,

    /// <summary>
    /// Represents the <c>{"</c> token at the beginning of a string.
    /// </summary>
    InterpolatedVerbatimStringStartToken,

    /// <summary>
    /// Represents the <c>"</c> token at the end of a string.
    /// </summary>
    RawStringEndToken,

    /// <summary>
    /// Represents the <c>@"</c> token at the beginning of a string.
    /// </summary>
    RawStringStartToken,

    /// <summary>
    /// Represents the <c>"}</c> token at the end of a string.
    /// </summary>
    RawVerbatimStringEndToken,

    /// <summary>
    /// Represents the <c>@{"</c> token at the beginning of a string.
    /// </summary>
    RawVerbatimStringStartToken,

    /// <summary>
    /// Represents text within a string.
    /// </summary>
    StringTextToken,

    // Group: Textual
    /// <summary>
    /// Represents an identifier in source code.
    /// </summary>
    IdentifierToken,

    // Group: Trivia
    /// <summary>
    /// An end of line in the syntax tree.
    /// </summary>
    EndOfLineTrivia,

    /// <summary>
    /// A single line comment in the syntax tree.
    /// </summary>
    SingleLineCommentTrivia,

    /// <summary>
    /// A whitespace in the syntax tree.
    /// </summary>
    WhitespaceTrivia,
}

internal enum SyntaxGroup : byte
{
    Block,

    CompoundPunctuation,

    ContextualKeyword,

    Expression,

    File,

    Invalid,

    Keyword,

    Numeric,

    PreprocessorKeyword,

    Punctuation,

    Statement,

    String,

    Textual,

    Trivia,
}

internal static class SyntaxKindExtensions
{
    public static SyntaxGroup GetGroup(this SyntaxKind kind)
        => (int)kind switch
        {
            < 1 => SyntaxGroup.Block,
            < 34 => SyntaxGroup.CompoundPunctuation,
            < 59 => SyntaxGroup.ContextualKeyword,
            < 92 => SyntaxGroup.Expression,
            < 93 => SyntaxGroup.File,
            < 94 => SyntaxGroup.Invalid,
            < 119 => SyntaxGroup.Keyword,
            < 120 => SyntaxGroup.Numeric,
            < 131 => SyntaxGroup.PreprocessorKeyword,
            < 156 => SyntaxGroup.Punctuation,
            < 157 => SyntaxGroup.Statement,
            < 166 => SyntaxGroup.String,
            < 167 => SyntaxGroup.Textual,
            < 170 => SyntaxGroup.Trivia,
            _ => throw new InvalidOperationException("Unreachable")
        };
}
