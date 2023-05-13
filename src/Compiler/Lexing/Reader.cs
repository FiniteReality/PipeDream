using System.Buffers;

namespace PipeDream.Compiler.Lexing;

internal ref struct Reader
{
    private readonly bool _isFinalBlock;
    private SequenceReader<byte> _reader;

    private SequencePosition _trackedPosition;

    public Reader(ReadOnlySequence<byte> sequence, bool isFinalBlock)
    {
        _isFinalBlock = isFinalBlock;
        _reader = new(sequence);
    }

    public bool IsSegmentEnd => _reader.End;
    public bool IsStreamEnd => _reader.End && _isFinalBlock;
    public SequencePosition Position => _reader.Position;
    public SequencePosition TrackedPosition => _trackedPosition;

    public bool TryRead(out byte read)
        => _reader.TryRead(out read);

    public readonly bool TryPeek(out byte read)
        => _reader.TryPeek(out read);

    public readonly bool TryPeek(int offset, out byte read)
        => _reader.TryPeek(offset, out read);

    public void StartTracking()
        => _trackedPosition = _reader.Position;

    public void Advance()
        => _ = TryAdvance(1);

    public OperationStatus TryAdvance(int count)
    {
        if (_reader.Remaining < count)
            return OperationStatus.NeedMoreData;

        _reader.Advance(count);
        return OperationStatus.Done;
    }

    public OperationStatus TryGetString(out string? value)
    {
        var sequence = _reader.Sequence.Slice(TrackedPosition, Position);

        if (sequence.Length > int.MaxValue)
        {
            value = null;
            return OperationStatus.DestinationTooSmall;
        }

        return StringUtils.GetUtf8String(sequence, out value)
            ? OperationStatus.Done
            : OperationStatus.InvalidData;
    }

    public readonly long GetTokenWidth()
    {
        var startOffset = _reader.Sequence.GetOffset(_trackedPosition);
        var currentPosition = _reader.Sequence.GetOffset(_reader.Position);
        return currentPosition - startOffset;
    }

    public readonly long GetOffset()
        => _reader.Sequence.GetOffset(_reader.Position);

    public bool Rewind()
        => Rewind(GetTokenWidth());

    public bool Rewind(long bytes)
    {
        if (bytes > 0)
        {
            // Due to a bug in .NET, calling Rewind(0) incorrectly sets a flag
            // indicating that the reader contains more data when this isn't
            // actually the case.
            // see also: https://github.com/dotnet/runtime/issues/68774
            _reader.Rewind(bytes);
            return true;
        }

        return false;
    }

    public void Rewind(SequencePosition position)
    {
        var desiredPosition = _reader.Sequence.GetOffset(position);
        var currentPosition = _reader.Sequence.GetOffset(_reader.Position);
        var delta = currentPosition - desiredPosition;
        if (delta > 0)
            _reader.Rewind(delta);
    }

    public OperationStatus TryAdvancePastAny(
        ReadOnlySpan<byte> validCharacters)
    {
        var longLength = _reader.AdvancePastAny(validCharacters);

        // If we didn't skip any characters for whatever reason.
        if (longLength <= 0)
            return OperationStatus.InvalidData;
        // If we hit the end of the current buffer and there's more data, we
        // should fail now because there could be more valid characters
        // afterwards.
        else if (IsSegmentEnd && !IsStreamEnd)
            return OperationStatus.NeedMoreData;

        // Unchecked here because we already checked earlier
        return OperationStatus.Done;
    }

    public OperationStatus TryReadToAny(ReadOnlySpan<byte> validCharacters)
    {
        if (!_reader.TryReadToAny(out ReadOnlySequence<byte> _,
            validCharacters, advancePastDelimiter: false))
            return OperationStatus.NeedMoreData;

        // If we hit the end of the current buffer and there's more data, we
        // should fail now because there could be more valid characters
        // afterwards.
        else if (IsSegmentEnd && !IsStreamEnd)
            return OperationStatus.NeedMoreData;

        return OperationStatus.Done;
    }
}
