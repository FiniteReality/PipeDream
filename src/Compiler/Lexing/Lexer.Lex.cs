using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
{
    /// <summary>
    /// Attempts to parse the next token in the stream.
    /// </summary>
    /// <returns>
    /// <code>true</code> if lexing completed successfully.
    /// </returns>
    public bool Lex()
    {
        switch (_mode)
        {
            case LexerMode.StartOfLine:
            {
                // Update the position of the start of line to compute
                // column information
                _lineStartOffset = _reader.Sequence.GetOffset(_reader.Position);
                Start();

                // Handle whitespace-sensitive blocks by inserting fake tokens
                var skipped = ReadWhitespace();
                if (_indentDepth > 0)
                {
                    if (_indentSize == null)
                    {
                        if (skipped == 0)
                            return Error(
                                $"Expected {_indentDepth} levels of indentation");

                        _indentSize = unchecked((int)skipped);
                    }
                    else
                    {
                        var expected = _indentDepth-- * _indentSize.Value;
                        if (skipped < expected)
                        {
                            Token(SyntaxKind.CloseBrace, LexerMode.StartOfLine);
                            // Rewind so we can insert more if necessary
                            _reader.Rewind(skipped);
                            return true;
                        }
                    }
                }

                // Now we've read the leading whitespace, start reading the
                // token
                if (!_reader.TryRead(out var value))
                    return Stop();

                if (value == (byte)'/')
                {
                    var (success, wasComment) = LexComment();

                    if (wasComment)
                        goto case LexerMode.EndOfLine;

                    return success;
                }

                return value switch
                {
                    (byte)'#'
                        => LexPreprocessorToken(),
                    (>= (byte)'A' and <= (byte)'Z') or
                    (>= (byte)'a' and <= (byte)'z') or
                    (byte)'_'
                        => LexIdentifier(LexerMode.MiddleOfLine),
                    var unknown
                        => Error($"Unknown token {unknown:X2}")
                };
            }
            case LexerMode.MiddleOfLine:
            {
                // Skip any whitespace between the previous token and the
                // current one
                _ = ReadWhitespace();

                Start();

                if (!_reader.TryRead(out var value))
                    return Stop();

                // If we've reached the end of line, rewind and restart parsing
                if (value == (byte)'\r' || value == (byte)'\n')
                {
                    _reader.Rewind(1);
                    goto case LexerMode.EndOfLine;
                }

                if (value == (byte)'/')
                {
                    var (success, wasComment) = LexComment();

                    if (wasComment)
                        goto case LexerMode.EndOfLine;

                    return success;
                }

                return value switch
                {
                    (byte)'!'
                        => Token(SyntaxKind.Exclamation,
                            LexerMode.MiddleOfLine),
                    (byte)'('
                        => Token(SyntaxKind.OpenParenthesis,
                            LexerMode.MiddleOfLine),
                    (byte)')'
                        => Token(SyntaxKind.CloseParenthesis,
                            LexerMode.MiddleOfLine),
                    (byte)'='
                        => LexEquals(),
                    (byte)'.'
                        => LexDot(),
                    (byte)'"' or (byte)'{' or (byte)'@'
                        => LexString(value),
                    (byte)'&'
                        => LexAmpersand(),
                    (>= (byte)'A' and <= (byte)'Z') or
                    (>= (byte)'a' and <= (byte)'z') or
                    (byte)'_'
                        => LexIdentifier(LexerMode.MiddleOfLine),
                    var unknown
                        => Error($"Unknown token {unknown:X2}")
                };
            }
            case LexerMode.EndOfLine:
            {
                Start();

                var skipped = _reader.AdvancePastAny((byte)'\r', (byte)'\n');

                if (skipped > 0)
                {
                    Token(SyntaxKind.EndOfLine, LexerMode.EndOfLine);
                    _lineNumber += CountLines(
                        _reader.Sequence.Slice(_start, _reader.Position));
                    return true;
                }
                else if (_beginBlock)
                {
                    _indentDepth++;
                    _beginBlock = false;
                    return Token(SyntaxKind.OpenBrace, LexerMode.StartOfLine);
                }
                else if (_isFinalBlock)
                {
                    // We have reached the end of the input stream. Start
                    // processing as if there's no more data.
                    goto case LexerMode.EndOfFile;
                }
                else
                {
                    // We haven't reached the end of the input stream. This
                    // must be a new line.
                    goto case LexerMode.StartOfLine;
                }
            }
            case LexerMode.EndOfFile:
            {
                Start();

                // Close out any remaining blocks
                if (--_indentDepth > 0)
                    return Token(SyntaxKind.CloseBrace, LexerMode.EndOfFile);

                // No more tokens in the stream, we are in the terminal state
                return Token(SyntaxKind.EndOfFile, LexerMode.EndOfFile);
            }
            default:
                Debug.Fail($"Invalid state ({_mode})");
                return false;
        }
    }
}
