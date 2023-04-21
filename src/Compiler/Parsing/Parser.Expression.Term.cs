using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
{
    private async ValueTask<ExpressionSyntax?> ParseTermAsync(
        CancellationToken cancellationToken)
    {
        var leadingSlash = await PeekAsync(cancellationToken);
        if (leadingSlash == null)
            return null;

        if (leadingSlash.Kind == SyntaxKind.SlashToken)
            _ = await AdvanceAsync(cancellationToken);

        var term = await ParseRawTermAsync(cancellationToken);
        // postfix operators
    }

    private async ValueTask<NameSyntax?> ParseRawTermAsync(
        CancellationToken cancellationToken)
    {
        var token = await PeekAsync(cancellationToken);
        if (token == null)
            return null;

        if (token.Kind is
            SyntaxKind.IdentifierToken or
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
            SyntaxKind.WorldKeyword)
            _ = await AdvanceAsync(cancellationToken);
        else
            return null;
    }
}