namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a class containing common extension methods for
/// <see cref="SyntaxList{T}" />.
/// </summary>
public static class SyntaxListExtensions
{
    /// <summary>
    /// Appends an item to the syntax list.
    /// </summary>
    /// <param name="list">
    /// The list to append an item to.
    /// </param>
    /// <param name="value">
    /// The value to add.
    /// </param>
    /// <typeparam name="T">
    /// The type of item to add.
    /// </typeparam>
    /// <returns>
    /// A <see cref="SyntaxList{T}" /> with the given <paramref name="value"/>
    /// added.
    /// </returns>
    public static SyntaxList<T> Prepend<T>(in this SyntaxList<T> list, T value)
        where T : SyntaxNode
    {
        var builder = new SyntaxListBuilder<T>(list.Count + 1);

        builder.Add(value);

        foreach (var item in list)
            builder.Add(item);

        return builder.Build();
    }

    /// <summary>
    /// Appends an item to the syntax list.
    /// </summary>
    /// <param name="list">
    /// The list to append an item to.
    /// </param>
    /// <param name="value">
    /// The value to add.
    /// </param>
    /// <typeparam name="T">
    /// The type of item to add.
    /// </typeparam>
    /// <returns>
    /// A <see cref="SyntaxList{T}" /> with the given <paramref name="value"/>
    /// added.
    /// </returns>
    public static SyntaxList<T> Append<T>(in this SyntaxList<T> list, T value)
        where T : SyntaxNode
    {
        var builder = new SyntaxListBuilder<T>(list.Count + 1);

        foreach (var item in list)
            builder.Add(item);

        builder.Add(value);

        return builder.Build();
    }

    /// <summary>
    /// Concatenates two syntax lists.
    /// </summary>
    /// <param name="list">
    /// The first list to concatenate.
    /// </param>
    /// <param name="value">
    /// The second list to concatenate.
    /// </param>
    /// <typeparam name="T">
    /// The type of syntax list to concatenate.
    /// </typeparam>
    /// <returns>
    /// A <see cref="SyntaxList{T}" /> combining both lists.
    /// </returns>
    public static SyntaxList<T> Concat<T>(in this SyntaxList<T> list,
        in SyntaxList<T> value)
        where T : SyntaxNode
    {
        var builder = new SyntaxListBuilder<T>(list.Count + value.Count);

        foreach (var item in list)
            builder.Add(item);

        foreach (var item in value)
            builder.Add(item);

        return builder.Build();
    }
}
