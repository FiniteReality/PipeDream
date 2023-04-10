using System.Diagnostics;
using System.IO.Compression;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

internal static class LexError
{
    private static Diagnostic Diagnostic(DiagnosticDefinition definition,
        params object[] parameters)
        => new(definition, parameters);

    public static Diagnostic UnexpectedCharacter(byte unexpected)
        => Diagnostic(KnownDiagnostics.UnexpectedCharacter, (char)unexpected);
}