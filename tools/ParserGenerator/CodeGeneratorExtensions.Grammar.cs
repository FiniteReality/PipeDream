namespace PipeDream.Tools.ParserGenerator;

internal static partial class CodeGeneratorExtensions
{
    public static void WriteCode(
        this Grammar grammar,
        Func<string, TextWriter> openForWriting,
        string @namespace,
        IEnumerable<string> usings)
    {
        WriteFile(
            openForWriting,
            "SyntaxKind",
            @namespace,
            @usings,
            static (w, g) => WriteKinds(w, g),
            grammar);

        foreach (var item in grammar.Items)
        {
            WriteFile(
                openForWriting,
                item.Key,
                @namespace,
                usings,
                static (w, i) => WriteItem(w, i),
                item.Value);
        }

        static bool WriteUsings(TextWriter writer,
            IEnumerable<string> usings)
        {
            var anyUsings = false;
            foreach (var @using in usings)
            {
                anyUsings = true;
                writer.WriteLine($"using {@using};");
            }

            return anyUsings;
        }

        static void WriteFile<T>(
            Func<string, TextWriter> openForWriting,
            string file,
            string @namespace,
            IEnumerable<string> usings,
            Action<TextWriter, T> writeAction,
            T state)
        {
            using var writer = openForWriting(file);

            if (WriteUsings(writer, usings))
                writer.WriteLine();

            writer.WriteLine($"namespace {@namespace};");
            writer.WriteLine();

            writeAction(writer, state);
        }
    }
}