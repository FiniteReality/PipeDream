using System.IO.Pipelines;
using System.Text;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Syntax;

if (args.Length < 1)
{
    Console.Error.WriteLine("Expected file");
    return -1;
}

var encoding = args.Length < 2
    ? Encoding.UTF8
    : Encoding.GetEncoding(args[1]);

using var file = File.OpenRead(args[0]);
using var transcode = Encoding.CreateTranscodingStream(
    file, encoding, Encoding.UTF8, false);
var reader = PipeReader.Create(transcode);

ReadResult result = default;
while (!result.IsCompleted || !result.Buffer.IsEmpty)
{
    result = await reader.ReadAsync();
    var pos = Lex(result);

    reader.AdvanceTo(pos, result.Buffer.End);
}

return 0;

static SequencePosition Lex(ReadResult result)
{
    var lexer = new Lexer(result.Buffer, result.IsCompleted);
    while (lexer.Lex())
    {
        var token = (lexer.Current as SyntaxToken)!;
        Console.WriteLine($"{token.Kind}");
        foreach (var thing in token.LeadingTrivia)
        {
            var trivia = (thing as SimpleTriviaSyntax)!;
            Console.WriteLine($"    {trivia.Kind}");
            Console.WriteLine($"        '{trivia.Text.Replace("\r", "\\r").Replace("\n", "\\n")}'");
        }

        Console.WriteLine($"    '{token.Text}'");

        foreach (var thing in token.TrailingTrivia)
        {
            var trivia = (thing as SimpleTriviaSyntax)!;
            Console.WriteLine($"    {trivia.Kind}");
            Console.WriteLine($"        '{trivia.Text.Replace("\r", "\\r").Replace("\n", "\\n")}'");
        }
    }

    return lexer.Position;
}