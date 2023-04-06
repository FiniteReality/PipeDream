using System.Xml;
using System.Xml.Serialization;
using PipeDream.Tools.ParserGenerator;

var grammar = MergeGrammars(
    ReadGrammar("grammar/kinds/tokens.xml"),
    ReadGrammar("grammar/kinds/keywords.xml"),
    ReadGrammar("grammar/kinds/expressions.xml"),
    ReadGrammar("grammar/kinds/blocks.xml"),
    ReadGrammar("grammar/nodes/base.xml"),
    ReadGrammar("grammar/nodes/expressions.xml"),
    ReadGrammar("grammar/nodes/blocks.xml"));

var builtGrammar = Grammar.BuildGrammar(grammar);
var dir = Directory.CreateDirectory("Generated");
builtGrammar.WriteCode(
    location => new StreamWriter(File.OpenWrite($"Generated/{location}.cs")),
    @namespace: "PipeDream.Compiler.Syntax",
    @usings: Enumerable.Empty<string>());

static XmlGrammar MergeGrammars(params XmlGrammar[] grammars)
    => new(grammars.SelectMany(x => x.Elements).ToArray());

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