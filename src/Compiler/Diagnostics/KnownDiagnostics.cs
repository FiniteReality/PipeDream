namespace PipeDream.Compiler.Diagnostics;

internal static class KnownDiagnostics
{
    // Rough guide, nothing final.
    // DM0000: the "WTF" error
    // DM0001-DM0999: lex errors
    // DM1000-DM1999: parse errors
    // DM2000-DM2999: semantic analysis errors

    // A catch all used for any unhandled exception.
    public static DiagnosticDefinition Unknown
        = new(
            Id: "DM0000",
            Description: "Internal compiler error",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition UnexpectedCharacter
        = new(
            Id: "DM0001",
            Description: "Unexpected character '{0}'",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition UnterminatedString
        = new(
            Id: "DM0002",
            Description: "Unterminated string literal",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition ExpectedToken
        = new(
            Id: "DM1001",
            Description: "Syntax error. A '{0}' was expected.",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition ExpectedExpression
        = new(
            Id: "DM1002",
            Description: "Syntax error. An expression was expected.",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition ExpectedIdentifier
        = new(
            Id: "DM1003",
            Description: "Syntax error. An identifier was expected.",
            DefaultSeverity: DiagnosticSeverity.Error);

    public static DiagnosticDefinition DirectiveMustBeFirstNonWhitespaceCharacter
        = new(
            Id: "DM1004",
            Description: "Preprocessor directives must appear as the first non-whitespace character on a line",
            DefaultSeverity: DiagnosticSeverity.Error);
}
