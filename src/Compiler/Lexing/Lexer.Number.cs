using System.Buffers;
using System.Buffers.Text;
using System.Collections.Immutable;
using System.Diagnostics;
using PipeDream.Compiler.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;

public ref partial struct Lexer
{
    private OperationStatus LexNumber(out LexerToken token)
    {
        var status = _reader.TryAdvancePastAny(CharacterMaps.Digit);
        if (status != OperationStatus.Done)
        {
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
            status = _reader.TryAdvancePastAny(CharacterMaps.Digit);
            if (status != OperationStatus.Done)
            {
                token = default;
                return status;
            }
        }

        status = _reader.TryGetString(out var number);
        if (status != OperationStatus.Done)
        {
            token = default;
            return status;
        }

        // TODO: actually convert it to a float here?

        token = new(
            Kind: SyntaxKind.NumberToken,
            Start: _reader.TrackedPosition,
            End: _reader.Position)
        {
            StringValue = number
        };
        return OperationStatus.InvalidData;
    }
}