using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
{
    private async ValueTask<SyntaxList<TriviaSyntax>>
        ParseDirectivesAsync<T>(
            Func<Parser, CancellationToken, ValueTask<T?>> parseCore,
            CancellationToken cancellationToken)
            where T : SyntaxNode
    {
        var list = new SyntaxListBuilder<TriviaSyntax>();

        while (true)
        {
            var item = await ParseDirectiveTriviaSyntaxAsync(
                parseCore,
                cancellationToken);

            if (item == null)
                break;

            list.Add(item);
        }

        return list.Build();
    }

    private async ValueTask<DirectiveTriviaSyntax?>
        ParseDirectiveTriviaSyntaxAsync<T>(
            Func<Parser, CancellationToken, ValueTask<T?>> parseCore,
            CancellationToken cancellationToken)
    {
        var hasLineTerminator = _lastReadToken?.TrailingTrivia
            .Any(x => x.Kind == SyntaxKind.EndOfLineTrivia)
            ?? true;

        var hash = await PeekAsync(SyntaxKind.HashToken, cancellationToken);
        if (hash == null)
            return null;

        _ = await AdvanceAsync(cancellationToken);

        var hasLeadingWhitespace = hash.LeadingTrivia.Any(
            x => x.Kind == SyntaxKind.WhitespaceTrivia);

        if (hasLeadingWhitespace || !hasLineTerminator)
        {
            ProduceDiagnostic(ParseError.DirectiveMustBeFirstNonWhitespaceCharacter);
            return null;
        }

        return await PeekAsync(cancellationToken) switch
        {
            null => null,

            //{ Kind: SyntaxKind.DefineKeyword }
            //    => await ParseDefineDirectiveTriviaAsync(hash, cancellationToken),

            _ => await ParseBadDirectiveTriviaAsync(hash, cancellationToken)
        };
    }

    private async ValueTask<BadDirectiveTriviaSyntax>
        ParseBadDirectiveTriviaAsync(
            SyntaxToken hash,
            CancellationToken cancellationToken)
    {
        var name = await ParseSimpleNameAsync(cancellationToken);

        var skipped = await SkipTokensWhileAsync(
            x => x.Kind != SyntaxKind.EndOfFileToken
                && x.TrailingTrivia.All(
                    y => y.Kind != SyntaxKind.EndOfLineTrivia),
            includeLast: true,
            cancellationToken);

        return new BadDirectiveTriviaSyntax(
            HashToken: hash,
            Name: name,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: skipped != null ? new(skipped) : new());
    }

    private async ValueTask<DefineDirectiveTriviaSyntax?>
        ParseDefineDirectiveTriviaAsync(
            SyntaxToken hash,
            CancellationToken cancellationToken)
    {
        await default(ValueTask);
        return null;
    }
}
