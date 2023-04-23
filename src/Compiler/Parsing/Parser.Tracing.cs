using System.Diagnostics;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
{
    private static readonly TraceSource Tracing
        = new(typeof(Parser).FullName!);

    private static class TraceIds
    {
        public const int DiagnosticProduced = 1;
    }
}
