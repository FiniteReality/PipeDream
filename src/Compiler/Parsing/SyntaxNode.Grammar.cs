using System.Collections.Immutable;
using System.Xml;

namespace PipeDream.Compiler.Parsing.Tree;

internal sealed record CompilationUnitNode(SyntaxNode Statements)
    : SyntaxNode(Statements.Span);

internal sealed record StatementListNode(SyntaxNode Statement, SyntaxNode? StatementList)
    : SyntaxNode(ComputeSpan(Statement, StatementList));

internal sealed record StatementNode(SyntaxNode Node)
    : SyntaxNode(Node.Span);

internal sealed record ExpressionNode(SyntaxNode Expression)
    : SyntaxNode(Expression.Span);

internal sealed record BinaryExpressionNode(
    SyntaxNode Left,
    SyntaxNode Right)
    : SyntaxNode(ComputeSpan(Left, Right));
