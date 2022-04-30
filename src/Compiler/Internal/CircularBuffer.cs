namespace PipeDream.Compiler.Internal;

internal class CircularBuffer<T>
{
    private readonly T[] _buffer;

    private int _startIndex;
    private int _endIndex;

    public CircularBuffer(int size)
    {
        _buffer = new T[size];
        _startIndex = 0;
        _endIndex = 0;
    }

    public bool Enqueue(T item)
    {
        var insertIndex = _endIndex + 1;

        if (insertIndex >= _buffer.Length)
            insertIndex -= _buffer.Length;

        if (insertIndex <= _startIndex)
            return false;

        _buffer[insertIndex] = item;
        _endIndex = insertIndex;
        return true;
    }

    public bool TryDequeue(out T? item)
    {
        if (TryPeekCore(out item, out var index))
        {
            _startIndex = index;
            return true;
        }

        return false;
    }

    public bool TryPeek(out T? item)
        => TryPeekCore(out item, out _);

    private bool TryPeekCore(out T? item, out int index)
    {
        var popIndex = _startIndex + 1;

        if (popIndex >= _buffer.Length)
            popIndex -= _buffer.Length;

        if (popIndex >= _endIndex)
        {
            item = default;
            index = -1;
            return false;
        }

        item = _buffer[popIndex];
        index = popIndex;
        return true;
    }
}