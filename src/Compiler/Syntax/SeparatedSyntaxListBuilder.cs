using System.Collections.Immutable;

namespace PipeDream.Compiler.Syntax;

internal struct SeparatedSyntaxListBuilder<T>
    where T : SyntaxNode
{
    // Store quick references to the first four elements to avoid allocating an
    // array until later
    private (T?, SyntaxToken?) _first;
    private (T?, SyntaxToken?) _second;
    private (T?, SyntaxToken?) _third;
    private (T?, SyntaxToken?) _fourth;

    // And use a builder if we need more.
    private ImmutableArray<(T?, SyntaxToken?)>.Builder? _builder;

    private int _numberOfItems;
    private int _numberOfTokens;

    public int Count => _numberOfItems;

    public readonly SeparatedSyntaxList<T> Build()
    {
        return _builder != null
            ? new(_builder.DrainToImmutable(), _numberOfItems, _numberOfTokens)
            : (_numberOfItems + _numberOfTokens) switch
            {
                0 => new(),
                1 => new(
                    ImmutableArray.Create(_first),
                    _numberOfItems,
                    _numberOfTokens),
                2 => new(
                    ImmutableArray.Create(_first, _second),
                    _numberOfItems,
                    _numberOfTokens),
                3 => new(
                    ImmutableArray.Create(_first, _second, _third),
                    _numberOfItems,
                    _numberOfTokens),
                4 => new(
                    ImmutableArray.Create(_first, _second, _third, _fourth),
                    _numberOfItems,
                    _numberOfTokens),

                _ => throw new InvalidOperationException("Unreachable")
            };
    }

    public void Add(T value)
    {
        AddCore((value, null));
        _numberOfItems++;
    }

    public void AddSeparator(SyntaxToken separator)
    {
        AddCore((null, separator));
        _numberOfTokens++;
    }

    private void AddCore((T?, SyntaxToken?) item)
    {
        if (_builder != null)
        {
            _builder.Add(item);
        }
        else if (_first is (null, null))
        {
            _first = item;
        }
        else if (_second is (null, null))
        {
            _second = item;
        }
        else if (_third is (null, null))
        {
            _third = item;
        }
        else if (_fourth is (null, null))
        {
            _fourth = item;
        }
        else
        {
            _builder = ImmutableArray.CreateBuilder<(T?, SyntaxToken?)>(5);

            _builder.Add(_first);
            _first = (null, null);

            _builder.Add(_second);
            _second = (null, null);

            _builder.Add(_third);
            _third = (null, null);

            _builder.Add(_fourth);
            _fourth = (null, null);

            _builder.Add(item);
        }
    }
}
