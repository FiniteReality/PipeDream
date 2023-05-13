using System.Diagnostics;
using PipeDream.Compiler.Diagnostics;

namespace PipeDream.Compiler.Lexing;

internal static class LexError
{
    private static Diagnostic Diagnostic(DiagnosticDefinition definition,
        params object[] parameters)
        => new(definition, parameters);

    public static Diagnostic UnexpectedCharacter(byte unexpected)
        => Diagnostic(KnownDiagnostics.UnexpectedCharacter, (char)unexpected);

    public static Diagnostic UnterminatedString()
        => Diagnostic(KnownDiagnostics.UnterminatedString);
}

internal ref partial struct Lexer
{
    private readonly void ProduceDiagnostic<T>(T state,
        Func<T, Diagnostic> handler)
    {
        var diagnostic = handler(state);
        _diagnostics.Add(diagnostic);
    }

    private readonly void ProduceDiagnostic(Func<Diagnostic> handler)
    {
        var diagnostic = handler();
        _diagnostics.Add(diagnostic);
    }
}
