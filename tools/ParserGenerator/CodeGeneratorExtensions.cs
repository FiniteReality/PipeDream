using System.Xml;
using System.Xml.Linq;

namespace PipeDream.Tools.ParserGenerator;

internal static partial class CodeGeneratorExtensions
{
    private static readonly XmlWriterSettings WriterSettings = new()
    {
        OmitXmlDeclaration = true,
        NewLineHandling = NewLineHandling.Replace,
        ConformanceLevel = ConformanceLevel.Fragment,
        NewLineChars = "\n",
        Indent = true,
        IndentChars = ""
    };

    private static void WriteXmlComment(TextWriter writer, XElement comment,
            string prefix)
    {
        comment.TrimWhitespace();
        using var childWriter = new PrefixedTextWriter(prefix, writer);
        using (var xWriter = XmlWriter.Create(childWriter, WriterSettings))
            foreach (var child in comment.Elements())
                child.WriteTo(xWriter);
        writer.WriteLine();
    }
}