namespace PipeDream.Compiler.Parsing;

internal enum ParserMode
{
    Initial,
    BeginLine,
        BeginStatement,
            BeginAssignmentStatement,
                BeginAssignmentStatementValue,
                BeginAssignmentStatementValueRootedPath,
        BeginBinaryExpression,
        BeginUnaryExpression,
            BeginUnaryExpressionIdentifier,
        BeginFunctionCall,
            BeginFunctionCallParameter,
        EndFunctionCall,

        BeginPreprocessorDefine,
            PreprocessorDefineIdentifier,
                PreprocessorDefineValue,
                PreprocessorDefineValueContinued,

        BeginPreprocessorEndIf,
        BeginPreprocessorError,
        BeginPreprocessorIf,
            BeginPreprocessorIfExpression,

        BeginPreprocessorIfDef,
            PreprocessorIfDefIdentifier,

        BeginPreprocessorIfNDef,

        BeginPreprocessorInclude,
            PreprocessorIncludeFile,

        BeginPreprocessorWarn,

        BeginRootedPath,
        EndRootedPath,
        BeginPath,
            PathComponent,
}

internal enum ParserRule
{
    Assignment,
    BinaryExpression,
    FunctionCall,
    PreprocessorDefinition,
    PreprocessorDefinitionValue,
    PreprocessorInclusion,
    PreprocessorIfDefinition,
    PreprocessorIfStatement,
    PreprocessorIfBlock,
    RootPath,
    UnaryExpression,
}