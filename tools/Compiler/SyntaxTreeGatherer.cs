using System.Diagnostics;
using System.Text;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Frontend;

internal sealed class SyntaxTreeGatherer
{
    private readonly IncludeVisitor _visitor
        = new();
    private readonly Dictionary<string, SyntaxTree> _parsedUnits
        = new();
    private readonly Queue<string> _parseQueue
        = new();

    private readonly string _rootDirectory;

    public SyntaxTreeGatherer(string entryPoint)
    {
        if (Path.GetDirectoryName(entryPoint) is not string root)
            throw new ArgumentException(
                "Unable to get root directory from path",
                nameof(entryPoint));

        // TODO: issue a warning for non-portable paths?
        _visitor.FileIncluded += f => _parseQueue.Enqueue(f.Replace('\\', '/'));
        _rootDirectory = root;

        _parseQueue.Enqueue(
            Path.GetFileName(entryPoint));
    }

    public async ValueTask GatherAsync(CancellationToken cancellationToken)
    {
        while (_parseQueue.TryDequeue(out var path))
        {
            path = Path.Combine(_rootDirectory, path);
            if (!_parsedUnits.ContainsKey(path))
            {
                var node = await ParseFileAsync(path, cancellationToken)
                    .ConfigureAwait(false);

                Debug.Assert(node != null);

                _parsedUnits.Add(path, node);
                _visitor.Visit(node.Root);
            }
        }
    }

    private static async ValueTask<SyntaxTree> ParseFileAsync(
        string path,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(path);

        using var file = File.OpenRead(path);
        try
        {
            return await SyntaxTree.ParseAsync(
                file,
                Encoding.UTF8,
                path,
                cancellationToken);
        }
        catch (Exception e)
        {
            e.Data["File"] = path;
            throw;
        }
    }
}
