namespace PipeDream.Compiler.Diagnostics;

internal static class KnownDiagnostics
{
    // A catch all used for any unhandled exception.
    public static DiagnosticDefinition Unknown
        = new(
            Id: "DM0000",
            Description: "Internal compiler error",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition ExpectedToken
        = new(
            Id: "DM0001",
            Description: "Syntax error. A '{0}' was expected.",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition UnexpectedCharacter
        = new(
            Id: "DM0002",
            Description: "Unexpected character '{0}'",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition UnterminatedString
        = new(
            Id: "DM0003",
            Description: "Unterminated string literal",
            DefaultSeverity: DiagnosticSeverity.Error);
}