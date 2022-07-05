using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Preprocessing;

public partial struct Preprocessor
{
    private StatementNode? HandleStatement(StatementNode statement)
        => !OutputEnable
            ? null
            : statement switch
            {
                EmptyStatementNode
                    => null,
                ExpressionStatementNode expression
                    => HandleExpressionStatement(expression),
                AssignmentStatementNode assignment
                    => HandleAssignmentStatement(assignment),
                _ => throw new InvalidOperationException(
                        $"Unhandled node {statement.GetType()}")
            };

    private ExpressionStatementNode? HandleExpressionStatement(
        ExpressionStatementNode expression)
    {
        var content = Handle(expression.Expression) as ExpressionNode;

        if (content == null)
            return null;
        else if (content != expression.Expression)
            return expression with { Expression = content };

        return expression;
    }

    private AssignmentStatementNode HandleAssignmentStatement(
        AssignmentStatementNode assignment)
    {
        var left = Handle(assignment.Left) as ExpressionNode;
        var right = Handle(assignment.Right) as ExpressionNode;

        if (left == null || right == null)
            throw new InvalidOperationException("Unexpected null");
        else if (left != assignment.Left
            || right != assignment.Right)
            return assignment with { Left = left, Right = right };

        return assignment;
    }
}