using System.Diagnostics;
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

var timer = Stopwatch.StartNew();
ReadResult result = default;
LexerState state = default;
while (!result.IsCompleted || !result.Buffer.IsEmpty)
{
    result = await reader.ReadAsync();
    if (!result.Buffer.IsEmpty)
    {
        var pos = Lex(result, ref state);

        reader.AdvanceTo(pos, result.Buffer.End);
    }
}
Console.WriteLine(
    $"Lexing {file.Length} bytes took {timer.ElapsedMilliseconds}ms");

return 0;

static SequencePosition Lex(ReadResult result, ref LexerState state)
{
    var lexer = new Lexer(state, result.Buffer, result.IsCompleted);
    while (lexer.Lex())
    {
        var token = (lexer.Current as SyntaxToken)!;
        /*Console.WriteLine($"{token.Kind}");
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
        }*/
    }

    state = lexer.State;
    return lexer.Position;
}