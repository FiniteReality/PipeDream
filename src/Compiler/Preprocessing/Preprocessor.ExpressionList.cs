using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Preprocessing;

public partial struct Preprocessor
{
    private ExpressionListNode? HandleExpressionList(
        ExpressionListNode list)
        => list switch
        {
            SingleItemExpressionListNode single
                => HandleSingleItemExpressionList(single),
            MultipleItemExpressionListNode multiple
                => HandleMultipleItemExpressionList(multiple),
            _ => throw new InvalidOperationException(
                $"Unhandled node {list.GetType()}")
        };

    private SingleItemExpressionListNode? HandleSingleItemExpressionList(
        SingleItemExpressionListNode list)
    {
        var expression = Handle(list.Expression) as ExpressionNode;

        if (expression == null)
            throw new InvalidOperationException("Unexpected null");
        else if (expression != list.Expression)
            return list with { Expression = expression };

        return list;
    }

    private MultipleItemExpressionListNode? HandleMultipleItemExpressionList(
        MultipleItemExpressionListNode list)
    {
        var expressions = Handle(list.ExpressionList) as ExpressionListNode;
        var expression = Handle(list.Expression) as ExpressionNode;

        if (expressions == null || expression == null)
            throw new InvalidOperationException("Unexpected null");
        else if (expressions != list.Expression
            || expression != null)
            return list with
            {
                ExpressionList = expressions,
                Expression = expression
            };

        return list;
    }
}