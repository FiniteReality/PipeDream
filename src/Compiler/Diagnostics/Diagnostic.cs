namespace PipeDream.Compiler.Diagnostics;

/// <summary>
/// Defines a record containing information about a compilation diagnostic.
/// </summary>
public readonly record struct Diagnostic
    : IEquatable<Diagnostic>
{
    internal Diagnostic(DiagnosticDefinition definition)
    {
        Definition = definition;
        Severity = definition.DefaultSeverity;
        Message = definition.Description;
    }

    internal Diagnostic(DiagnosticDefinition definition, object[] parameters)
    {
        Definition = definition;
        Severity = definition.DefaultSeverity;
        Message = string.Format(definition.Description, parameters);
    }

    internal Diagnostic(DiagnosticDefinition definition,
        DiagnosticSeverity severity)
    {
        Definition = definition;
        Severity = severity;
        Message = definition.Description;
    }

    internal Diagnostic(DiagnosticDefinition definition,
        DiagnosticSeverity severity,
        object[] parameters)
    {
        Definition = definition;
        Severity = severity;
        Message = string.Format(definition.Description, parameters);
    }

    /// <summary>
    /// Gets the definition of this diagnostic.
    /// </summary>
    public DiagnosticDefinition Definition { get; }

    /// <summary>
    /// Gets the message of this diagnostic.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the final severity of this diagnostic, affected by compiler flags
    /// and other directives which modify diagnostic severity.
    /// </summary>
    public DiagnosticSeverity Severity { get; }
}