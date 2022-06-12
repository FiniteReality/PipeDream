namespace PipeDream.Compiler.Parsing;

internal enum Rule
{
    CompilationUnit,
    Block,
        StatementList,
            Statement,
                Assignment,

    Expression,
        String,
        Identifier,
        BinaryExpression,
}
