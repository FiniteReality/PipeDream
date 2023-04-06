using System.Xml;
using System.Xml.Serialization;
using PipeDream.Tools.ParserGenerator;

var serializer = new XmlSerializer(typeof(XmlGrammar));
XmlGrammar grammar;
{
    using var stream = File.OpenRead("grammar.xml");
    using var reader = XmlReader.Create(stream, new()
    {
        IgnoreComments = true,
        DtdProcessing = DtdProcessing.Prohibit,
        IgnoreWhitespace = true,
        IgnoreProcessingInstructions = true
    });
    grammar = (XmlGrammar)serializer.Deserialize(reader)!;
    if (grammar == null)
        throw new InvalidOperationException("Failed to parse grammar file");
}

var builtGrammar = Grammar.BuildGrammar(grammar);
var dir = Directory.CreateDirectory("Generated");
builtGrammar.WriteCode(
    location => new StreamWriter(File.OpenWrite($"Generated/{location}.cs")),
    @namespace: "PipeDream.Compiler.Syntax",
    @usings: Enumerable.Empty<string>());
