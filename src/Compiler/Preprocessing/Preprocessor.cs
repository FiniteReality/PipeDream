using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Preprocessing;

/// <summary>
/// Defines a struct used for preprocessing Dream Maker source code.
/// </summary>
public partial struct Preprocessor
{
    /// <summary>
    /// Creates a preprocessor for the given preprocessor state.
    /// </summary>
    public Preprocessor()
    {
        OutputEnable = true;
    }

    private bool OutputEnable { get; set; }

    /// <summary>
    /// Preprocesses the given compilation unit.
    /// </summary>
    /// <param name="compilationUnit">
    /// The compilation unit to preprocess.
    /// </param>
    /// <returns>
    /// The preprocessed compilation unit.
    /// </returns>
    public CompilationUnitNode Preprocess(CompilationUnitNode compilationUnit)
        => compilationUnit with
        {
            Contents = (StatementListNode)Handle(compilationUnit.Contents)!
        };

    private void HandleDirective(PreprocessorStatementNode directive)
    {

    }

    private SyntaxNode? Handle(SyntaxNode node)
        => node switch
        {
            null => throw new ArgumentNullException(nameof(node)),

            BlockNode block
                => HandleBlock(block),
            StatementListNode statementList
                => HandleStatementList(statementList),
            StatementNode statement
                => HandleStatement(statement),
            ExpressionListNode expressionList
                => HandleExpressionList(expressionList),
            ExpressionNode expression
                => HandleExpression(expression),

            _ => throw new InvalidOperationException(
                $"Unhandled node {node.GetType()}")
        };
}