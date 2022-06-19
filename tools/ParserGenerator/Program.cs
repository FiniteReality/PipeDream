using System.Collections.Generic;
using System.Linq;
using System.Text;

var grammar = new Grammar(
    new("CompilationUnit",
        new Production("StatementList", "EndOfFile")),

    new("Block",
        new("OpenBrace", "StatementList", "CloseBrace") { Name = "NestedBlock" },
        new("Expression", "OpenBrace", "StatementList", "CloseBrace") { Name = "ExpressionBlock" }),

    new("StatementList",
        new("Statement") { Name = "SingleStatementItemStatementList" },
        new("PreprocessorStatement") { Name = "SinglePreprocessorItemStatementList" },
        new("Block") { Name = "SingleBlockItemStatementList" },
        new("StatementList", "PreprocessorStatement") { Name = "MultiplePreprocessorItemStatementList" },
        new("StatementList", "Statement") { Name = "MultipleStatementItemStatementList" },
        new("StatementList", "Block") { Name = "MultipleBlockItemStatementList" }),

    new("StatementTerminator",
        new("EndOfLine"),
        new("Semicolon")),

    new("Statement",
        new("StatementTerminator") { Name = "EmptyStatement" },
        new("Expression", "StatementTerminator") { Name = "ExpressionStatement" },
        new("Expression", "Equals", "Expression", "StatementTerminator") { Name = "AssignmentStatement" }),

    new("PreprocessorStatement",
        new("PreprocessorDefine", "Identifier", "PreprocessorDefinitionValue", "EndOfLine") { Name = "ValuedPreprocessorDefinition" },
        new("PreprocessorDefine", "Identifier", "EndOfLine") { Name = "UnvaluedPreprocessorDefinition" },
        new("PreprocessorEndIf", "EndOfLine") { Name = "PreprocessorEndIf" },
        new("PreprocessorIf", "Expression", "EndOfLine") { Name = "PreprocessorIf" },
        new("PreprocessorIfDef", "Identifier", "EndOfLine") { Name = "PreprocessorIfDef" },
        new("PreprocessorInclude", "String", "EndOfLine") { Name = "PreprocessorInclude" }),

    new("PreprocessorDefinitionValue",
        new("Dot") { Name = "SingleItemPreprocessorDefinitionValue" },
        new("Identifier") { Name = "SingleItemPreprocessorDefinitionValue" },
        new("String") { Name = "SingleItemPreprocessorDefinitionValue" },
        new("PreprocessorDefinitionValue", "Dot") { Name = "MultipleItemPreprocessorDefinitionValue" },
        new("PreprocessorDefinitionValue", "Identifier") { Name = "MultipleItemPreprocessorDefinitionValue" },
        new("PreprocessorDefinitionValue", "String") { Name = "MultipleItemPreprocessorDefinitionValue" }),

    new("ExpressionList",
        new("Expression") { Name = "SingleItemExpressionList" },
        new("ExpressionList", "Comma", "Expression") { Name = "MultipleItemExpressionList" }),

    new("Expression",
        new("Dot") { Name = "DotExpression" },
        new("Identifier") { Name = "IdentifierExpression" },
        new("Slash", "Identifier") { Name = "RootPathExpression" },
        new("Exclamation", "Expression") { Name = "UnaryExpression" },
        new("Expression", "Slash", "Expression") { Name = "BinaryExpression" },
        new("Expression", "DoubleAmpersand", "Expression") { Name = "BinaryExpression" },
        new("OpenParenthesis", "Expression", "CloseParenthesis") { Name = "ParenthesizedExpression" },
        new("DoubleDot", "OpenParenthesis", "ExpressionList", "CloseParenthesis") { Name = "ParentCallExpression" },
        new("Identifier", "OpenParenthesis", "ExpressionList", "CloseParenthesis") { Name = "FunctionCallExpression" })
);

//Console.WriteLine(AntlrTools.GenerateAntlrGrammar(grammar, "dreammaker"));

var table = new LrOneTable(grammar);

var output = new StringBuilder(
@"#nullable enable
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Parsing;

public partial struct Parser
{
    private bool Decide((int state, SyntaxNode? node) current, Token lookahead)
        => (current.state, lookahead.Kind) switch
        {
");

foreach (var action in table.Actions)
{
    _ = output.AppendLine(
@$"            ({action.Key.Item1}, SyntaxKind.{action.Key.Item2}) // {action.Value}");
    _ = action.Value.Type switch
    {
        // TODO: better error recovery!
        LrOneTable.ActionType.Error
            => output.AppendLine(
@"                => Error(ParseError.Unexpected(lookahead)),"),
        LrOneTable.ActionType.Shift
            => output.AppendLine(
$"                => Shift({action.Value.TargetState}),"),
        LrOneTable.ActionType.Reduce
            => ReduceBy(output, action.Value),
        LrOneTable.ActionType.Accept
            => output.AppendLine(
$"                => Accept(),"),
        _ => output.AppendLine(
@$"                => Error(new(lookahead.Span, ""Unknown action {action.Value}"")),")
    };
}

output.AppendLine(
@"            var x => Error(ParseError.Unexpected(lookahead))
        };
}");

File.WriteAllText(args[0], output.ToString());

StringBuilder ReduceBy(StringBuilder output, LrOneTable.Action action)
{
    var pops = string.Join(",",
        Enumerable.Repeat("PopNode()!", action.Production!.Symbols.Length));
    var types = string.Join(", ",
        action.Production!.Symbols.Reverse().Select((x, i) =>
            $"{x}{(grammar.GetRuleByName(x) != null ? "" : "Token")}Node _{i}"));
    var variables = string.Join(",\n                                    ",
        Enumerable.Range(1, action.Production!.Symbols.Length)
            .Select(x => $"_{action.Production.Symbols.Length - x}"));
    _ = output.Append(
$@"                => ({pops}) switch {{
                        ({types})
                            => Reduce(
                                new {action.Production?.Name ?? action.Rule!.Name}Node(
                                    {variables}),
                                state => state switch
                                {{");
    // TODO: instead of dumping all of the possible goto states, we should
    // only dump the ones reachable from the current state
    int i = 8;
    foreach (var state in table.Gotos[action.Rule!.Name])
    {
        switch (i++)
        {
            case >= 8:
                i = 0;
                _ = output.Append($@"
                                    ");
                break;
            default:
                _ = output.Append(' ');
                break;
        }

        _ = output.Append($@"{state.Key} => {state.Value},");
    }

    var debuggingVariables = string.Join(", ",
        Enumerable.Range(1, action.Production!.Symbols.Length)
            .Select(x => $"var dbg{x}"));
    _ = output.AppendLine($@"
                                _ => -1
                            }}),
                        ({debuggingVariables}) => Error(
                            new(default, ""Unexpected syntax node when reducing {action.Item}"")
                        )
                    }},");

    return output;
}