using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Xml;

namespace PipeDream.Compiler.Parsing.Tree;

/// <summary>
/// Defines a record containing syntactical information about a compilation
/// unit.
/// </summary>
/// <param name="Contents">
/// The contents of this compilation unit.
/// </param>
/// <param name="EndOfFile">
/// The end of file token for this compilation unit.
/// </param>
public sealed record CompilationUnitNode(
    StatementListNode Contents,
    EndOfFileTokenNode EndOfFile)
    : SyntaxNode(ComputeSpan(Contents, EndOfFile))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Contents);
        visitor.Visit(EndOfFile);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a block.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record BlockNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a block delimited
/// with braces.
/// </summary>
/// <param name="OpenBrace">
/// The opening brace token.
/// </param>
/// <param name="StatementList">
/// The statements this block contains.
/// </param>
/// <param name="CloseBrace">
/// The closing brace token.
/// </param>
public sealed record BracedBlockNode(
    OpenBraceTokenNode OpenBrace,
    StatementListNode StatementList,
    CloseBraceTokenNode CloseBrace)
        : BlockNode(ComputeSpan(OpenBrace, CloseBrace))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(OpenBrace);
        visitor.Visit(StatementList);
        visitor.Visit(CloseBrace);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a block
/// identified by an expression.
/// </summary>
/// <param name="Expression">
/// The expression which identifies this block.
/// </param>
/// <param name="OpenBrace">
/// The opening brace token.
/// </param>
/// <param name="StatementList">
/// The statements this block contains.
/// </param>
/// <param name="CloseBrace">
/// The closing brace token.
/// </param>
public sealed record IdentifiedBlockNode(
    ExpressionNode Expression,
    OpenBraceTokenNode OpenBrace,
    StatementListNode StatementList,
    CloseBraceTokenNode CloseBrace)
    : BlockNode(ComputeSpan(Expression, CloseBrace))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Expression);
        visitor.Visit(OpenBrace);
        visitor.Visit(StatementList);
        visitor.Visit(CloseBrace);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a statement list.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record StatementListNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a statement list
/// containing only one item.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record SingleItemStatementListNode(TokenSpan Span)
    : StatementListNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a statement list
/// containing multiple items.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record MultipleItemStatementListNode(TokenSpan Span)
    : StatementListNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a statement list
/// containing a single statement.
/// </summary>
/// <param name="Statement">
/// The statement contained by this statement list.
/// </param>
public sealed record SingleStatementItemStatementListNode(
    StatementNode Statement)
    : SingleItemStatementListNode(Statement.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Statement);
}

/// <summary>
/// Defines a record containing syntactical information about a statement list
/// containing a single preprocessor directive.
/// </summary>
/// <param name="PreprocessorStatement">
/// The preprocessor directive contained by this statement list.
/// </param>
public sealed record SinglePreprocessorItemStatementListNode(
    PreprocessorStatementNode PreprocessorStatement)
    : SingleItemStatementListNode(PreprocessorStatement.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(PreprocessorStatement);
}

/// <summary>
/// Defines a record containing syntactical information about a statement list
/// containing a single block.
/// </summary>
/// <param name="Block">
/// The block contained by this statement list.
/// </param>
public sealed record SingleBlockItemStatementListNode(BlockNode Block)
    : SingleItemStatementListNode(Block.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Block);
}

/// <summary>
/// Defines a record containing syntactical information about a statement list
/// containin multiple statements, followed by a statement.
/// </summary>
/// <param name="StatementList">
/// The statement list contained by this statement list.
/// </param>
/// <param name="Statement">
/// The statement contained by this statement list.
/// </param>
public sealed record MultipleStatementItemStatementListNode(
    StatementListNode StatementList,
    StatementNode Statement)
    : MultipleItemStatementListNode(
        ComputeSpan(StatementList, Statement))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(StatementList);
        visitor.Visit(Statement);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a statement list
/// containing multiple statements, followed by a preprocessor statement.
/// </summary>
/// <param name="StatementList">
/// The statement list contained by this statement list.
/// </param>
/// <param name="PreprocessorStatement">
/// The preprocessor statement contained by this statement list.
/// </param>
public sealed record MultiplePreprocessorItemStatementListNode(
    StatementListNode StatementList,
    PreprocessorStatementNode PreprocessorStatement)
    : MultipleItemStatementListNode(
        ComputeSpan(StatementList, PreprocessorStatement))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(StatementList);
        visitor.Visit(PreprocessorStatement);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a statement list
/// containing multiple statements, followed by a block.
/// </summary>
/// <param name="StatementList">
/// The statement list contained by this statement list.
/// </param>
/// <param name="Block">
/// The block contained by this statement list.
/// </param>
public sealed record MultipleBlockItemStatementListNode(
    StatementListNode StatementList,
    BlockNode Block)
    : MultipleItemStatementListNode(ComputeSpan(StatementList, Block))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(StatementList);
        visitor.Visit(Block);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a statement
/// terminator.
/// </summary>
/// <param name="Terminator">
/// The syntax node used to terminate the current statement.
/// </param>
public sealed record StatementTerminatorNode(
    SyntaxNode Terminator)
    : SyntaxNode(Terminator.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Terminator);
}

/// <summary>
/// Defines a record containing syntactical information about a statement.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record StatementNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an empty
/// statement.
/// </summary>
/// <param name="Terminator">
/// The terminator used to terminate this empty statement.
/// </param>
public sealed record EmptyStatementNode(StatementTerminatorNode Terminator)
    : StatementNode(Terminator.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Terminator);
}

/// <summary>
/// Defines a record containing syntactical information about a statement
/// containing a single expression.
/// </summary>
/// <param name="Expression">
/// The expression contained by this statement.
/// </param>
/// <param name="Terminator">
/// The terminator used to terminate this statement.
/// </param>
public sealed record ExpressionStatementNode(
    ExpressionNode Expression,
    StatementTerminatorNode Terminator)
    : StatementNode(ComputeSpan(Expression, Terminator))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Expression);
        visitor.Visit(Terminator);
    }
}

/// <summary>
/// Defines a record containing syntactical information about an assignment
/// statement.
/// </summary>
/// <param name="Left">
/// The left-hand expression of the assignment.
/// </param>
/// <param name="EqualsSign">
/// The equals sign token.
/// </param>
/// <param name="Right">
/// The right-hand expression of the assignment.
/// </param>
/// <param name="Terminator">
/// The terminator used to terminate this statement.
/// </param>
public sealed record AssignmentStatementNode(
    ExpressionNode Left,
    EqualsTokenNode EqualsSign,
    ExpressionNode Right,
    StatementTerminatorNode Terminator)
    : StatementNode(ComputeSpan(Left, Terminator))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Left);
        visitor.Visit(EqualsSign);
        visitor.Visit(Right);
        visitor.Visit(Terminator);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// statement.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record PreprocessorStatementNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// definition statement.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record PreprocessorDefinitionNode(TokenSpan Span)
    : PreprocessorStatementNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// end-if node.
/// </summary>
/// <param name="PreprocessorEndIf">
/// The end-if preprocessor token.
/// </param>
/// <param name="EndOfLine">
/// The end-of-line token.
/// </param>
public sealed record PreprocessorEndIfNode(
    PreprocessorEndIfTokenNode PreprocessorEndIf,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorStatementNode(ComputeSpan(PreprocessorEndIf, EndOfLine))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorEndIf);
        visitor.Visit(EndOfLine);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a preprocessor if
/// node.
/// </summary>
/// <param name="PreprocessorIf">
/// The if preprocessor token.
/// </param>
/// <param name="Expression">
/// The expression contained by this preprocessor if node.
/// </param>
/// <param name="EndOfLine">
/// The end-of-line token.
/// </param>
public sealed record PreprocessorIfNode(
    PreprocessorIfTokenNode PreprocessorIf,
    ExpressionNode Expression,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorStatementNode(ComputeSpan(PreprocessorIf, EndOfLine))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorIf);
        visitor.Visit(Expression);
        visitor.Visit(EndOfLine);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// if-defined node.
/// </summary>
/// <param name="PreprocessorIfDef">
/// The preprocessor if-defined token.
/// </param>
/// <param name="Identifier">
/// The identifier contained by this preprocessor if-defined node.
/// </param>
/// <param name="EndOfLine">
/// The end-of-line token.
/// </param>
public sealed record PreprocessorIfDefNode(
    PreprocessorIfDefTokenNode PreprocessorIfDef,
    IdentifierTokenNode Identifier,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorStatementNode(ComputeSpan(PreprocessorIfDef, EndOfLine))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorIfDef);
        visitor.Visit(Identifier);
        visitor.Visit(EndOfLine);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// include node.
/// </summary>
/// <param name="PreprocessorInclude">
/// The preprocessor include token.
/// </param>
/// <param name="File">
/// The string token indicating which file to include.
/// </param>
/// <param name="EndOfLine">
/// The end of line token.
/// </param>
public sealed record PreprocessorIncludeNode(
    PreprocessorIncludeTokenNode PreprocessorInclude,
    StringTokenNode File,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorStatementNode(ComputeSpan(PreprocessorInclude, EndOfLine))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorInclude);
        visitor.Visit(File);
        visitor.Visit(EndOfLine);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// definition node containing a value.
/// </summary>
/// <param name="PreprocessorDefine">
/// The preprocessor define token.
/// </param>
/// <param name="Identifier">
/// The identifier to define.
/// </param>
/// <param name="Value">
/// The value to assign to the given identifier.
/// </param>
/// <param name="EndOfLine">
/// The end-of-line token.
/// </param>
public sealed record ValuedPreprocessorDefinitionNode(
    PreprocessorDefineTokenNode PreprocessorDefine,
    IdentifierTokenNode Identifier,
    PreprocessorDefinitionValueNode Value,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorDefinitionNode(ComputeSpan(PreprocessorDefine, EndOfLine))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorDefine);
        visitor.Visit(Identifier);
        visitor.Visit(Value);
        visitor.Visit(EndOfLine);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// definition value.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record PreprocessorDefinitionValueNode(TokenSpan Span)
    : PreprocessorStatementNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// definition value containing only one item.
/// </summary>
/// <param name="Value">
/// The value of this preprocessor definition.
/// </param>
public sealed record SingleItemPreprocessorDefinitionValueNode(
    SyntaxNode Value)
    : PreprocessorDefinitionValueNode(Value.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Value);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// definition value containing multiple items.
/// </summary>
/// <param name="PreprocessorDefinitionValue">
/// The preprocessor definition value contained by this preprocessor definition
/// value.
/// </param>
/// <param name="Value">
/// The value of this preprocessor definition.
/// </param>
public sealed record MultipleItemPreprocessorDefinitionValueNode(
    PreprocessorDefinitionValueNode PreprocessorDefinitionValue,
    SyntaxNode Value)
    : PreprocessorDefinitionNode(
        ComputeSpan(PreprocessorDefinitionValue, Value))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorDefinitionValue);
        visitor.Visit(Value);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a preprocessor
/// definition node containing no value.
/// </summary>
/// <param name="PreprocessorDefine">
/// The preprocessor define token.
/// </param>
/// <param name="Identifier">
/// The identifier to define.
/// </param>
/// <param name="EndOfLine">
/// The end-of-line token.
/// </param>
public sealed record UnvaluedPreprocessorDefinitionNode(
    PreprocessorDefineTokenNode PreprocessorDefine,
    IdentifierTokenNode Identifier,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorDefinitionNode(ComputeSpan(PreprocessorDefine, EndOfLine))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorDefine);
        visitor.Visit(Identifier);
        visitor.Visit(EndOfLine);
    }
}

/// <summary>
/// Defines a record containing syntactical information about an expression
/// list.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record ExpressionListNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about an expression
/// list containing only one item.
/// </summary>
/// <param name="Expression">
/// The expression contained by this expression list.
/// </param>
public sealed record SingleItemExpressionListNode(ExpressionNode Expression)
    : ExpressionListNode(Expression.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Expression);
}

/// <summary>
/// Defines a record containing syntactical information about an expression
/// list containing multiple items.
/// </summary>
/// <param name="ExpressionList">
/// The expression list contained by this expression list.
/// </param>
/// <param name="Comma">
/// The comma token.
/// </param>
/// <param name="Expression">
/// The expression contained by this expression list.
/// </param>
public sealed record MultipleItemExpressionListNode(
    ExpressionListNode ExpressionList,
    CommaTokenNode Comma,
    ExpressionNode Expression)
    : ExpressionListNode(ComputeSpan(ExpressionList, Expression))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(ExpressionList);
        visitor.Visit(Comma);
        visitor.Visit(Expression);
    }
}

/// <summary>
/// Defines a record containing syntactical information about an expression.
/// </summary>
/// <param name="Span">
/// The location of this syntax node in the source code.
/// </param>
public abstract record ExpressionNode(TokenSpan Span)
    : SyntaxNode(Span);

/// <summary>
/// Defines a record containing syntactical information about a dot expression.
/// </summary>
/// <param name="Dot">
/// The dot token.
/// </param>
public sealed record DotExpressionNode(DotTokenNode Dot)
    : ExpressionNode(Dot.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Dot);
}

/// <summary>
/// Defines a record containing syntactical information about an identifier
/// expression.
/// </summary>
/// <param name="Identifier">
/// The identifier token.
/// </param>
public sealed record IdentifierExpressionNode(IdentifierTokenNode Identifier)
    : ExpressionNode(Identifier.Span)
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Identifier);
}

/// <summary>
/// Defines a record containing syntactical information about a rooted path
/// expression.
/// </summary>
/// <param name="Slash">
/// The slash token.
/// </param>
/// <param name="Identifier">
/// The identifier token.
/// </param>
public sealed record RootPathExpressionNode(
    SlashTokenNode Slash,
    IdentifierTokenNode Identifier)
    : ExpressionNode(ComputeSpan(Slash, Identifier))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Slash);
        visitor.Visit(Identifier);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a unary
/// expression.
/// </summary>
/// <param name="Operator">
/// The operator to apply.
/// </param>
/// <param name="Expression">
/// The expression to apply the operator to.
/// </param>
public sealed record UnaryExpressionNode(
    SyntaxNode Operator,
    ExpressionNode Expression)
    : ExpressionNode(ComputeSpan(Operator, Expression))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Operator);
        visitor.Visit(Expression);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a binary
/// expression.
/// </summary>
/// <param name="Left">
/// The left-hand expression of the binary expression.
/// </param>
/// <param name="Operator">
/// The operator to apply.
/// </param>
/// <param name="Right">
/// The right-hand expression of the binary expression.
/// </param>
public sealed record BinaryExpressionNode(
    ExpressionNode Left,
    SyntaxNode Operator,
    ExpressionNode Right)
    : ExpressionNode(ComputeSpan(Left, Right))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Left);
        visitor.Visit(Operator);
        visitor.Visit(Right);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a parenthesized
/// expression.
/// </summary>
/// <param name="OpenParenthesis">
/// The open parenthesis token.
/// </param>
/// <param name="Expression">
/// The expression contained by this parenthesized expression.
/// </param>
/// <param name="CloseParenthesis">
/// The close parenthesis token.
/// </param>
/// <returns></returns>
public sealed record ParenthesizedExpressionNode(
    OpenParenthesisTokenNode OpenParenthesis,
    ExpressionNode Expression,
    CloseParenthesisTokenNode CloseParenthesis)
    : ExpressionNode(ComputeSpan(OpenParenthesis, CloseParenthesis))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(OpenParenthesis);
        visitor.Visit(Expression);
        visitor.Visit(CloseParenthesis);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a parent function
/// call expression.
/// </summary>
/// <param name="DoubleDot">
/// The double dot token.
/// </param>
/// <param name="OpenParenthesis">
/// The open parenthesis token.
/// </param>
/// <param name="ExpressionList">
/// The expression list of parameters.
/// </param>
/// <param name="CloseParenthesis">
/// The close parenthesis token.
/// </param>
public sealed record ParentCallExpressionNode(
    DoubleDotTokenNode DoubleDot,
    OpenParenthesisTokenNode OpenParenthesis,
    ExpressionListNode ExpressionList,
    CloseParenthesisTokenNode CloseParenthesis)
    : ExpressionNode(ComputeSpan(DoubleDot, CloseParenthesis))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(DoubleDot);
        visitor.Visit(OpenParenthesis);
        visitor.Visit(ExpressionList);
        visitor.Visit(CloseParenthesis);
    }
}

/// <summary>
/// Defines a record containing syntactical information about a function call
/// expression.
/// </summary>
/// <param name="Identifier">
/// The name of the function to call.
/// </param>
/// <param name="OpenParenthesis">
/// The open parenthesis token.
/// </param>
/// <param name="ExpressionList">
/// The expression list of parameters.
/// </param>
/// <param name="CloseParenthesis">
/// The close parenthesis token.
/// </param>
public sealed record FunctionCallExpressionNode(
    IdentifierTokenNode Identifier,
    OpenParenthesisTokenNode OpenParenthesis,
    ExpressionListNode ExpressionList,
    CloseParenthesisTokenNode CloseParenthesis)
    : ExpressionNode(ComputeSpan(Identifier, CloseParenthesis))
{
    /// <inheritdoc />
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Identifier);
        visitor.Visit(OpenParenthesis);
        visitor.Visit(ExpressionList);
        visitor.Visit(CloseParenthesis);
    }
}