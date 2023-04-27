using System.Buffers;
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

// TODO: there's a race condition when using a bounded channel:
// writing can fail as the channel is full and we don't correctly add missed
// tokens when re-entering the Lex() loop.
var channel = Channel.CreateUnbounded<SyntaxToken>();
var parser = new Parser(channel.Reader);
var cancelToken = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) => {
    e.Cancel = true;
    cancelToken.Cancel();
};

try
{
    var parseTask = parser.RunAsync(cancelToken.Token).AsTask();
    await Task.WhenAll(
        parseTask,
        ProduceTokensAsync(args[0], encoding, channel.Writer, cancelToken.Token))
        .ConfigureAwait(false);

    if (parseTask.Result != null)
    {
        var visitor = new Visitor();
        visitor.Visit(parseTask.Result);
    }
}
catch (Exception e)
{
    Console.WriteLine("Uncaught exception");
    Console.WriteLine(e);
}

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
                    var status = Lex(sequence, result.IsCompleted,
                        writer, ref state, out lastPosition);

                    sequence = sequence.Slice(lastPosition);

                    if (status == OperationStatus.NeedMoreData)
                        break;
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

static OperationStatus Lex(
    ReadOnlySequence<byte> sequence,
    bool isCompleted,
    ChannelWriter<SyntaxToken> writer,
    ref LexerState state,
    out SequencePosition position)
{
    var lexer = new Lexer(state, sequence, isCompleted);
    var status = lexer.Lex();
    while (status == OperationStatus.Done)
    {
        var current = lexer.Current
            ?? throw new InvalidOperationException("Should be unreachable");

        if (!writer.TryWrite(current))
            break;

        status = lexer.Lex();
    }

    state = lexer.State;
    position = lexer.Position;
    return status;
}

internal class Visitor : SyntaxVisitor
{
    private int _indentation = -1;

    protected override void BeforeVisit(SyntaxNode value)
    {
        foreach (var leading in value.LeadingTrivia)
        {
            Visit(leading);
        }

        _indentation++;
    }

    protected override void AfterVisit(SyntaxNode value)
    {
        foreach (var trailing in value.TrailingTrivia)
            Visit(trailing);

        _indentation--;
    }

    protected internal override void VisitSyntaxNode(SyntaxNode value)
    {
        Console.Write(new string('|', _indentation));
        Console.Write("- ");
        Console.Write(value.Kind);
        Console.Write(" (");
        Console.Write(value.GetType().Name);
        Console.WriteLine(')');
    }

    protected internal override void VisitSyntaxToken(SyntaxToken value)
    {
        Console.Write(new string('|', _indentation));
        Console.Write("- ");
        Console.Write(value.Kind);
        Console.Write(" '");
        Console.Write(value.Text.Replace("\n", "\\n"));
        Console.WriteLine('\'');
    }

    protected internal override void VisitSimpleTriviaSyntax(SimpleTriviaSyntax value)
    {
        Console.Write(new string('|', _indentation));
        Console.Write("- ");
        Console.Write(value.Kind);
        Console.Write(" '");
        Console.Write(value.Text.Replace("\n", "\\n"));
        Console.WriteLine('\'');
    }
}
