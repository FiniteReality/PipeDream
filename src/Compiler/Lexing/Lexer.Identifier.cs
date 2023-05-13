using System.Buffers;
using System.Diagnostics;
using PipeDream.Compiler.Syntax;

namespace PipeDream.Compiler.Lexing;
internal ref partial struct Lexer
{
    private OperationStatus LexIdentifier(out LexerToken token)
    {
        // Identifiers cannot start with digits
        if (!_reader.TryPeek(out var c) || CharacterMaps.Digit.Contains(c))
        {
            Debug.Fail("LexIdentifier() with digit");
            token = default;
            return OperationStatus.InvalidData;
        }

        var status = _reader.TryAdvancePastAny(CharacterMaps.Identifier);
        if (status != OperationStatus.Done)
        {
            Debug.Assert(status != OperationStatus.InvalidData, "LexIdentifier() advance fail");
            token = default;
            return status;
        }

        status = _reader.TryGetString(out var identifier);
        if (status != OperationStatus.Done)
        {
            Debug.Assert(status != OperationStatus.InvalidData, "LexIdentifier() getstring fail");
            token = default;
            return status;
        }

        token = new(
            Kind: SyntaxKind.IdentifierToken,
            Start: _reader.TrackedPosition,
            End: _reader.Position)
        {
            StringValue = identifier
        };
        return OperationStatus.Done;
    }
}
