using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Channels;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing;

namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a type representing the parsed form of a DM source document.
/// </summary>
public sealed class SyntaxTree
{
    private SyntaxTree(SyntaxNode root, string path, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(root, nameof(root));
        ArgumentNullException.ThrowIfNull(path, nameof(path));
        ArgumentNullException.ThrowIfNull(encoding, nameof(encoding));

        Root = root;
        FilePath = path;
        Encoding = encoding;
    }

    /// <summary>
    /// Gets the root node of the parsed syntax tree.
    /// </summary>
    public SyntaxNode Root { get; }
    /// <summary>
    /// Gets the path of the source document.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Gets the encoding of the source document.
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    /// Parses a syntax tree from the given source text.
    /// </summary>
    /// <param name="text">
    /// The source text to parse.
    /// </param>
    /// <param name="path">
    /// The path of the source text.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel parsing and lexing.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> representing the potentially asynchronous
    /// work of parsing.
    /// </returns>
    public static ValueTask<SyntaxTree> ParseAsync(
        string text,
        string path = "",
        CancellationToken cancellationToken = default)
    {
        return ParseAsync(
            new MemoryStream(Encoding.UTF8.GetBytes(text), false),
            Encoding.UTF8,
            path,
            cancellationToken);
    }

    /// <summary>
    /// Parses a syntax tree from the given source text.
    /// </summary>
    /// <param name="stream">
    /// The source stream to parse.
    /// </param>
    /// <param name="encoding">
    /// The encoding of the source stream.
    /// </param>
    /// <param name="path">
    /// The path of the source text.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel parsing and lexing.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> representing the potentially asynchronous
    /// work of parsing.
    /// </returns>
    public static async ValueTask<SyntaxTree> ParseAsync(
        Stream stream,
        Encoding encoding,
        string path = "",
        CancellationToken cancellationToken = default)
    {
        using var token = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken);
        using var transcode = encoding == Encoding.UTF8
            ? stream
            : Encoding.CreateTranscodingStream(stream, encoding, Encoding.UTF8);

        var channel = Channel.CreateBounded<SyntaxToken>(1024);
        var parser = new Parser(channel.Reader);
        var reader = PipeReader.Create(transcode);

        var lexTask = CancelLexErrorAsync(token,
            Lexer.LexAsync(reader, channel.Writer, token.Token))
            .ConfigureAwait(false);
        var parseTask = CancelParseErrorAsync(token,
            parser.RunAsync(token.Token))
            .ConfigureAwait(false);

        await lexTask;
        return new(await parseTask, path, encoding);

        // We need duplicate definitions here since ValueTask and ValueTask<T>
        // have no common type that we could constrain a generic method to
        static async ValueTask CancelLexErrorAsync(
            CancellationTokenSource source, ValueTask task)
        {
            try
            {
                await task;
            }
            catch (Exception)
            {
                source.Cancel();
                throw;
            }
        }

        static async ValueTask<SyntaxNode> CancelParseErrorAsync(
            CancellationTokenSource source, ValueTask<SyntaxNode> task)
        {
            try
            {
                return await task;
            }
            catch (Exception)
            {
                source.Cancel();
                throw;
            }
        }
    }
}
