using System.Diagnostics;
using PipeDream.Compiler.Diagnostics;

namespace PipeDream.Compiler;

internal static class Tracing
{
    private static readonly DiagnosticSource Listener
        = new DiagnosticListener("PipeDream.Compiler");

    private const string LexingToken = nameof(LexingToken);

    private static Activity? _tokenLexingActivity = null;
    public static void StartLexingToken(
        long position)
    {
        if (Listener.IsEnabled(LexingToken))
        {
            Debug.Assert(_tokenLexingActivity == null);

            _tokenLexingActivity = Listener.StartActivity(
                new(LexingToken),
                new {
                    Position = position
                });
        }
    }

    public static void StopLexingToken(
        long position,
        bool rewind)
    {
        if (Listener.IsEnabled(LexingToken))
        {
            Debug.Assert(_tokenLexingActivity != null);

            Listener.StopActivity(
                _tokenLexingActivity,
                new {
                    Position = position,
                    Rewinding = rewind
                });
            _tokenLexingActivity = null;
        }
    }

    public static void DiagnosticProduced(Diagnostic diagnostic)
    {
        if (Listener.IsEnabled(nameof(DiagnosticProduced)))
            Listener.Write(nameof(DiagnosticProduced),
                new {
                    Diagnostic = diagnostic
                });
    }
}
