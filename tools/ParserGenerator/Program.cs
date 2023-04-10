using System.Xml;
using System.Xml.Serialization;
using PipeDream.Tools.ParserGenerator;

var grammar = MergeGrammars(
    ReadGrammar("grammar/kinds/tokens.xml"),
    ReadGrammar("grammar/kinds/trivia.xml"),
    ReadGrammar("grammar/kinds/keywords.xml"),
    ReadGrammar("grammar/kinds/directives.xml"),
    ReadGrammar("grammar/kinds/expressions.xml"),
    ReadGrammar("grammar/kinds/statements.xml"),
    ReadGrammar("grammar/kinds/declarations.xml"),

    ReadGrammar("grammar/nodes/base.xml"),
    ReadGrammar("grammar/nodes/token.xml"),
    ReadGrammar("grammar/nodes/trivia.xml"),
    ReadGrammar("grammar/nodes/expressions.xml"),
    ReadGrammar("grammar/nodes/statements.xml"),
    ReadGrammar("grammar/nodes/declarations.xml"));

var location = args[0]
    ?? throw new InvalidOperationException("Expected location");

var builtGrammar = Grammar.BuildGrammar(grammar);
_ = Directory.CreateDirectory(location);
builtGrammar.WriteCode(
    file => {
        var f = File.OpenWrite(Path.Combine(location, $"{file}.cs"));
        f.SetLength(0);
        return new StreamWriter(f);
    },
    @namespace: "PipeDream.Compiler.Syntax",
    @usings: Enumerable.Empty<string>());

static XmlGrammar MergeGrammars(params XmlGrammar[] grammars)
    => new(grammars.SelectMany(
        x => x.Elements ?? Array.Empty<XmlGrammarElement>())
        .ToArray());

static XmlGrammar ReadGrammar(string path)
{
    using var stream = File.OpenRead(path);
    using var reader = XmlReader.Create(stream, new()
    {
        IgnoreComments = true,
        DtdProcessing = DtdProcessing.Parse,
        IgnoreWhitespace = true,
        IgnoreProcessingInstructions = true
    });
    try
    {
        return (XmlGrammar)Serializer.Deserialize(reader)!;
    }
    catch (Exception e)
    {
        throw new InvalidOperationException(
            $"Failed to parse grammar file '{path}'", e);
    }
}

public partial class Program
{
    private static readonly XmlSerializer Serializer = new(typeof(XmlGrammar));
}