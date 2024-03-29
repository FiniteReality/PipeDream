namespace PipeDream.Tools.ParserGenerator;

internal static partial class CodeGeneratorExtensions
{
    private static void WriteKinds(TextWriter writer, Grammar grammar)
    {
        WriteKinds(writer, grammar);
        writer.WriteLine();
        WriteGroups(writer, grammar);
        writer.WriteLine();
        WriteGroupLookup(writer, grammar);

        static void WriteKinds(TextWriter writer, Grammar grammar)
        {
            writer.WriteLine(@"
/// <summary>
/// Defines an enum containing all possible syntax kinds.
/// </summary>");
            writer.WriteLine("public enum SyntaxKind");
            writer.WriteLine("{");

            var firstGroup = true;
            foreach (var group in grammar.Kinds
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

        static void WriteGroups(TextWriter writer, Grammar grammar)
        {
            writer.WriteLine("internal enum SyntaxGroup : byte");
            writer.WriteLine("{");

            var firstGroup = true;
            foreach (var kind in grammar.Kinds
                .DistinctBy(x => x.Group)
                .OrderBy(x => x.Group))
            {
                if (!firstGroup)
                    writer.WriteLine();

                writer.WriteLine($"    {kind.Group},");
                firstGroup = false;
            }

            writer.WriteLine("}");
        }

        static void WriteGroupLookup(TextWriter writer, Grammar grammar)
        {
            writer.WriteLine(
@"internal static class SyntaxKindExtensions
{
    public static SyntaxGroup GetGroup(this SyntaxKind kind)
        => (int)kind switch
        {");

            var lastCount = 0;
            foreach (var (count, Key) in grammar.Kinds
                .GroupBy(x => x.Group)
                .OrderBy(x => x.Key)
                .Select(x => (x.Count(), x.Key)))
            {
                writer.WriteLine(
$"            < {lastCount + count} => SyntaxGroup.{Key},");
                lastCount += count;
            }

            writer.WriteLine(
@"            _ => throw new InvalidOperationException(""Unreachable"")");

            writer.WriteLine("        };");
            writer.WriteLine("}");
        }
    }
}
