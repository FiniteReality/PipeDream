using System.Reflection;
using PipeDream.Compiler.Syntax;
using Xunit.Sdk;

namespace PipeDream.Compiler.UnitTests;

internal sealed class SyntaxTreeTestDataAttribute : DataAttribute
{
    private readonly string _path;

    public SyntaxTreeTestDataAttribute(string path)
    {
        _path = path;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach (var path in Directory.EnumerateFiles(
            _path, "*.dm", SearchOption.AllDirectories))
        {
            yield return new[] { File.ReadAllText(path) };
        }
    }
}

/// <summary>
/// Defines tests against syntax trees.
/// </summary>
public sealed class SyntaxTreeTests
{
    /// <summary>
    /// Ensures that a given source code can be parsed without error.
    /// </summary>
    /// <param name="source">
    /// The source code to parse.
    /// </param>
    [Theory]
    [SyntaxTreeTestData("TestSyntax/ValidCode")]
    public async Task SourceCodeCanBeParsedAsync(string source)
    {
        Assert.NotEmpty(source);

        // Parsing code should not take longer than 3 seconds; if it does there
        // is likely a bug in the parser or lexer.
        using var token = new CancellationTokenSource(
            TimeSpan.FromSeconds(3));
        var tree = await SyntaxTree.ParseAsync(source,
            cancellationToken: token.Token);

        Assert.NotNull(tree);
        Assert.NotNull(tree.Encoding);
        Assert.NotNull(tree.FilePath);
        Assert.NotNull(tree.Root);
    }
}
