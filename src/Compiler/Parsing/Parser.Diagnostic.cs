using System.Diagnostics;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

internal static class ParseError
{
    private static Diagnostic Diagnostic(DiagnosticDefinition definition,
        params object[] parameters)
        => new(definition, parameters);

    // TODO: convert these to their actual string representation?
    public static Diagnostic ExpectedSyntax(
        SyntaxKind expected)
        => Diagnostic(KnownDiagnostics.ExpectedToken, expected);

    public static Diagnostic ExpectedExpression()
        => Diagnostic(KnownDiagnostics.ExpectedExpression);

    public static Diagnostic DirectiveMustBeFirstNonWhitespaceCharacter()
        => Diagnostic(KnownDiagnostics.DirectiveMustBeFirstNonWhitespaceCharacter);
}

public sealed partial class Parser
{
    private void ProduceDiagnostic<T>(T state,
        Func<T, Diagnostic> handler)
    {
        var diagnostic = handler(state);

        Tracing.DiagnosticProduced(diagnostic);
        _diagnostics.Add(diagnostic);
    }

    private void ProduceDiagnostic(Func<Diagnostic> handler)
    {
        var diagnostic = handler();

        Tracing.DiagnosticProduced(diagnostic);
        _diagnostics.Add(diagnostic);
    }
}
