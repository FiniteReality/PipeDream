using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Preprocessing;

public partial struct Preprocessor
{
    private BlockNode? HandleBlock(BlockNode block)
        => block switch
        {
            BracedBlockNode braced
                => HandleBracedBlock(braced),
            IdentifiedBlockNode identified
                => HandleIdentifiedBlock(identified),
            _ => throw new InvalidOperationException(
                $"Unhandled node {block.GetType()}")
        };

    private BlockNode? HandleBracedBlock(BracedBlockNode block)
    {
        var statements = Handle(block.StatementList) as StatementListNode;

        if (statements == null)
            throw new InvalidOperationException(
                "Expected non-null statement list");

        if (statements != block.StatementList)
            return block with { StatementList = statements };

        return block;
    }

    private BlockNode? HandleIdentifiedBlock(
        IdentifiedBlockNode block)
    {
        var expression = Handle(block.Expression) as ExpressionNode;
        var statements = Handle(block.StatementList) as StatementListNode;

        if (statements == null)
            throw new InvalidOperationException(
                "Expected non-null statement list");

        if (expression == null)
        {
            return new BracedBlockNode(
                block.OpenBrace,
                statements,
                block.CloseBrace);
        }
        else if (expression != block.Expression
            || statements != block.StatementList)
        {
            return block with
            {
                Expression = expression,
                StatementList = statements
            };
        }

        return block;
    }
}