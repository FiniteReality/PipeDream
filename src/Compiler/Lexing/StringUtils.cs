using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PipeDream.Compiler.Lexing;

internal static class StringUtils
{
    public static bool GetUtf8String(ReadOnlySequence<byte> sequence,
        [NotNullWhen(true)] out string? value)
    {
        // If the string is too big, fail early rather than attempting to
        // do anything.
        if (sequence.Length > int.MaxValue)
        {
            value = null;
            return false;
        }

        // If the sequence is a single segment, we can use the cheap method.
        if (sequence.IsSingleSegment)
        {
            value = Encoding.UTF8.GetString(sequence.FirstSpan);
            return true;
        }
        // If it's short enough, allocating a temporary stack buffer is faster.
        else if (sequence.Length <= 256)
        {
            Span<byte> tempBuffer = stackalloc byte[256];
            sequence.CopyTo(tempBuffer);
            // Unchecked cast is safe here since we're checking above
            value = Encoding.UTF8.GetString(
                tempBuffer[..unchecked((int)sequence.Length)]);
            return true;
        }
        // Otherwise, fall back to the more expensive transcode method.
        else
        {
            value = Encoding.UTF8.GetString(sequence);
            return true;
        }
    }
}
