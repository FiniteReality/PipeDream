using System.Buffers;
using System.Buffers.Text;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

internal ref partial struct Lexer
{
    private OperationStatus LexNumber(out LexerToken token)
    {
        var status = _reader.TryAdvancePastAny(CharacterMaps.Digit);
        if (status != OperationStatus.Done)
        {
            Debug.Assert(status != OperationStatus.InvalidData, "LexNumber advance fail");
            token = default;
            return status;
        }

        if (!_reader.TryPeek(out var trailer))
        {
            token = default;
            return OperationStatus.NeedMoreData;
        }

        if (trailer == (byte)'.')
        {
            _reader.Advance();
            status = _reader.TryAdvancePastAny(CharacterMaps.Digit);
            if (status != OperationStatus.Done)
            {
                Debug.Assert(status != OperationStatus.InvalidData, "LexNumber advance fail");
                token = default;
                return status;
            }
        }

        status = _reader.TryGetString(out var number);
        if (status != OperationStatus.Done)
        {
            Debug.Assert(status != OperationStatus.InvalidData, "LexNumber getstring fail");
            token = default;
            return status;
        }

        // TODO: actually convert it to a float here?

        token = new(
            Kind: SyntaxKind.NumericLiteralToken,
            Start: _reader.TrackedPosition,
            End: _reader.Position)
        {
            StringValue = number
        };
        return OperationStatus.Done;
    }
}
