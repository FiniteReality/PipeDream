using PipeDream.Compiler.Frontend;
using PipeDream.Compiler.Syntax;

if (args.Length < 1)
{
    Console.Error.WriteLine("Expected file");
    return -1;
}

var cancelToken = new CancellationTokenSource();
var gatherer = new SyntaxTreeGatherer(args[0]);

Console.CancelKeyPress += (_, e) => {
    Console.WriteLine("Stopping...");
    e.Cancel = true;
    cancelToken.Cancel();
};

try
{
    await gatherer.GatherAsync(cancelToken.Token);
}
catch (Exception e)
{
    if (e.Data.Contains("File"))
        Console.WriteLine($"Error while parsing file {e.Data["File"]}");
    throw;
}

return 0;

internal class Visitor : SyntaxVisitor
{
    private int _indentation;

    protected override void BeforeVisit(SyntaxNode value)
    {
        if (value is CompilationUnitSyntax)
        {
            Console.WriteLine("SyntaxTree:");
        }

        if (value is not TriviaSyntax)
            _indentation++;
    }

    protected override void AfterVisit(SyntaxNode node)
    {
        if (node.TrailingTrivia.Count > 0)
        {
            _indentation++;
            Console.Write(new string(' ', _indentation));
            Console.WriteLine("- TrailingTrivia:");

            _indentation++;
            foreach (var trailing in node.TrailingTrivia)
                Visit(trailing);

            _indentation--;
            _indentation--;
        }

        if (node is not TriviaSyntax)
            _indentation--;
    }

    protected internal override void VisitSyntaxNode(SyntaxNode value)
    {
        Console.Write(new string(' ', _indentation));
        Console.Write("- ");
        Console.Write(value.Kind);
        Console.Write(": # ");
        Console.WriteLine(value.GetType().Name);

        if (value.LeadingTrivia.Count > 0)
        {
            _indentation++;

            Console.Write(new string(' ', _indentation));
            Console.WriteLine("- LeadingTrivia:");

            _indentation++;

            foreach (var leading in value.LeadingTrivia)
            {
                Visit(leading);
            }

            _indentation--;
            _indentation--;
        }
    }

    protected internal override void VisitSyntaxToken(SyntaxToken value)
    {
        Console.Write(new string(' ', _indentation));
        Console.Write("- ");
        Console.Write(value.Kind);
        Console.Write(": '");
        Console.Write(value.Text.Replace("\n", "\\n"));
        Console.WriteLine('\'');

        if (value.LeadingTrivia.Count > 0)
        {
            _indentation++;

            Console.Write(new string(' ', _indentation));
            Console.WriteLine("- LeadingTrivia:");

            _indentation++;

            foreach (var leading in value.LeadingTrivia)
            {
                Visit(leading);
            }

            _indentation--;
            _indentation--;
        }
    }

    protected internal override void VisitSimpleTriviaSyntax(SimpleTriviaSyntax value)
    {
        Console.Write(new string(' ', _indentation));
        Console.Write("- ");
        Console.Write(value.Kind);
        Console.Write(": '");
        Console.Write(value.Text.Replace("\n", "\\n"));
        Console.WriteLine('\'');
    }
}
