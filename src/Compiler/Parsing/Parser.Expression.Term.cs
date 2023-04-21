using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Parsing;

public sealed partial class Parser
{
    private async ValueTask<ExpressionSyntax?> ParseTermAsync(
        CancellationToken cancellationToken)
    {
        var term = await ParseRawTermAsync(cancellationToken);
    }

    private async ValueTask<ExpressionSyntax?> ParseRawTermAsync(
        CancellationToken cancellationToken)
    {
        
    }
}