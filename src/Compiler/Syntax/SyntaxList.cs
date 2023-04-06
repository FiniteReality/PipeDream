using System.Collections;
using System.Collections.Immutable;

namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing a list of <see cref="SyntaxNode"/>.
/// </summary>
public readonly record struct SyntaxList<T>
    : IReadOnlyList<T>
    where T : SyntaxNode
{
    private readonly ImmutableArray<T> _values;

    /// <inheritdoc />
    public T this[int index] => _values[index];

    /// <inheritdoc />
    public int Count => _values.Length;

    /// <summary>
    /// Returns an enumerator for the contents of the list.
    /// </summary>
    /// <returns>
    /// An enumerator.
    /// </returns>
    public Enumerator GetEnumerator()
        => new(_values.GetEnumerator());

    /// <inheritdoc />
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
        => ((IEnumerable<T>)_values).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)_values).GetEnumerator();

    /// <summary>
    /// An array enumerator.
    /// </summary>
    public struct Enumerator
    {
        private ImmutableArray<T>.Enumerator _backingEnumerator;

        internal Enumerator(ImmutableArray<T>.Enumerator backingEnumerator)
        {
            _backingEnumerator = backingEnumerator;
        }

        /// <inheritdoc />
        public T Current => _backingEnumerator.Current;

        /// <inheritdoc />
        public bool MoveNext() => _backingEnumerator.MoveNext();
    }
}