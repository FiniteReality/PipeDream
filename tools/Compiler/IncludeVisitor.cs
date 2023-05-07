using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Frontend;

internal sealed class IncludeVisitor : SyntaxVisitor
{
    private readonly HashSet<string> _foundFiles = new();

    public event Action<string>? FileIncluded;

    protected internal override void VisitIncludeDirectiveTriviaSyntax(
        IncludeDirectiveTriviaSyntax value)
    {
        if (value.File is LiteralStringSyntax literal)
        {
            if (_foundFiles.Add(literal.Text.Text))
                FileIncluded?.Invoke(literal.Text.Text);
        }

        base.VisitIncludeDirectiveTriviaSyntax(value);
    }
}
