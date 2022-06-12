using System.Collections.Generic;
using System.Linq;
using System.Text;

var grammar = new Grammar(
    new("CompilationUnit",
        new("BlockList", "EndOfFile"),
        new("EndOfFile")),

    new("BlockList",
        new("Block"),
        new("Block", "EndOfLine"),
        new("BlockList", "Block"),
        new("BlockList", "Block", "EndOfLine")),

    new("Block",
        new("OpenBrace", "StatementList", "CloseBrace"),
        new("Expression", "OpenBrace", "StatementList", "CloseBrace")),

    new("StatementList",
        new("Statement", "StatementTerminator"),
        new("PreprocessorStatement", "EndOfLine"),
        new("Block"),
        new("StatementList", "Statement", "StatementTerminator"),
        new("StatementList", "PreprocessorStatement", "EndOfLine"),
        new("StatementList", "Block")),

    new("StatementTerminator",
        new("EndOfLine"),
        new("Semicolon")),

    new("Statement",
        new("Expression"),
        new("Expression", "Equals", "Expression")),

    new("PreprocessorStatement",
        new("PreprocessorInclude", "String"),
        new("PreprocessorDefine", "Identifier"),
        new("PreprocessorDefine", "Identifier", "Statement"),
        new("PreprocessorDefine", "Identifier", "Expression")),

    new("ExpressionList",
        new("Expression"),
        new("ExpressionList", "Comma", "Expression")),

    new("Expression",
        new("Dot"),
        new("Identifier"),
        new("Slash", "Identifier"),
        new("Exclamation", "Expression"),
        new("Expression", "Slash", "Expression"),
        new("Expression", "DoubleAmpersand", "Expression"),
        new("OpenParenthesis", "Expression", "CloseParenthesis"),
        new("DoubleDot", "OpenParenthesis", "ExpressionList", "CloseParenthesis"),
        new("Identifier", "OpenParenthesis", "ExpressionList", "CloseParenthesis"))
);

//Console.WriteLine(AntlrTools.GenerateAntlrGrammar(grammar, "dreammaker"));

var table = new LrOneTable(grammar);