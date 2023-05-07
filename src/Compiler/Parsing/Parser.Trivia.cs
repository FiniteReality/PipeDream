using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
{
    private async ValueTask<SkippedTokensTriviaSyntax> SkipTokensWhileAsync(
        Func<SyntaxToken, bool> predicate,
        bool includeLast,
        CancellationToken cancellationToken)
    {
        SyntaxListBuilder<SyntaxToken> skipped = new();

        var token = await PeekAsync(cancellationToken);

        while (token != null
            && token.Kind != SyntaxKind.EndOfFileToken
            && predicate(token))
        {
            skipped.Add(token);

            _ = await AdvanceAsync(cancellationToken);
            token = await PeekAsync(cancellationToken);
        }

        if (token != null
            && token.Kind != SyntaxKind.EndOfFileToken
            && includeLast)
        {
            skipped.Add(token);
            _ = await AdvanceAsync(cancellationToken);
        }

        return new SkippedTokensTriviaSyntax(
            Tokens: skipped.Build(),
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }
}
