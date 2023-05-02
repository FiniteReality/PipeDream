using System.Diagnostics;
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
            { Kind: SyntaxKind.DefineKeyword }
                => await ParseDefineDirectiveTriviaAsync(hash, cancellationToken),

            { Kind: SyntaxKind.IncludeKeyword }
                => await ParseIncludeDirectiveTriviaAsync(hash, cancellationToken),

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
        var define = await ExpectAsync(SyntaxKind.DefineKeyword, cancellationToken);
        if (define == null)
            return null;

        var name = await ParseSimpleNameAsync(cancellationToken);
        if (name == null)
        {
            ProduceDiagnostic(ParseError.ExpectedIdentifier);
            return null;
        }

        if (name.Name.TrailingTrivia.Any(x => x.Kind == SyntaxKind.EndOfLineTrivia))
            return new DefineDirectiveTriviaSyntax(
                HashToken: hash,
                DefineKeyword: define,
                Name: name,
                Value: null,
                Span: default,
                LeadingTrivia: new(),
                TrailingTrivia: new());

        // TODO: multiple values (SeparatedSyntaxList?)
        // TODO: accept backslash as an escape
        var value =
            (SyntaxNode?)await ParseMemberDeclarationCoreAsync(cancellationToken)
            ?? await ParseExpressionCoreAsync(cancellationToken);

        if (value == null)
        {
            Debug.Fail("Failed to parse a value?");
            return null;
        }

        return new DefineDirectiveTriviaSyntax(
            HashToken: hash,
            DefineKeyword: define,
            Name: name,
            Value: value,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }

    private async ValueTask<IncludeDirectiveTriviaSyntax?>
        ParseIncludeDirectiveTriviaAsync(
            SyntaxToken hash,
            CancellationToken cancellationToken)
    {
        var include = await ExpectAsync(SyntaxKind.IncludeKeyword, cancellationToken);
        if (include == null)
            return null;

        var file = await ParseStringAsync(cancellationToken);
        if (file is not LiteralStringSyntax)
        {
            ProduceDiagnostic(SyntaxKind.LiteralString, ParseError.ExpectedSyntax);
            return null;
        }

        return new IncludeDirectiveTriviaSyntax(
            HashToken: hash,
            IncludeKeyword: include,
            File: file,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }
}
