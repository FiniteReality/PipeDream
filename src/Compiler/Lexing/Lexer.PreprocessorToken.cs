using System.Buffers;
using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Unicode;

namespace PipeDream.Compiler.Lexing;

ref partial struct Lexer
{
    private static ReadOnlySpan<byte> DefineToken
        => new byte[]
        {
            (byte)'d', (byte)'e', (byte)'f', (byte)'i', (byte)'n', (byte)'e'
        };
    private static ReadOnlySpan<byte> ElifToken
        => new byte[]
        {
            (byte)'e', (byte)'l', (byte)'i', (byte)'f'
        };
    private static ReadOnlySpan<byte> ElseToken
        => new byte[]
        {
            (byte)'e', (byte)'l', (byte)'s', (byte)'e'
        };
    private static ReadOnlySpan<byte> EndIfToken
        => new byte[]
        {
            (byte)'e', (byte)'n', (byte)'d', (byte)'i', (byte)'f'
        };
    private static ReadOnlySpan<byte> ErrorToken
        => new byte[]
        {
            (byte)'e', (byte)'r', (byte)'r', (byte)'o', (byte)'r'
        };
    private static ReadOnlySpan<byte> IfToken
        => new byte[]
        {
            (byte)'i', (byte)'f',
        };
    private static ReadOnlySpan<byte> IfDefToken
        => new byte[]
        {
            (byte)'i', (byte)'f', (byte)'d', (byte)'e', (byte)'f'
        };
    private static ReadOnlySpan<byte> IfNDefToken
        => new byte[]
        {
            (byte)'i', (byte)'f', (byte)'n', (byte)'d', (byte)'e', (byte)'f'
        };
    private static ReadOnlySpan<byte> IncludeToken
        => new byte[]
        {
            (byte)'i', (byte)'n', (byte)'c', (byte)'l', (byte)'u', (byte)'d',
            (byte)'e'
        };
    private static ReadOnlySpan<byte> WarnToken
        => new byte[]
        {
            (byte)'w', (byte)'a', (byte)'r', (byte)'n'
        };

    private bool LexPreprocessorToken()
    {
        if (!_reader.TryPeek(out var one))
            return Stop("Invalid preprocessing token");
        if (!_reader.TryPeek(1, out var two))
            return Stop("Invalid preprocessing token");
        if (!_reader.TryPeek(2, out var three))
            return Stop("Invalid preprocessing token");

        return one switch
        {
            (byte)'d' => ProduceIfNext(ref this, DefineToken,
                SyntaxKind.PreprocessorDefine,
                LexerMode.MiddleOfLine),

            (byte)'i' => two switch { // if, ifdef, ifndef, include
                (byte)'f' => three switch { // if, ifdef, ifndef
                    (byte)'d' => ProduceIfNext(ref this, IfDefToken,
                        SyntaxKind.PreprocessorIfDef,
                        LexerMode.MiddleOfLine),
                    (byte)'n' => ProduceIfNext(ref this, IfNDefToken,
                        SyntaxKind.PreprocessorIfNDef,
                        LexerMode.MiddleOfLine),
                    (>= (byte)'A' and <= (byte)'Z') or
                    (>= (byte)'a' and <= (byte)'z') or
                    (byte)'_' => Error(
                        $"Invalid preprocessing token {three:X2}"),
                    _ => ProduceIfNext(ref this, IfToken,
                        SyntaxKind.PreprocessorIf,
                        LexerMode.MiddleOfLine)
                },
                (byte)'n' => ProduceIfNext(ref this, IncludeToken,
                    SyntaxKind.PreprocessorInclude,
                    LexerMode.MiddleOfLine),
                _ => Error($"Invalid preprocessing token {two:X2}")
            },
            (byte)'e' => two switch { // elif, else, endif, error
                (byte)'l' => three switch { // elif, else
                    (byte)'i' => ProduceIfNext(ref this, ElifToken,
                        SyntaxKind.PreprocessorElseIf,
                        LexerMode.MiddleOfLine),
                    (byte)'s' => ProduceIfNext(ref this, ElseToken,
                        SyntaxKind.PreprocessorElse,
                        LexerMode.EndOfLine),
                    _ => Error($"Invalid preprocessing token {three:X2}")
                },
                (byte)'n' => ProduceIfNext(ref this, EndIfToken,
                    SyntaxKind.PreprocessorEndIf,
                    LexerMode.EndOfLine),
                (byte)'r' => ProduceIfNext(ref this, ErrorToken,
                    SyntaxKind.PreprocessorError,
                    LexerMode.MiddleOfLine),
                _ => Error($"Invalid preprocessing token {two:X2}")
            },
            (byte)'w' => ProduceIfNext(ref this, WarnToken,
                SyntaxKind.PreprocessorWarn,
                LexerMode.MiddleOfLine),
            _ => Error($"Invalid preprocessing token {one:X2}")
        };

        static bool ProduceIfNext(
            ref Lexer @this,
            ReadOnlySpan<byte> next,
            SyntaxKind token,
            LexerMode mode)
        {
            if (next.Length > @this._reader.Remaining)
                return @this.Stop();

            return @this._reader.IsNext(next, true)
                ? @this.Token(token, mode)
                : @this.Error($"Invalid preprocessing token (best guess {token})");
        }
    }
}