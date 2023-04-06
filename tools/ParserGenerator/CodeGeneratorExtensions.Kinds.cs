namespace PipeDream.Tools.ParserGenerator;

internal static partial class CodeGeneratorExtensions
{
    private static void WriteKinds(TextWriter writer, Grammar grammar)
    {
        writer.WriteLine(@"
/// <summary>
/// Defines an enum containing all possible syntax kinds.
/// </summary>");
        writer.WriteLine("public enum SyntaxKind");
        writer.WriteLine("{");

        var firstGroup = true;
        foreach (var group in grammar.Kinds.Values
            .GroupBy(x => x.Group)
            .OrderBy(x => x.Key))
        {
            if (!firstGroup)
                writer.WriteLine();

            writer.WriteLine($"    // Group: {group.Key}");
            firstGroup = false;

            var firstItem = true;
            foreach (var kind in group.OrderBy(x => x.Name))
            {
                if (!firstItem)
                    writer.WriteLine();

                if (kind.DocumentationComment != null)
                    WriteXmlComment(writer, kind.DocumentationComment,
                    "    /// ");

                writer.WriteLine($"    {kind.Name},");
                firstItem = false;
            }
        }

        writer.WriteLine("}");
    }
}