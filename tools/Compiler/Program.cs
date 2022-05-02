using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Text;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing;

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
LexerState lexState = new();
ParserState parseState = new();
while (!result.IsCompleted || !result.Buffer.IsEmpty)
{
    result = await reader.ReadAsync();
    var pos = Lex(result, ref lexState, ref parseState);

    if (lexState.HasErrors || parseState.HasErrors)
        break;

    reader.AdvanceTo(pos, result.Buffer.End);
}

if (lexState.HasErrors)
    foreach (var error in lexState.Errors)
        Console.Error.WriteLine($"{args[0]}({error.Span}): {error.Message}");

if (parseState.HasErrors)
    foreach (var error in parseState.Errors)
        Console.Error.WriteLine($"{args[0]}({error.Span}): {error.Message}");

static SequencePosition Lex(ReadResult result,
    ref LexerState lexState, ref ParserState parseState)
{
    var lexer = new Lexer(result.Buffer, result.IsCompleted, lexState);
    var parser = new Parser(parseState);

    while (lexer.Lex())
    {
        bool queueFailed = false;
        do
        {
            if (!parser.Queue(lexer.CurrentToken))
            {
                queueFailed = true;
                break;
            }
        }
        while (lexer.Lex());

        // Parse as much as possible based on the queued tokens
        if (parser.Parse())
        {
            // Assuming we consumed some tokens, try and queue the token we failed
            // to parse
            if (queueFailed && !parser.Queue(lexer.CurrentToken))
                throw new InvalidOperationException(
                    "Somehow failed to enqueue token");
        }
    }

    lexState = lexer.CurrentState;
    parseState = parser.CurrentState;

    return lexer.Position;
}

return 0;
