namespace PipeDream.Compiler.Diagnostics;

/// <summary>
/// Defines a record used to define a <see cref="Diagnostic" />.
/// </summary>
public sealed record DiagnosticDefinition(
    string Id,
    string Description,
    DiagnosticSeverity DefaultSeverity)
{
    /// <summary>
    /// Gets the unique identifier for the diagnostic.
    /// </summary>
    public string Id { get; } = Id;

    /// <summary>
    /// Gets the human-readable description for the diagnostic.
    /// </summary>
    public string Description { get; } = Description;

    /// <summary>
    /// Gets the default severity of diagnostics produced by this definition.
    /// </summary>
    public DiagnosticSeverity DefaultSeverity { get; } = DefaultSeverity;
}
