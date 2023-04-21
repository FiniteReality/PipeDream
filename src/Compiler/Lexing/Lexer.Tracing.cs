using System.Diagnostics;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
{
    private static readonly TraceSource Tracing
        = new(typeof(Lexer).FullName!);

    private static class TraceIds
    {
        public const int LexingToken = 1;

        public const int DiagnosticProduced = 2;
    }
}