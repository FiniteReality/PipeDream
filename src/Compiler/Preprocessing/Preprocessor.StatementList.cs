using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Preprocessing;

public partial struct Preprocessor
{
    private StatementListNode? HandleStatementList(
        StatementListNode list)
        => list switch
        {
            SingleItemStatementListNode single
                => HandleSingleItemStatementList(single),
            MultipleItemStatementListNode multiple
                => HandleMultipleItemStatementList(multiple),
            _ => throw new InvalidOperationException(
                $"Unhandled node {list.GetType()}")
        };

    private StatementListNode? HandleSingleItemStatementList(
        SingleItemStatementListNode list)
    {
        switch (list)
        {
            case SingleStatementItemStatementListNode statement:
            {
                var contents = Handle(statement.Statement) as StatementNode;

                if (contents == null)
                    return null;
                else if (contents != statement.Statement)
                    return statement with { Statement = contents };
                else
                    return statement;
            }
            case SinglePreprocessorItemStatementListNode preprocessor:
            {
                HandleDirective(preprocessor.PreprocessorStatement);
                return null;
            }
            case SingleBlockItemStatementListNode block:
            {
                var contents = Handle(block.Block) as BlockNode;

                if (contents == null)
                    return null;
                else if (contents != block.Block)
                    return block with { Block = contents };
                else
                    return block;
            }
            default:
                throw new InvalidOperationException(
                    $"Unhandled node {list.GetType()}");
        }
    }

    private StatementListNode? HandleMultipleItemStatementList(
        MultipleItemStatementListNode list)
    {
        // list.StatementList comes *before* whatever we contain.
        var statements = HandleMultipleItemStatementListChildren(list);

        if (statements == null)
        {
            // Morph to single
            switch (list)
            {
                case MultipleStatementItemStatementListNode statement:
                {
                    var contents = Handle(statement.Statement)
                        as StatementNode;

                    return contents != null
                        ? new SingleStatementItemStatementListNode(contents)
                        : null;
                }
                case MultiplePreprocessorItemStatementListNode preprocessor:
                {
                    HandleDirective(preprocessor.PreprocessorStatement);
                    return null;
                }
                case MultipleBlockItemStatementListNode block:
                {
                    var contents = Handle(block.Block)
                        as BlockNode;

                    return contents != null
                        ? new SingleBlockItemStatementListNode(contents)
                        : null;
                }
                default:
                    throw new InvalidOperationException(
                        $"Unhandled node {list.GetType()}");
            }
        }

        // Remain multiple
        switch (list)
        {
            case MultipleStatementItemStatementListNode statement:
            {
                var contents = Handle(statement.Statement)
                    as StatementNode;

                if (contents == null)
                    return statements;
                else if (contents != statement.Statement
                    || statements != statement.StatementList)
                    return statement with
                    {
                        StatementList = statements,
                        Statement = contents
                    };

                return statement;
            }
            case MultiplePreprocessorItemStatementListNode preprocessor:
            {
                HandleDirective(preprocessor.PreprocessorStatement);
                return statements;
            }
            case MultipleBlockItemStatementListNode block:
            {
                var contents = Handle(block.Block)
                    as BlockNode;

                if (contents == null)
                    return statements;
                else if (contents != block.Block
                    || statements != block.StatementList)
                    return block with
                    {
                        StatementList = statements,
                        Block = contents
                    };

                return block;
            }
            default:
                throw new InvalidOperationException(
                    $"Unhandled node {list.GetType()}");
        }
    }

    private StatementListNode? HandleMultipleItemStatementListChildren(
        MultipleItemStatementListNode list)
    {
        switch (list)
        {
            case MultipleStatementItemStatementListNode statement:
                return Handle(statement.StatementList)
                    as StatementListNode;
            case MultiplePreprocessorItemStatementListNode preprocessor:
                return Handle(preprocessor.StatementList)
                    as StatementListNode;
            case MultipleBlockItemStatementListNode block:
                return Handle(block.StatementList)
                    as StatementListNode;
            default:
                throw new InvalidOperationException(
                    $"Unhandled node {list.GetType()}");
        }
    }
}