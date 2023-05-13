using System.Diagnostics.CodeAnalysis;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

internal sealed partial class Parser
{
    private async ValueTask<NameSyntax?> ParseNameAsync(
        CancellationToken cancellationToken)
        => (NameSyntax?)await ParseQualifiedNameAsync(cancellationToken)
            ?? await ParseSimpleNameAsync(cancellationToken);

    private async ValueTask<QualifiedNameSyntax?> ParseQualifiedNameAsync(
        CancellationToken cancellationToken)
    {
        var path = new SeparatedSyntaxListBuilder<NameSyntax>();

        var token = await PeekAsync(cancellationToken);
        bool firstSection = true;
        while (token != null)
        {
            if (token.Kind == SyntaxKind.SlashToken && firstSection)
            {
                var root = await ParseRootedNameAsync(cancellationToken);

                // This means we have a leading or trailing `/` which is a
                // syntax error. Ideally we should skip it.
                if (root == null)
                    break;

                path.Add(root);
            }
            else if (token.Kind == SyntaxKind.SlashToken && !firstSection)
            {
                _ = await AdvanceAsync(cancellationToken);
                path.AddSeparator(token);
            }
            else
            {
                var name = await ParseSimpleNameAsync(cancellationToken);

                // The next token can't be interpreted as a name, so that's the
                // end of that for us.
                if (name == null)
                    break;

                path.Add(name);
            }

            if (token.TrailingTrivia.Any(
                x => x.Kind is SyntaxKind.WhitespaceTrivia
                    or SyntaxKind.EndOfLineTrivia))
                break;

            token = await PeekAsync(cancellationToken);
            firstSection = false;
        }

        return path.Count > 0
            ? new QualifiedNameSyntax(
                Parts: path.Build(),
                Span: default,
                LeadingTrivia: new(),
                TrailingTrivia: new())
            : null;
    }

    private async ValueTask<RootedNameSyntax?> ParseRootedNameAsync(
        CancellationToken cancellationToken)
    {
        var slash = await ExpectAsync(SyntaxKind.SlashToken, cancellationToken);
        if (slash == null)
            return null;

        var token = await PeekAsync(cancellationToken);
        if (token == null)
            // TODO: this should error
            return null;

        if (IsValidNameToken(token))
        {
            _ = await AdvanceAsync(cancellationToken);
            return new RootedNameSyntax(
                PathRootToken: slash,
                Name: token,
                Span: default,
                LeadingTrivia: new(),
                TrailingTrivia: new());
        }
        else
            return null;
    }

    private async ValueTask<SimpleNameSyntax?> ParseSimpleNameAsync(
        CancellationToken cancellationToken)
    {
        var token = await PeekAsync(cancellationToken);
        if (IsValidNameToken(token))
        {
            _ = await AdvanceAsync(cancellationToken);
            return new SimpleNameSyntax(
                Name: token,
                Span: token.Span,
                LeadingTrivia: new(),
                TrailingTrivia: new());
        }
        else
            return null;
    }

    private static bool IsValidNameToken(
        [NotNullWhen(true)] SyntaxToken? token)
    {
        return token != null
            && token.Kind is
                SyntaxKind.IdentifierToken or
                SyntaxKind.DefineKeyword or
                SyntaxKind.ElifKeyword or
                SyntaxKind.EndIfKeyword or
                SyntaxKind.ErrorKeyword or
                SyntaxKind.IfDefKeyword or
                SyntaxKind.IfNDefKeyword or
                SyntaxKind.IncludeKeyword or
                SyntaxKind.PipeDreamKeyword or
                SyntaxKind.PragmaKeyword or
                SyntaxKind.UndefKeyword or
                SyntaxKind.WarnKeyword or
                SyntaxKind.NewKeyword or
                SyntaxKind.VarKeyword or
                SyntaxKind.ConstKeyword or
                SyntaxKind.FinalKeyword or
                SyntaxKind.GlobalKeyword or
                SyntaxKind.OperatorKeyword or
                SyntaxKind.TmpKeyword or
                SyntaxKind.VerbKeyword or
                SyntaxKind.AreaKeyword or
                SyntaxKind.AtomKeyword or
                SyntaxKind.ClientKeyword or
                SyntaxKind.DatabaseKeyword or
                SyntaxKind.DatumKeyword or
                SyntaxKind.IconKeyword or
                SyntaxKind.ImageKeyword or
                SyntaxKind.ListKeyword or
                SyntaxKind.MatrixKeyword or
                SyntaxKind.MobKeyword or
                SyntaxKind.MutableAppearanceKeyword or
                SyntaxKind.ObjKeyword or
                SyntaxKind.ProcKeyword or
                SyntaxKind.RegexKeyword or
                SyntaxKind.SoundKeyword or
                SyntaxKind.TextKeyword or
                SyntaxKind.TurfKeyword or
                SyntaxKind.WorldKeyword;
    }
}
