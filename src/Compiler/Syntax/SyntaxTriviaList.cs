using System.Collections;
using System.Collections.Immutable;

namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing a list of <see cref="SyntaxTrivia"/>.
/// </summary>
public readonly record struct SyntaxTriviaList
    : IReadOnlyList<SyntaxTrivia>
{
    private readonly ImmutableArray<SyntaxTrivia> _values;

    /// <inheritdoc />
    public SyntaxTrivia this[int index] => _values[index];

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
    IEnumerator<SyntaxTrivia> IEnumerable<SyntaxTrivia>.GetEnumerator()
        => ((IEnumerable<SyntaxTrivia>)_values).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)_values).GetEnumerator();

    /// <summary>
    /// An array enumerator.
    /// </summary>
    public struct Enumerator
    {
        private ImmutableArray<SyntaxTrivia>.Enumerator _backingEnumerator;

        internal Enumerator(ImmutableArray<SyntaxTrivia>.Enumerator backingEnumerator)
        {
            _backingEnumerator = backingEnumerator;
        }

        /// <inheritdoc />
        public SyntaxTrivia Current => _backingEnumerator.Current;

        /// <inheritdoc />
        public bool MoveNext() => _backingEnumerator.MoveNext();
    }
}