using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
{
    private async ValueTask<DirectiveTriviaSyntax> ParseDirectiveTriviaSyntaxAsync(
        CancellationToken cancellationToken)
    {
        await default(ValueTask);
        return null!;
    }
}
