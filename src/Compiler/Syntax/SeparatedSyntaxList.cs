using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;

namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing a separated list of <see cref="SyntaxNode"/>.
/// </summary>
public readonly record struct SeparatedSyntaxList<T>
    : IReadOnlyList<T>
    where T : SyntaxNode
{
    // Contents look something like this:
    // 0: (SyntaxNode, null)
    // 1: (null, token)
    // 2: (SyntaxNode, null)
    // 3: (null, token)
    // 4: (SyntaxNode, null)

    // So, we can compute node offsets using the formula N * 2:
    // 0 * 2 = 0 -> (SyntaxNode, null)
    // 1 * 2 = 2 -> (SyntaxNode, null)
    // 2 * 2 = 4 -> (SyntaxNode, null)

    // And we can compute token offsets using the formula N * 2 + 1:
    // 0 * 2 + 1 = 1 -> (null, token)
    // 1 * 2 + 1 = 3 -> (null, token)

    private readonly ImmutableArray<(T? item, SyntaxToken? token)> _values;
    private readonly int _numberOfItems;
    private readonly int _numberOfSeparators;

    /// <summary>
    /// Creates an empty <see cref="SyntaxList{T}" />
    /// </summary>
    public SeparatedSyntaxList()
    {
        _values = ImmutableArray.Create<(T?, SyntaxToken?)>();
        _numberOfItems = 0;
        _numberOfSeparators = 0;
    }

    internal SeparatedSyntaxList(
        ImmutableArray<(T?, SyntaxToken?)> values,
        int numberOfItems, int numberOfSeparators)
    {
        _values = values;
        _numberOfItems = numberOfItems;
        _numberOfSeparators = numberOfSeparators;

#if DEBUG
        int countItems = 0, countSeparators = 0;
        foreach (var item in values)
        {
            if (item is (not null, null))
                countItems++;
            else if (item is (null, not null))
                countSeparators++;
            else
                Debug.Fail("Only one of the items should be set");
        }
        Debug.Assert(countItems == _numberOfItems);
        Debug.Assert(countSeparators == _numberOfSeparators);
#endif
    }

    /// <inheritdoc />
    public T this[int index]
        => (index < 0 || index > _numberOfItems)
            ? throw new ArgumentOutOfRangeException(nameof(index))
            : _values[index * 2].item!;

    /// <inheritdoc />
    public int Count => _numberOfItems;

    /// <summary>
    /// Gets the number of separators in this list.
    /// </summary>
    public int SeparatorCount => _numberOfSeparators;

    /// <summary>
    /// Gets the separator at the given index.
    /// </summary>
    /// <param name="index">
    /// The zero-based element of the separator to get.
    /// </param>
    /// <returns>
    /// The separator at the specified index in the read-only list.
    /// </returns>
    public SyntaxToken GetSeparator(int index)
    {
        return (index < 0 || index > _numberOfSeparators)
            ? throw new ArgumentOutOfRangeException(nameof(index))
            : _values[(index * 2) + 1].token!;
    }

    /// <summary>
    /// Returns an enumerator for the contents of the list.
    /// </summary>
    /// <returns>
    /// An enumerator.
    /// </returns>
    public Enumerator GetEnumerator()
        => new(this);

    /// <inheritdoc />
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
        => new EnumerableEnumerator(this);

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
        => new EnumerableEnumerator(this);

    /// <summary>
    /// Defines an enumerator for separated syntax lists.
    /// </summary>
    public struct Enumerator
    {
        private readonly SeparatedSyntaxList<T> _syntaxList;

        private int _index;

        internal Enumerator(SeparatedSyntaxList<T> list)
        {
            _syntaxList = list;
            _index = 0;
        }

        /// <inheritdoc />
        public T Current => _syntaxList[_index];

        /// <inheritdoc />
        public bool MoveNext()
        {
            if (_index >= _syntaxList.Count)
                return false;

            _index++;
            return true;
        }
    }

    private class EnumerableEnumerator : IEnumerator<T>, IEnumerator
    {
        private readonly SeparatedSyntaxList<T> _syntaxList;

        private int _index;

        internal EnumerableEnumerator(SeparatedSyntaxList<T> list)
        {
            _syntaxList = list;
            _index = 0;
        }

        public T Current => _syntaxList[_index];
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_index >= _syntaxList.Count)
                return false;

            _index++;
            return true;
        }

        public void Dispose()
        { /* no-op */ }

        public void Reset()
        { /*no-op */ }
    }
}
