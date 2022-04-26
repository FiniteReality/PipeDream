using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Text;
using PipeDream.Compiler.Lexing;

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
LexerState state = new();
while (!result.IsCompleted || !result.Buffer.IsEmpty)
{
    result = await reader.ReadAsync();
    (var pos, state) = Lex(result);

    if (state.HasErrors)
        break;

    reader.AdvanceTo(pos, result.Buffer.End);
}

Console.WriteLine();

if (state.HasErrors)
    foreach (var error in state.Errors)
        Debug.WriteLine($"Parse error: {error.Message}");

(SequencePosition, LexerState) Lex(ReadResult result)
{
    var lexer = new Lexer(result.Buffer, result.IsCompleted, state);

    while (!lexer.End && lexer.Lex())
    {
        Debug.WriteLine(lexer.CurrentToken);

        // This shouldn't need to happen but oh well
        var bufferOffset = checked((int)result.Buffer.GetOffset(result.Buffer.Start));
        var absoluteLength = checked((int)(bufferOffset + result.Buffer.Length));
        var (absoluteOffset, length) = lexer.CurrentToken.Span.GetOffsetAndLength(absoluteLength);
        var relativeOffset = absoluteOffset - bufferOffset;
        var region = result.Buffer.Slice(relativeOffset, length);
        Console.Write(Encoding.UTF8.GetString(region.ToArray()));
    }

    Console.Out.Flush();

    return (lexer.Position, lexer.CurrentState);
}

return 0;