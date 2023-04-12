namespace PipeDream.Tools.ParserGenerator;

internal static partial class CodeGeneratorExtensions
{
    private static string CamelCase(string original)
        => string.IsNullOrWhiteSpace(original)
            ? original
            : string.Create(original.Length, original,
                static (span, original) => {
                    original.AsSpan().CopyTo(span);
                    span[0] = char.ToLowerInvariant(span[0]);
                });

    private static void WriteItem(
        TextWriter writer,
        GrammarItem item)
    {
        if (item.DocumentationComment != null)
            WriteXmlComment(writer, item.DocumentationComment, "/// ");

        WriteTypeDeclaration(writer, item);

        writer.WriteLine("{");

        if (item.Kinds.Count > 0)
        {
            WriteValidateMethod(writer, "Kind", item.Kinds);
            writer.WriteLine();
        }

        bool first = true;
        foreach (var member in item.Members)
        {
            if (!first)
                writer.WriteLine();
            first = false;

            WriteMember(writer, member);
        }

        writer.WriteLine("}");

        static void WriteTypeDeclaration(TextWriter writer, GrammarItem item)
        {
            writer.Write("public ");
            if (item is AbstractGrammarItem or RootGrammarItem)
                writer.Write("abstract ");
            else if (item is SealedGrammarItem)
                writer.Write("sealed ");

            writer.Write("partial record ");
            WriteConstructor(writer, item,
                "    ",
                m => $"{m.Type} {m.Name}");

            if (item.BaseType is GrammarItem baseType)
            {
                writer.Write($"    : ");

                if (item.Kinds.Count > 0)
                    WriteConstructor(writer, baseType,
                        "        ",
                        m => m.Name == "Kind"
                            ? $"{m.Name}: ValidateKind({m.Name}, nameof({m.Name}))"
                            : item.Members.Any(x => x.Name == m.Name)
                                ? $"{m.Name}: Validate{m.Name}({m.Name}, nameof({m.Name}))"
                                : $"{m.Name}: {m.Name}");
                else
                    WriteConstructor(writer, baseType,
                        "        ",
                        m => item.Members.Any(x => x.Name == m.Name)
                            ? $"{m.Name}: Validate{m.Name}({m.Name}, nameof({m.Name}))"
                            : $"{m.Name}: {m.Name}");
            }
        }

        static void WriteConstructor(TextWriter writer,
            GrammarItem item,
            string indentation,
            Func<Member, string> memberSelector,
            bool includeGrandparentMembers = true)
        {
            writer.Write($"{item.Type}(");

            var firstMember = true;
            foreach (var member in GetSetOfAllMembers(item,
                includeGrandparentMembers))
            {
                if (!firstMember)
                    writer.WriteLine(",");
                else
                    writer.WriteLine();

                firstMember = false;
                writer.Write($"{indentation}{memberSelector(member)}");
            }

            writer.WriteLine(")");
        }

        static IEnumerable<Member> GetSetOfAllMembers(
            GrammarItem item,
            bool recursive = true)
        {
            foreach (var member in item.Members)
                yield return member;

            if (item.BaseType is GrammarItem baseType)
                foreach (var member in recursive
                    ? GetSetOfAllMembers(baseType)
                    : baseType.Members)
                {
                    if (item.Members.All(x => x.Name != member.Name))
                        yield return member;
                }
        }

        static void WriteMember(TextWriter writer, Member member)
        {
            if (member.Kinds.Count == 0)
            {
                if (member.DocumentationComment != null)
                    WriteXmlComment(writer,
                        member.DocumentationComment,
                        "    /// ");

                writer.Write("    public ");
                if (member.Virtual)
                    writer.Write("virtual ");
                writer.Write(member.Type);
                writer.Write($" {member.Name} ");
                writer.Write("{ get; init; } = ");
                writer.WriteLine($"{member.Name};");
            }
            else if (member.Override)
            {
                if (member.DocumentationComment != null)
                    WriteXmlComment(writer,
                        member.DocumentationComment,
                        "    /// ");

                writer.WriteLine(
$"    public override {member.Type} {member.Name}\n" +
"    {\n" +
$"        get => base.{member.Name};\n" +
$"        init => base.{member.Name} = Validate{member.Name}(value, nameof({member.Name}));\n" +
"    }");
                writer.WriteLine();
                WriteValidateMethod(writer,
                    member.Name,
                    member.Kinds,
                    member.Type);
            }
            else
            {
                var backingField = $"_{CamelCase(member.Name)}";
                writer.WriteLine(
$"    private {member.Type} {backingField} = " +
$"Validate{member.Name}({member.Name}, nameof({member.Name}));");

                writer.WriteLine();

                if (member.DocumentationComment != null)
                    WriteXmlComment(writer,
                        member.DocumentationComment,
                        "    /// ");

                writer.Write($"    public ");
                if (member.Virtual)
                    writer.Write("virtual ");
                writer.Write(member.Type);
                writer.WriteLine($" {member.Name}");
    
                writer.WriteLine(
"    {\n" +
$"        get => {backingField};\n" +
$"        init => {backingField} = Validate{member.Name}(value, nameof({member.Name}));\n" +
"    }");
                writer.WriteLine();
                WriteValidateMethod(writer,
                    member.Name,
                    member.Kinds,
                    member.Type);
            }
        }

        static void WriteValidateMethod(TextWriter writer,
            string name,
            IReadOnlyCollection<Kind> kinds,
            string? type = null)
        {
            if (type != null)
                writer.WriteLine(
$"    private static {type} Validate{name}({type} value, string paramName)\n" +
$"        => value.Kind switch");
            else
                writer.WriteLine(
$"    private static SyntaxKind Validate{name}(SyntaxKind value, string paramName)\n" +
$"        => value switch");
            writer.WriteLine("        {");

            var firstKind = true;
            foreach (var kind in kinds)
            {
                if (!firstKind)
                    writer.WriteLine(" or");
                firstKind = false;
                writer.Write($"            SyntaxKind.{kind.Name}");
            }
            if (!firstKind)
            {
                writer.WriteLine();
                writer.WriteLine("                => value,");
            }
            else
            {
                writer.WriteLine(" => value,");
            }

            writer.WriteLine(
$"            _ => throw new ArgumentException(\n" +
"                $\"The kind '{value}' is not a supported kind.\",\n" +
"                paramName)\n" +
"        };");
        }
    }
}