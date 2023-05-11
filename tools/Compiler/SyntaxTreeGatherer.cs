using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Threading.Channels;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Frontend;

internal sealed class SyntaxTreeGatherer
{
    private readonly IncludeVisitor _visitor
        = new();
    private readonly Dictionary<string, SyntaxNode?> _parsedUnits
        = new();
    private readonly Queue<string> _parseQueue
        = new();

    private readonly string _rootDirectory;

    public SyntaxTreeGatherer(string entryPoint)
    {
        if (Path.GetDirectoryName(entryPoint) is not string root)
            throw new ArgumentException(
                "Unable to get root directory from path",
                nameof(entryPoint));

        // TODO: issue a warning for non-portable paths?
        _visitor.FileIncluded += f => _parseQueue.Enqueue(f.Replace('\\', '/'));
        _rootDirectory = root;

        _parseQueue.Enqueue(
            Path.GetFileName(entryPoint));
    }

    public async ValueTask GatherAsync(CancellationToken cancellationToken)
    {
        while (_parseQueue.TryDequeue(out var path))
        {
            path = Path.Combine(_rootDirectory, path);
            if (!_parsedUnits.ContainsKey(path))
            {
                var node = await ParseFileAsync(path, cancellationToken)
                    .ConfigureAwait(false);

                Debug.Assert(node != null);

                _parsedUnits.Add(path, node);
                _visitor.Visit(node);
            }
        }
    }

    private static async ValueTask<SyntaxNode?> ParseFileAsync(string path,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(path);
        using var token = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken);
        var channel = Channel.CreateBounded<SyntaxToken>(1024);
        var parser = new Parser(channel.Reader);

        var lexTask = CancelLexErrorAsync(token,
                LexFileAsync(path, channel.Writer, token.Token))
                .ConfigureAwait(false);
        var parseTask = CancelParseErrorAsync(token,
            parser.RunAsync(token.Token))
            .ConfigureAwait(false);

        await lexTask;
        return await parseTask;

        // We need duplicate definitions here since ValueTask and ValueTask<T>
        // have no common type that we could constrain a generic method to
        static async ValueTask CancelLexErrorAsync(
            CancellationTokenSource source, ValueTask task)
        {
            try
            {
                await task;
            }
            catch (Exception)
            {
                source.Cancel();
                throw;
            }
        }

        static async ValueTask<SyntaxNode?> CancelParseErrorAsync(
            CancellationTokenSource source, ValueTask<SyntaxNode?> task)
        {
            try
            {
                return await task;
            }
            catch (Exception)
            {
                source.Cancel();
                throw;
            }
        }
    }

    private static async ValueTask LexFileAsync(
        string path,
        ChannelWriter<SyntaxToken> writer,
        CancellationToken cancellationToken)
    {
        // TODO: detect encoding and transcode to UTF-8 if necessary
        using var file = File.OpenRead(path);
        var reader = PipeReader.Create(file);

        try
        {
            ReadResult result = default;
            LexerState state = default;
            while (!result.IsCompleted || !result.Buffer.IsEmpty)
            {
                result = await reader.ReadAsync(cancellationToken);
                var sequence = result.Buffer;

                while (await writer.WaitToWriteAsync(cancellationToken))
                {
                    var status = Lex(
                        ref sequence,
                        result.IsCompleted,
                        writer,
                        ref state);

                    if (status is
                        OperationStatus.NeedMoreData or
                        OperationStatus.InvalidData)
                        break;
                }

                reader.AdvanceTo(sequence.Start, sequence.End);
            }

            writer.Complete();
        }
        catch (Exception e)
        {
            e.Data["File"] = path;
            writer.Complete(e);
            throw;
        }

        static OperationStatus Lex(
            ref ReadOnlySequence<byte> sequence,
            bool isCompleted,
            ChannelWriter<SyntaxToken> writer,
            ref LexerState state)
        {
            var lexer = new Lexer(state, sequence, isCompleted);
            var lastPosition = lexer.Position;
            var status = lexer.Lex();
            while (status == OperationStatus.Done)
            {
                var current = lexer.Current
                    ?? throw new InvalidOperationException(
                        "Should be unreachable");

                Debug.Assert(current.Kind != SyntaxKind.BadToken,
                    $"Got a bad token {current.Text}");

                if (!writer.TryWrite(current))
                {
                    // If we fail to write the produced token, we'll need to
                    // re-parse the token after waiting to read again.
                    // Hopefully this doesn't happen too often that it becomes
                    // an issue.
                    sequence = sequence.Slice(lastPosition);
                    return OperationStatus.DestinationTooSmall;
                }

                lastPosition = lexer.Position;
                state = lexer.State;
                status = lexer.Lex();
            }

            state = lexer.State;
            sequence = sequence.Slice(lexer.Position);
            return status;
        }
    }
}
