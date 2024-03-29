using System.Collections;
using System.Collections.Immutable;
using System.Text;

namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record containing a list of <see cref="SyntaxNode"/>.
/// </summary>
public readonly record struct SyntaxList<T>
    : IReadOnlyList<T>
    where T : SyntaxNode
{
    private readonly ImmutableArray<T> _values;

    /// <summary>
    /// Creates an empty <see cref="SyntaxList{T}" />
    /// </summary>
    public SyntaxList()
    {
        _values = ImmutableArray.Create<T>();
    }

    internal SyntaxList(T value)
    {
        _values = ImmutableArray.Create(value);
    }

    internal SyntaxList(ImmutableArray<T> values)
    {
        _values = values;
    }

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

    /// <inheritdoc />
    public override string ToString()
    {
        var builder = new StringBuilder()
            .Append("SyntaxList")
            .Append('<')
            .Append(typeof(T).Name)
            .Append('>')
            .Append(" { ");

        var first = true;
        foreach (var item in this)
        {
            if (!first)
                _ = builder.Append(", ");

            _ = builder
                .Append(item.ToString())
                .Append(' ');
            first = false;
        }

        return builder
            .Append('}')
            .ToString();
    }

    /// <summary>
    /// Defines an enumerator for syntax lists.
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
