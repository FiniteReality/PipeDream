namespace PipeDream.Tools.ParserGenerator;

internal static partial class CodeGeneratorExtensions
{
    private static void WriteVisitor(
        TextWriter writer, Grammar grammar)
    {
        writer.WriteLine("public abstract partial class SyntaxVisitor");
        writer.WriteLine('{');

        var first = true;
        foreach (var item in grammar.Items)
        {
            if (!first)
                writer.WriteLine();

            WriteVisitorMethod(writer, item);
            first = false;
        }

        writer.WriteLine('}');
    }

    private static void WriteVisitorMethod(TextWriter writer, GrammarItem item)
    {
        writer.Write(
@"    /// <summary>
    /// Visits the given <see cref=""");
        writer.Write(item.Type);
        writer.Write(@"""/>.
    /// </summary>
    /// <param name=""value"">
    /// The <see cref=""");
        writer.Write(item.Type);
        writer.WriteLine(@"""/> to visit.
    /// </param>");
        writer.Write("    protected internal virtual void Visit");
        writer.Write(item.Type);
        writer.Write('(');
        writer.Write(item.Type);
        writer.WriteLine(" value)");
        writer.WriteLine("    {");

        if (item.BaseType is GrammarItem baseType)
        {
            writer.Write("        Visit");
            writer.Write(baseType.Type);
            writer.WriteLine("(value);");
        }

        foreach (var member in item.Members)
        {
            var type = member.Type.TrimEnd('?');
            if (member.Type.StartsWith("SyntaxList", StringComparison.OrdinalIgnoreCase))
            {
                writer.Write("        VisitSyntaxList(value.");
                writer.Write(member.Name);
                writer.WriteLine(");");
            }
            else if (member.Type.StartsWith("SeparatedSyntaxList", StringComparison.OrdinalIgnoreCase))
            {
                writer.Write("        VisitSeparatedSyntaxList(value.");
                writer.Write(member.Name);
                writer.WriteLine(");");
            }
            else if (member.Type.EndsWith("Syntax", StringComparison.OrdinalIgnoreCase)
                || member.Type.EndsWith("Token", StringComparison.OrdinalIgnoreCase))
            {
                writer.Write("        Visit(value.");
                writer.Write(member.Name);
                writer.WriteLine(");");
            }
            else if (member.Type.EndsWith("Syntax?", StringComparison.OrdinalIgnoreCase)
                || member.Type.EndsWith("Token?", StringComparison.OrdinalIgnoreCase))
            {
                writer.Write("        if (value.");
                writer.Write(member.Name);
                writer.WriteLine(" != null)");
                writer.Write("            Visit(value.");
                writer.Write(member.Name);
                writer.WriteLine(");");
            }
        }

        writer.WriteLine("    }");
    }
}
