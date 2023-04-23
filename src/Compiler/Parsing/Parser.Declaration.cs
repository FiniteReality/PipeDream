using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
{
    private async ValueTask<CompilationUnitSyntax> ParseCompilationUnitAsync(
        CancellationToken cancellationToken)
    {
        await default(ValueTask);
        return null!;
    }
}