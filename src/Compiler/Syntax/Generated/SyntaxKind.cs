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

    // Group: Keyword
    /// <summary>
    /// Represents the <c>in</c> keyword.
    /// </summary>
    InKeyword,

    // Group: Token
    /// <summary>
    /// Represents the <c>&amp;</c> token.
    /// </summary>
    AmpersandToken,

    /// <summary>
    /// Represents the <c>**</c> token.
    /// </summary>
    AsteriskAsteriskToken,

    /// <summary>
    /// Represents the <c>*</c> token.
    /// </summary>
    AsteriskToken,

    /// <summary>
    /// Represents the <c>|</c> token.
    /// </summary>
    BarToken,

    /// <summary>
    /// Represents the <c>^</c> token.
    /// </summary>
    CaretToken,

    /// <summary>
    /// Represents the <c>:</c> token.
    /// </summary>
    ColonToken,

    /// <summary>
    /// Represents the <c>&amp;&amp;</c> token.
    /// </summary>
    DoubleAmpersandToken,

    /// <summary>
    /// Represents the <c>||</c> token.
    /// </summary>
    DoubleBarToken,

    /// <summary>
    /// Represents the <c>==</c> token.
    /// </summary>
    DoubleEqualsToken,

    /// <summary>
    /// Represents the <c>&gt;&gt;</c> token.
    /// </summary>
    DoubleGreaterThanToken,

    /// <summary>
    /// Represents the <c>&lt;&lt;</c> token.
    /// </summary>
    DoubleLessThanToken,

    /// <summary>
    /// Represents the <c>!=</c> token.
    /// </summary>
    ExclamationEqualsToken,

    /// <summary>
    /// Represents the <c>!</c> token.
    /// </summary>
    ExclamationToken,

    /// <summary>
    /// Represents the <c>&gt;=</c> token.
    /// </summary>
    GreaterThanEqualsToken,

    /// <summary>
    /// Represents the <c>&gt;</c> token.
    /// </summary>
    GreaterThanToken,

    /// <summary>
    /// Represents the <c>&lt;=</c> token.
    /// </summary>
    LessThanEqualsToken,

    /// <summary>
    /// Represents the <c>&lt;&gt;</c> token.
    /// </summary>
    LessThanGreaterThanToken,

    /// <summary>
    /// Represents the <c>&lt;</c> token.
    /// </summary>
    LessThanToken,

    /// <summary>
    /// Represents the <c>--</c> token.
    /// </summary>
    MinusMinusToken,

    /// <summary>
    /// Represents the <c>-</c> token.
    /// </summary>
    MinusToken,

    /// <summary>
    /// Represents the <c>%%</c> token.
    /// </summary>
    PercentPercentToken,

    /// <summary>
    /// Represents the <c>%</c> token.
    /// </summary>
    PercentToken,

    /// <summary>
    /// Represents the <c>++</c> token.
    /// </summary>
    PlusPlusToken,

    /// <summary>
    /// Represents the <c>+</c> token.
    /// </summary>
    PlusToken,

    /// <summary>
    /// Represents the <c>?</c> token.
    /// </summary>
    QuestionToken,

    /// <summary>
    /// Represents the <c>//</c> token.
    /// </summary>
    SlashToken,

    /// <summary>
    /// Represents the <c>~=</c> token.
    /// </summary>
    TildeEqualsToken,

    /// <summary>
    /// Represents the <c>~!</c> token.
    /// </summary>
    TildeExclamationToken,

    /// <summary>
    /// Represents the <c>~</c> token.
    /// </summary>
    TildeToken,
}
