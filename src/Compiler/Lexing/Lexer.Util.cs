using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Threading.Channels;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

internal ref partial struct Lexer
{
    internal static async ValueTask LexAsync(
        PipeReader reader,
        ChannelWriter<SyntaxToken> writer,
        CancellationToken cancellationToken)
    {
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
