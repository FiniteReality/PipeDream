using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
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

        return new CompilationUnitSyntax(
            Members: members.Build(),
            Span: default,
            LeadingTrivia: new(),
            TrailingTrivia: new());
    }

    private async ValueTask<MemberDeclarationSyntax?>
        ParseMemberDeclarationAsync(
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
