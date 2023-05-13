using System.Buffers;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeDream.Compiler.Syntax;

using static PipeDream.Compiler.Syntax.SyntaxKind;

namespace PipeDream.Compiler.Lexing;

internal static class KeywordLookup
{
    static KeywordLookup()
    {
        Array.Sort(Entries, (l, r) => string.CompareOrdinal(l.Item1, r.Item1));
    }

    public static readonly (string, SyntaxKind)[] Entries = new[]
    {
        ("area", AreaKeyword), ("as", AsKeyword), ("atom", AtomKeyword),
        ("break", BreakKeyword), ("call", CallKeyword),
        ("catch", CatchKeyword), ("client", ClientKeyword),
        ("const", ConstKeyword), ("continue", ContinueKeyword),
        ("database", DatabaseKeyword), ("datum", DatumKeyword),
        ("define", DefineKeyword), ("del", DelKeyword), ("do", DoKeyword),
        ("elif", ElifKeyword), ("else", ElseKeyword), ("endif", EndIfKeyword),
        ("error", ErrorKeyword), ("final", FinalKeyword), ("for", ForKeyword),
        ("global", GlobalKeyword), ("goto", GotoKeyword),
        ("icon", IconKeyword), ("ifdef", IfDefKeyword), ("if", IfKeyword),
        ("ifndef", IfNDefKeyword), ("image", ImageKeyword),
        ("include", IncludeKeyword), ("in", InKeyword), ("list", ListKeyword),
        ("matrix", MatrixKeyword), ("mob", MobKeyword),
        ("mutable_appearance", MutableAppearanceKeyword), ("new", NewKeyword),
        ("null", NullKeyword), ("obj", ObjKeyword),
        ("operator", OperatorKeyword), ("pipedream", PipeDreamKeyword),
        ("pragma", PragmaKeyword), ("proc", ProcKeyword),
        ("regex", RegexKeyword), ("return", ReturnKeyword),
        ("savefile", SavefileKeyword), ("set", SetKeyword),
        ("sleep", SleepKeyword), ("sound", SoundKeyword),
        ("spawn", SpawnKeyword), ("step", StepKeyword),
        ("switch", SwitchKeyword), ("text", TextKeyword),
        ("throw", ThrowKeyword), ("tmp", TmpKeyword), ("to", ToKeyword),
        ("try", TryKeyword), ("turf", TurfKeyword), ("undef", UndefKeyword),
        ("var", VarKeyword), ("verb", VerbKeyword), ("warn", WarnKeyword),
        ("while", WhileKeyword), ("world", WorldKeyword),
    };
}

internal ref partial struct Lexer
{
    private OperationStatus LexKeywordOrIdentifier(out LexerToken token)
    {
        var result = LexIdentifier(out token);
        if (result != OperationStatus.Done)
            return result;

        Debug.Assert(token.StringValue != null);
        Debug.Assert(token.StringValue.Length >= 1);

        if (!TryGetKeywordKind(token.StringValue, out var kind))
            return OperationStatus.Done;

        token = token with { Kind = kind };
        return OperationStatus.Done;

        static bool TryGetKeywordKind(string value, out SyntaxKind kind)
        {
            // Plain old binary search. MemoryMarshal/Unsafe are used here to
            // elide bounds checks.
            int start = 0, end = KeywordLookup.Entries.Length - 1;
            ref var startAddress = ref MemoryMarshal.GetArrayDataReference(
                KeywordLookup.Entries);
            do
            {
                var midpoint = (start + end) / 2;
                var entry = Unsafe.Add(ref startAddress, midpoint);
                switch (string.CompareOrdinal(value, entry.Item1))
                {
                    case < 0:
                        end = midpoint;
                        break;
                    case > 0:
                        start = midpoint + 1;
                        break;
                    case 0:
                        kind = entry.Item2;
                        return true;
                }
            } while (start < end);

            kind = default;
            return false;
        }
    }
}
