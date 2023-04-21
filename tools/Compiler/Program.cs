using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Channels;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing;
using PipeDream.Compiler.Syntax;

if (args.Length < 1)
{
    Console.Error.WriteLine("Expected file");
    return -1;
}

var encoding = args.Length < 2
    ? Encoding.UTF8
    : Encoding.GetEncoding(args[1]);

var channel = Channel.CreateBounded<SyntaxToken>(64);
var parser = new Parser(channel.Reader);
var cancelToken = CancellationToken.None;

var parseTask = parser.RunAsync(cancelToken).AsTask();
await Task.WhenAll(
    parseTask,
    ProduceTokensAsync(args[0], encoding, channel.Writer, cancelToken))
    .ConfigureAwait(false);

Console.WriteLine(parseTask.Result);

return 0;

static async Task ProduceTokensAsync(string path, Encoding encoding,
    ChannelWriter<SyntaxToken> writer, CancellationToken cancellationToken)
{
    try
    {
        using var file = File.OpenRead(path);
        using var transcode = Encoding.CreateTranscodingStream(
            file, encoding, Encoding.UTF8, false);
        var reader = PipeReader.Create(transcode);

        ReadResult result = default;
        LexerState state = default;
        while (!result.IsCompleted || !result.Buffer.IsEmpty)
        {
            result = await reader.ReadAsync(cancellationToken);
            if (!result.Buffer.IsEmpty)
            {
                var sequence = result.Buffer;
                var lastPosition = sequence.Start;

                while (!sequence.IsEmpty &&
                    await writer.WaitToWriteAsync(cancellationToken))
                {
                    lastPosition = Lex(sequence, result.IsCompleted,
                        writer, ref state);

                    sequence = sequence.Slice(lastPosition);
                }

                reader.AdvanceTo(lastPosition, result.Buffer.End);
            }
        }

        writer.Complete();
    }
    catch (Exception e)
    {
        writer.Complete(e);
    }
}

static SequencePosition Lex(
    ReadOnlySequence<byte> sequence,
    bool isCompleted,
    ChannelWriter<SyntaxToken> writer,
    ref LexerState state)
{
    var lexer = new Lexer(state, sequence, isCompleted);
    while (lexer.Lex())
    {
        var current = lexer.Current
            ?? throw new InvalidOperationException("Should be unreachable");

        if (!writer.TryWrite(current))
            break;
    }

    state = lexer.State;
    return lexer.Position;
}