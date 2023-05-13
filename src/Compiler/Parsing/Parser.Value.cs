using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

internal sealed partial class Parser
{
    private async ValueTask<LiteralExpressionSyntax?> ParseNumericLiteralAsync(
        CancellationToken cancellationToken)
    {
        var number = await PeekAsync(
            SyntaxKind.NumericLiteralToken,
            cancellationToken);
        if (number == null)
            return null;

        _ = await AdvanceAsync(cancellationToken);

        return new LiteralExpressionSyntax(
            Token: number,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }

    private async ValueTask<StringSyntax?> ParseStringAsync(
        CancellationToken cancellationToken)
    {
        var quote = await PeekAsync(cancellationToken);
        if (quote is not
            {
                Kind: SyntaxKind.RawStringStartToken or
                SyntaxKind.RawVerbatimStringStartToken or
                SyntaxKind.InterpolatedStringStartToken or
                SyntaxKind.InterpolatedVerbatimStringStartToken
            })
        {
            return null;
        }

        _ = await AdvanceAsync(cancellationToken);

        return quote switch
        {
            //{ Kind: SyntaxKind.RawStringStartToken }
            //    => await ParseRawStringAsync(quote, cancellationToken),
            //{ Kind: SyntaxKind.RawVerbatimStringStartToken }
            //    => await ParseRawVerbatimStringAsync(quote, cancellationToken),
            { Kind: SyntaxKind.InterpolatedStringStartToken }
                => await ParseInterpolatedStringAsync(quote, cancellationToken),
            //{ Kind: SyntaxKind.InterpolatedVerbatimStringStartToken }
            //    => await ParseInterpolatedVerbatimStringAsync(quote, cancellationToken),

            _ => Unreachable(this)
        };

        [DoesNotReturn]
        static StringSyntax Unreachable(
            Parser @this)
        {
            @this.ProduceDiagnostic(() => new(KnownDiagnostics.Unknown));
            Debug.Fail("Invalid quote type");
            return null;
        }
    }

    private async ValueTask<StringSyntax?> ParseInterpolatedStringAsync(
        SyntaxToken quote,
        CancellationToken cancellationToken)
    {
        SyntaxToken lastToken = null!;
        var textSections = new SyntaxListBuilder<SyntaxToken>();

        do
        {
            var token = await PeekAsync(cancellationToken);
            if (token == null)
            {
                ProduceDiagnostic(
                    SyntaxKind.InterpolatedStringEndToken,
                    ParseError.ExpectedSyntax);
                return null;
            }

            if (token.Kind is SyntaxKind.StringTextToken)
                _ = await AdvanceAsync(cancellationToken);
            else if (token.Kind is SyntaxKind.InterpolatedStringEndToken)
                break;

            if (lastToken != null)
                textSections.Add(lastToken);
            lastToken = token;
        }
        while (true);

        var endQuote = await ExpectAsync(
            SyntaxKind.InterpolatedStringEndToken,
            cancellationToken);
        if (endQuote == null)
            return null;

        if (textSections.Empty)
            return new LiteralStringSyntax(
                StringStartToken: quote,
                Text: lastToken,
                StringEndToken: endQuote,
                Span: default,
                LeadingTrivia: new(),
                TrailingTrivia: new());

        Debug.Fail("Not Yet Implemented");
        return null;
    }
}
