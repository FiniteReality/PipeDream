using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Xml;

namespace PipeDream.Compiler.Parsing.Tree;

internal sealed record CompilationUnitNode(
    SyntaxNode Contents,
    EndOfFileTokenNode EndOfFile)
    : SyntaxNode(ComputeSpan(Contents, EndOfFile))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Contents);
        visitor.Visit(EndOfFile);
    }
}

internal record BlockNode(
    StatementListNode StatementList)
    : SyntaxNode(StatementList.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(StatementList);
}

internal record NestedBlockNode : BlockNode
{
    public OpenBraceTokenNode OpenBrace { get; init; }
    public CloseBraceTokenNode CloseBrace { get; init; }

    public NestedBlockNode(
        OpenBraceTokenNode openBrace,
        StatementListNode statementList,
        CloseBraceTokenNode closeBrace)
        : base(statementList)
    {
        OpenBrace = openBrace;
        CloseBrace = closeBrace;
        Span = ComputeSpan(OpenBrace, CloseBrace);
    }

    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(OpenBrace);
        visitor.Visit(StatementList);
        visitor.Visit(CloseBrace);
    }
}

internal sealed record ExpressionBlockNode : NestedBlockNode
{
    public ExpressionNode Expression { get; init; }

    public ExpressionBlockNode(
        ExpressionNode expression,
        OpenBraceTokenNode openBrace,
        StatementListNode statementList,
        CloseBraceTokenNode closeBrace)
        : base(openBrace, statementList, closeBrace)
    {
        Expression = expression;
        Span = ComputeSpan(Expression, CloseBrace);
    }

    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Expression);
        visitor.Visit(OpenBrace);
        visitor.Visit(StatementList);
        visitor.Visit(CloseBrace);
    }
}

internal abstract record StatementListNode(TokenSpan Span)
    : SyntaxNode(Span);

internal abstract record SingleItemStatementListNode(TokenSpan Span)
    : StatementListNode(Span);

internal abstract record MultipleItemStatementListNode(TokenSpan Span)
    : StatementListNode(Span);

internal sealed record SingleStatementItemStatementListNode(
    StatementNode Statement)
    : SingleItemStatementListNode(Statement.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Statement);
}

internal sealed record SinglePreprocessorItemStatementListNode(
    PreprocessorStatementNode PreprocessorStatement)
    : SingleItemStatementListNode(PreprocessorStatement.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(PreprocessorStatement);
}

internal sealed record SingleBlockItemStatementListNode(BlockNode Block)
    : SingleItemStatementListNode(Block.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Block);
}

internal sealed record MultiplePreprocessorItemStatementListNode(
    StatementListNode StatementList,
    PreprocessorStatementNode PreprocessorStatement)
    : SingleItemStatementListNode(
        ComputeSpan(StatementList, PreprocessorStatement))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(StatementList);
        visitor.Visit(PreprocessorStatement);
    }
}

internal sealed record MultipleStatementItemStatementListNode(
    StatementListNode StatementList,
    StatementNode Statement)
    : MultipleItemStatementListNode(
        ComputeSpan(StatementList, Statement))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(StatementList);
        visitor.Visit(Statement);
    }
}

internal sealed record MultipleBlockItemStatementListNode(
    StatementListNode StatementList,
    BlockNode Block)
    : MultipleItemStatementListNode(ComputeSpan(StatementList, Block))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(StatementList);
        visitor.Visit(Block);
    }
}

internal sealed record StatementTerminatorNode(
    SyntaxNode Terminator)
    : SyntaxNode(Terminator.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Terminator);
}

internal abstract record StatementNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record EmptyStatementNode(StatementTerminatorNode Terminator)
    : StatementNode(Terminator.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Terminator);
}

internal sealed record ExpressionStatementNode(
    ExpressionNode Expression,
    StatementTerminatorNode Terminator)
    : StatementNode(ComputeSpan(Expression, Terminator))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Expression);
        visitor.Visit(Terminator);
    }
}

internal sealed record AssignmentStatementNode(
    ExpressionNode Left,
    EqualsTokenNode EqualsSign,
    ExpressionNode Right,
    StatementTerminatorNode Terminator)
    : StatementNode(ComputeSpan(Left, Terminator))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Left);
        visitor.Visit(EqualsSign);
        visitor.Visit(Right);
        visitor.Visit(Terminator);
    }
}

internal abstract record PreprocessorStatementNode(TokenSpan Span)
    : SyntaxNode(Span);

internal abstract record PreprocessorDefinitionNode(TokenSpan Span)
    : PreprocessorStatementNode(Span);

internal sealed record PreprocessorEndIfNode(
    PreprocessorEndIfTokenNode PreprocessorEndIf,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorStatementNode(ComputeSpan(PreprocessorEndIf, EndOfLine))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorEndIf);
        visitor.Visit(EndOfLine);
    }
}

internal sealed record PreprocessorIfNode(
    PreprocessorIfTokenNode PreprocessorIf,
    ExpressionNode Expression,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorStatementNode(ComputeSpan(PreprocessorIf, EndOfLine))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorIf);
        visitor.Visit(Expression);
        visitor.Visit(EndOfLine);
    }
}

internal sealed record PreprocessorIfDefNode(
    PreprocessorIfDefTokenNode PreprocessorIfDef,
    IdentifierTokenNode Identifier,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorStatementNode(ComputeSpan(PreprocessorIfDef, EndOfLine))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorIfDef);
        visitor.Visit(Identifier);
        visitor.Visit(EndOfLine);
    }
}

internal sealed record PreprocessorIncludeNode(
    PreprocessorIncludeTokenNode PreprocessorInclude,
    StringTokenNode File,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorStatementNode(ComputeSpan(PreprocessorInclude, EndOfLine))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorInclude);
        visitor.Visit(File);
        visitor.Visit(EndOfLine);
    }
}

internal sealed record ValuedPreprocessorDefinitionNode(
    PreprocessorDefineTokenNode PreprocessorDefine,
    IdentifierTokenNode Identifier,
    PreprocessorDefinitionValueNode Value,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorDefinitionNode(ComputeSpan(PreprocessorDefine, EndOfLine))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorDefine);
        visitor.Visit(Identifier);
        visitor.Visit(Value);
        visitor.Visit(EndOfLine);
    }
}

internal abstract record PreprocessorDefinitionValueNode(TokenSpan Span)
    : PreprocessorStatementNode(Span);

internal sealed record SingleItemPreprocessorDefinitionValueNode(
    SyntaxNode Value)
    : PreprocessorDefinitionValueNode(Value.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Value);
    }
}

internal sealed record MultipleItemPreprocessorDefinitionValueNode(
    PreprocessorDefinitionValueNode PreprocessorDefinitionValue,
    SyntaxNode Value)
    : PreprocessorDefinitionNode(
        ComputeSpan(PreprocessorDefinitionValue, Value))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorDefinitionValue);
        visitor.Visit(Value);
    }
}

internal sealed record UnvaluedPreprocessorDefinitionNode(
    PreprocessorDefineTokenNode PreprocessorDefine,
    IdentifierTokenNode Identifier,
    EndOfLineTokenNode EndOfLine)
    : PreprocessorDefinitionNode(ComputeSpan(PreprocessorDefine, EndOfLine))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(PreprocessorDefine);
        visitor.Visit(Identifier);
        visitor.Visit(EndOfLine);
    }
}

internal abstract record ExpressionListNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record SingleItemExpressionListNode(ExpressionNode Expression)
    : ExpressionListNode(Expression.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Expression);
}

internal sealed record MultipleItemExpressionListNode(
    ExpressionListNode ExpressionList,
    CommaTokenNode Comma,
    ExpressionNode Expression)
    : ExpressionListNode(ComputeSpan(ExpressionList, Expression))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(ExpressionList);
        visitor.Visit(Comma);
        visitor.Visit(Expression);
    }
}

internal abstract record ExpressionNode(TokenSpan Span)
    : SyntaxNode(Span);

internal sealed record DotExpressionNode(DotTokenNode Dot)
    : ExpressionNode(Dot.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Dot);
}

internal sealed record IdentifierExpressionNode(IdentifierTokenNode Identifier)
    : ExpressionNode(Identifier.Span)
{
    protected override void Accept(SyntaxVisitor visitor)
        => visitor.Visit(Identifier);
}

internal sealed record RootPathExpressionNode(
    SlashTokenNode Slash,
    IdentifierTokenNode Identifier)
    : ExpressionNode(ComputeSpan(Slash, Identifier))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Slash);
        visitor.Visit(Identifier);
    }
}

internal sealed record UnaryExpressionNode(
    SyntaxNode Operator,
    ExpressionNode Expression)
    : ExpressionNode(ComputeSpan(Operator, Expression))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Operator);
        visitor.Visit(Expression);
    }
}

internal sealed record BinaryExpressionNode(
    ExpressionNode Left,
    SyntaxNode Operator,
    ExpressionNode Right)
    : ExpressionNode(ComputeSpan(Left, Right))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Left);
        visitor.Visit(Operator);
        visitor.Visit(Right);
    }
}

internal sealed record ParenthesizedExpressionNode(
    OpenParenthesisTokenNode OpenParenthesis,
    ExpressionNode Expression,
    CloseParenthesisTokenNode CloseParenthesis)
    : ExpressionNode(ComputeSpan(OpenParenthesis, CloseParenthesis))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(OpenParenthesis);
        visitor.Visit(Expression);
        visitor.Visit(CloseParenthesis);
    }
}

internal sealed record ParentCallExpressionNode(
    DoubleDotTokenNode DoubleDot,
    OpenParenthesisTokenNode OpenParenthesis,
    ExpressionListNode ExpressionList,
    CloseParenthesisTokenNode CloseParenthesis)
    : ExpressionNode(ComputeSpan(DoubleDot, CloseParenthesis))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(DoubleDot);
        visitor.Visit(OpenParenthesis);
        visitor.Visit(ExpressionList);
        visitor.Visit(CloseParenthesis);
    }
}

internal sealed record FunctionCallExpressionNode(
    IdentifierTokenNode Identifier,
    OpenParenthesisTokenNode OpenParenthesis,
    ExpressionListNode ExpressionList,
    CloseParenthesisTokenNode CloseParenthesis)
    : ExpressionNode(ComputeSpan(Identifier, CloseParenthesis))
{
    protected override void Accept(SyntaxVisitor visitor)
    {
        visitor.Visit(Identifier);
        visitor.Visit(OpenParenthesis);
        visitor.Visit(ExpressionList);
        visitor.Visit(CloseParenthesis);
    }
}