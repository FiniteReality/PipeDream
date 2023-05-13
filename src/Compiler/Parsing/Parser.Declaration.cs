using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

internal sealed partial class Parser
{
    private async ValueTask<CompilationUnitSyntax> ParseCompilationUnitAsync(
        CancellationToken cancellationToken)
    {
        var members = new SyntaxListBuilder<MemberDeclarationSyntax>();
        while (true)
        {
            var member = await ParseMemberDeclarationAsync(cancellationToken);

            if (member == null)
                break;

            members.Add(member);
        }

        var skipped = await SkipTokensWhileAsync(t => true, true, cancellationToken);
        var eof = await ExpectAsync(SyntaxKind.EndOfFileToken,
            cancellationToken);
        Debug.Assert(eof != null, "EOF was null");

        eof = eof with
        {
            TrailingTrivia = eof.TrailingTrivia.Append(skipped)
        };

        return new CompilationUnitSyntax(
            Members: members.Build(),
            EndOfFileToken: eof,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }

    private async ValueTask<MemberDeclarationSyntax?>
        ParseMemberDeclarationAsync(
            CancellationToken cancellationToken)
    {
        var leading = await ParseDirectivesAsync(
            static (@this, token) => @this.ParseMemberDeclarationCoreAsync(token),
            cancellationToken);

        var result = await ParseMemberDeclarationCoreAsync(cancellationToken);

        var trailing = await ParseDirectivesAsync(
            static (@this, token) => @this.ParseMemberDeclarationCoreAsync(token),
            cancellationToken);

        return (result, leading.Count, trailing.Count) switch
        {
            (null, _, _) => null,
            (_, 0, 0) => result,

            (_, > 0, > 0) => result with
            {
                LeadingTrivia = result.LeadingTrivia.Concat(leading),
                TrailingTrivia = result.TrailingTrivia.Concat(trailing)
            },
            (_, > 0, 0) => result with
            {
                LeadingTrivia = result.LeadingTrivia.Concat(leading),
            },
            (_, 0, > 0) => result with
            {
                TrailingTrivia = result.TrailingTrivia.Concat(trailing),
            },

            _ => Unreachable()
        };

        [DoesNotReturn]
        static MemberDeclarationSyntax? Unreachable()
        {
            Debug.Fail("Leading or trailing trivia size incorrect");

            throw new InvalidOperationException("Failed to parse trivia");
        }
    }

    private async ValueTask<MemberDeclarationSyntax?>
        ParseMemberDeclarationCoreAsync(
            CancellationToken cancellationToken)
    {
        return await ParseVariableDeclarationAsync(cancellationToken);
    }

    private async ValueTask<VariableDeclarationSyntax?>
        ParseVariableDeclarationAsync(
            CancellationToken cancellationToken)
    {
        var @var = await PeekAsync(SyntaxKind.VarKeyword, cancellationToken);
        if (@var == null)
            return null;

        _ = await AdvanceAsync(cancellationToken);

        var identifier = await ParseNameAsync(cancellationToken);
        if (identifier == null)
        {
            ProduceDiagnostic(ParseError.ExpectedIdentifier);
            return null;
        }

        return new VariableDeclarationSyntax(
            VarKeyword: @var,
            Identifier: identifier,
            Initializer: await ParseEqualsValueClauseAsync(cancellationToken),
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }

    private async ValueTask<EqualsValueClauseSyntax?>
        ParseEqualsValueClauseAsync(
            CancellationToken cancellationToken)
    {
        var equals = await PeekAsync(SyntaxKind.EqualsToken, cancellationToken);
        if (equals == null)
            return null;

        _ = await AdvanceAsync(cancellationToken);

        var value = await ParseExpressionAsync(cancellationToken);
        if (value == null)
        {
            ProduceDiagnostic(ParseError.ExpectedExpression);
            return null;
        }

        return new EqualsValueClauseSyntax(
            EqualsToken: equals,
            Value: value,
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }
}
