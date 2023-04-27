using System.Collections.Immutable;

namespace PipeDream.Compiler.Syntax;

internal struct SyntaxListBuilder<T>
    where T : SyntaxNode
{
    // Store quick references to the first four elements to avoid allocating an
    // array until later
    private T? _first;
    private T? _second;
    private T? _third;
    private T? _fourth;

    // And use a builder if we need more.
    private ImmutableArray<T>.Builder? _builder;

    public SyntaxListBuilder(int initialCapacity)
    {
        if (initialCapacity > 4)
            _builder = ImmutableArray.CreateBuilder<T>(initialCapacity);
    }

    public readonly SyntaxList<T> Build()
    {
        return _builder != null
            ? new(_builder.DrainToImmutable())
            : (_first, _second, _third, _fourth) switch
            {
                (null, null, null, null)
                    => new(),
                (not null, null, null, null)
                    => new(ImmutableArray.Create(_first)),
                (not null, not null, null, null)
                    => new(ImmutableArray.Create(_first, _second)),
                (not null, not null, not null, null)
                    => new(ImmutableArray.Create(_first, _second, _third)),
                (not null, not null, not null, not null)
                    => new(ImmutableArray.Create(_first, _second, _third, _fourth)),

                _ => throw new InvalidOperationException("Unreachable")
            };
    }

    public void Add(T item)
    {
        if (_builder != null)
        {
            _builder.Add(item);
        }
        else if (_first == null)
        {
            _first = item;
        }
        else if (_second == null)
        {
            _second = item;
        }
        else if (_third == null)
        {
            _third = item;
        }
        else if (_fourth == null)
        {
            _fourth = item;
        }
        else
        {
            _builder = ImmutableArray.CreateBuilder<T>(5);

            _builder.Add(_first);
            _first = null;

            _builder.Add(_second);
            _second = null;

            _builder.Add(_third);
            _third = null;

            _builder.Add(_fourth);
            _fourth = null;

            _builder.Add(item);
        }
    }
}
