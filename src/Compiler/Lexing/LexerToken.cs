using System.Diagnostics.CodeAnalysis;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

/// <summary>
/// A record containing information about a lexer token before translation to a
/// <see cref="SyntaxToken"/>.
/// </summary>
internal record struct LexerToken(
    SyntaxKind Kind,
    SequencePosition Start,
    SequencePosition End)
{
    public string? StringValue { get; init; }
}