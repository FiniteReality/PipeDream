using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading.Channels;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

/// <summary>
/// Defines a class used for parsing Dream Maker syntax tokens into syntax
/// trees.
/// </summary>
public sealed partial class Parser
{
    private readonly ImmutableArray<Diagnostic>.Builder _diagnostics;
    private readonly ChannelReader<SyntaxToken> _reader;

    /// <summary>
    /// Creates a new instance of the <see cref="Parser"/> type.
    /// </summary>
    /// <param name="input">
    /// The channel reader to read tokens from.
    /// </param>
    public Parser(ChannelReader<SyntaxToken> input)
    {
        _diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();
        _reader = input;
    }

    /// <summary>
    /// Parses all of the tokens until the end of file is reached.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> used for cancelling parsing.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask"/> representing the asynchronous work of parsing
    /// tokens.
    /// </returns>
    public async ValueTask<SyntaxNode?> RunAsync(CancellationToken cancellationToken)
        => await ParseCompilationUnitAsync(cancellationToken);

    private ValueTask<SyntaxToken?>
        ExpectAsync(
            SyntaxKind kind,
            CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_reader.TryRead(out var token))
            return GoAsync(this, _reader, kind, cancellationToken);

        if (token.Kind == kind)
            return new(token);

        ProduceDiagnostic(kind, ParseError.ExpectedSyntax);
        return default;

        static async ValueTask<SyntaxToken?> GoAsync(
            Parser @this,
            ChannelReader<SyntaxToken> reader,
            SyntaxKind kind,
            CancellationToken cancellationToken)
        {
            while (await reader.WaitToReadAsync(cancellationToken))
            {
                if (reader.TryRead(out var token))
                {
                    if (token.Kind == kind)
                        return token;

                    @this.ProduceDiagnostic(kind, ParseError.ExpectedSyntax);
                    return null;
                }
            }

            Debug.Fail("Should never fail to read a token?");
            return null;
        }
    }

    private ValueTask<SyntaxToken?> PeekAsync(
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _reader.TryRead(out var token)
            ? new(token)
            : GoAsync(_reader, cancellationToken);

        static async ValueTask<SyntaxToken?> GoAsync(
            ChannelReader<SyntaxToken> reader,
            CancellationToken cancellationToken)
        {
            while (await reader.WaitToReadAsync(cancellationToken))
            {
                if (reader.TryRead(out var token))
                {
                    return token;
                }
            }

            Debug.Fail("Should never fail to peek a token?");
            return null;
        }
    }

    // TODO: decide whether this should return the token or not
    // - if it's already been Peeked we shouldn't need it, right?
    private ValueTask<SyntaxToken?> AdvanceAsync(
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _reader.TryRead(out var token)
            ? new(token)
            : GoAsync(_reader, cancellationToken);

        static async ValueTask<SyntaxToken?> GoAsync(
            ChannelReader<SyntaxToken> reader,
            CancellationToken cancellationToken)
        {
            while (await reader.WaitToReadAsync(cancellationToken))
            {
                if (reader.TryRead(out var token))
                    return token;
            }

            Debug.Fail("Should never fail to read a token?");
            return null;
        }
    }
}