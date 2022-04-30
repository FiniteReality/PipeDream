namespace PipeDream.Compiler.Parsing;

internal enum ParserMode
{
    Initial,
    BeginLine,
    EmptyLine,
    CommentLine,
    Final,

    BeginStatement,
    BeginAssignmentStatement,
    EndAssignmentStatement,
    EndStatement,
    EndStatementLine,

    RootedPath,
    RootedPathLeaf,

    BeginExpression,
    BeginPathExpression,
    BeginCompoundExpression,
    EndExpression
}

// N.B. nonterminal rules!
internal enum ParserRule
{
    CompilationUnit,
    Comment,
    Statement,
        AssignmentStatement,
    Expression,
    PathRoot,
    Path
}