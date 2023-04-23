namespace PipeDream.Compiler.Diagnostics;

/// <summary>
/// Defines an enum containing all possible diagnostic severities.
/// </summary>
public enum DiagnosticSeverity
{
    /// <summary>
    /// A diagnostic which is hidden, usually due to the appropriate diagnostic
    /// flags being upplied to silence the message.
    /// </summary>
    Hidden,

    /// <summary>
    /// A diagnostic suggesting some means to improve code, such as by
    /// following naming convention preferences.
    /// </summary>
    Suggestion,

    /// <summary>
    /// A diagnostic which can be safely ignored, usually presenting some form
    /// of informational status to the user, such as the compiler version.
    /// </summary>
    Informational,

    /// <summary>
    /// A diagnostic which indicates suspicious code which may fail at runtime,
    /// such as accessing members of a variable whose value may be null.
    /// </summary>
    Warning,

    /// <summary>
    /// A diagnostic which indicates a disallowed language construct, either by
    /// the language rules or some other authority.
    /// </summary>
    Error
}
