using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Text;
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing;
using PipeDream.Compiler.Parsing.Tree;
using PipeDream.Compiler.Preprocessing;

if (args.Length < 1)
{
    Console.Error.WriteLine("Expected file");
    return -1;
}

var encoding = args.Length < 2
    ? Encoding.UTF8
    : Encoding.GetEncoding(args[1]);

using var file = File.OpenRead(args[0]);
using var transcode = Encoding.CreateTranscodingStream(
    file, encoding, Encoding.UTF8, false);
var reader = PipeReader.Create(transcode);

ReadResult result = default;
LexerState lexState = new();
ParserState parseState = new();
while (!result.IsCompleted || !result.Buffer.IsEmpty)
{
    result = await reader.ReadAsync();
    var pos = Lex(result, ref lexState, ref parseState);

    if (lexState.HasErrors || parseState.HasErrors)
        break;

    reader.AdvanceTo(pos, result.Buffer.End);
}

if (lexState.HasErrors)
    foreach (var error in lexState.Errors)
        Console.Error.WriteLine($"{args[0]}({error.Span}): {error.Message}");

if (parseState.HasErrors)
    foreach (var error in parseState.Errors)
        Console.Error.WriteLine($"{args[0]}({error.Span}): {error.Message}");

if (parseState.CompilationUnit is null)
    throw new InvalidOperationException("Failed to get compilation unit");

var tree = new Preprocessor().Preprocess(parseState.CompilationUnit);
new TreePrinter().Visit(tree);

return 0;

static SequencePosition Lex(ReadResult result,
    ref LexerState lexState, ref ParserState parseState)
{
    var lexer = new Lexer(result.Buffer, result.IsCompleted, lexState);
    var parser = new Parser(parseState);

    while (lexer.Lex() && !parser.IsAccept)
    {
        bool queueFailed = false;
        do
        {
            if (!parser.Queue(lexer.CurrentToken))
            {
                queueFailed = true;
                break;
            }
        }
        while (lexer.Lex() && lexer.CurrentToken.Kind != SyntaxKind.EndOfFile);

        // Parse as much as possible based on the queued tokens
        if (parser.Parse())
        {
            // Assuming we consumed some tokens, try and queue the token we failed
            // to parse
            if (queueFailed && !parser.Queue(lexer.CurrentToken))
                throw new InvalidOperationException(
                    "Somehow failed to enqueue token");
        }
    }

    /*if (result.IsCompleted)
    {
        // We don't care if the parse succeeds or not at this point,
        // we're not processing any more so we'll handle errors anyway.
        _ = parser.Parse();
    }*/

    lexState = lexer.CurrentState;
    parseState = parser.CurrentState;

    return lexer.Position;
}

internal class TreePrinter : SyntaxVisitor
{
    private int _indentLevel = -1;

    protected override void Accept(SyntaxNode node)
        => Console.WriteLine(
            $"{new string(' ', _indentLevel)}{node.Span}: " +
            $"{node.GetType().Name}");

    protected override void BeforeVisit(SyntaxNode node)
    {
        if (node is not StatementListNode)
            _indentLevel++;
    }

    protected override void AfterVisit(SyntaxNode node)
    {
        if (node is not StatementListNode)
            _indentLevel--;
    }
}