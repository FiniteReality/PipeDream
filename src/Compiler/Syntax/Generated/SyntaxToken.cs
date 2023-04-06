namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record which represents a token in the syntax tree.
/// </summary>
public sealed partial record SyntaxToken(
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxTriviaList LeadingTrivia,
    SyntaxTriviaList TrailingTrivia)
    : SyntaxNode(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.AmpersandAmpersandEqualsToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.AmpersandEqualsToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.AreaKeyword or
            SyntaxKind.AsKeyword or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.AsteriskEqualsToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.AtomKeyword or
            SyntaxKind.BarBarEqualsToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.BarEqualsToken or
            SyntaxKind.BarToken or
            SyntaxKind.BreakKeyword or
            SyntaxKind.CallKeyword or
            SyntaxKind.CaretEqualsToken or
            SyntaxKind.CaretToken or
            SyntaxKind.CatchKeyword or
            SyntaxKind.ClientKeyword or
            SyntaxKind.CloseParenthesisToken or
            SyntaxKind.ColonColonToken or
            SyntaxKind.ColonEqualsToken or
            SyntaxKind.ColonToken or
            SyntaxKind.ConstKeyword or
            SyntaxKind.ContinueKeyword or
            SyntaxKind.DatabaseKeyword or
            SyntaxKind.DatumKeyword or
            SyntaxKind.DelKeyword or
            SyntaxKind.DoKeyword or
            SyntaxKind.DotToken or
            SyntaxKind.ElseKeyword or
            SyntaxKind.EqualsEqualsToken or
            SyntaxKind.EqualsToken or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.ExclamationToken or
            SyntaxKind.FinalKeyword or
            SyntaxKind.ForKeyword or
            SyntaxKind.GlobalKeyword or
            SyntaxKind.GotoKeyword or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.GreaterThanGreaterThanEqualsToken or
            SyntaxKind.GreaterThanGreaterThanToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.IconKeyword or
            SyntaxKind.IfKeyword or
            SyntaxKind.ImageKeyword or
            SyntaxKind.InKeyword or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.LessThanLessThanEqualsToken or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.ListKeyword or
            SyntaxKind.MatrixKeyword or
            SyntaxKind.MinusEqualsToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.MinusToken or
            SyntaxKind.MobKeyword or
            SyntaxKind.MutableAppearanceKeyword or
            SyntaxKind.NewKeyword or
            SyntaxKind.NullKeyword or
            SyntaxKind.ObjKeyword or
            SyntaxKind.OpenParenthesisToken or
            SyntaxKind.OperatorKeyword or
            SyntaxKind.PercentEqualsToken or
            SyntaxKind.PercentPercentEqualsToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.PercentToken or
            SyntaxKind.PlusEqualsToken or
            SyntaxKind.PlusPlusToken or
            SyntaxKind.PlusToken or
            SyntaxKind.ProcKeyword or
            SyntaxKind.QuestionColonToken or
            SyntaxKind.QuestionDotToken or
            SyntaxKind.QuestionToken or
            SyntaxKind.RegexKeyword or
            SyntaxKind.ReturnKeyword or
            SyntaxKind.SavefileKeyword or
            SyntaxKind.SetKeyword or
            SyntaxKind.SlashEqualsToken or
            SyntaxKind.SlashToken or
            SyntaxKind.SleepKeyword or
            SyntaxKind.SoundKeyword or
            SyntaxKind.SpawnKeyword or
            SyntaxKind.StepKeyword or
            SyntaxKind.SwitchKeyword or
            SyntaxKind.TextKeyword or
            SyntaxKind.ThrowKeyword or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.TildeToken or
            SyntaxKind.TmpKeyword or
            SyntaxKind.ToKeyword or
            SyntaxKind.TryKeyword or
            SyntaxKind.TurfKeyword or
            SyntaxKind.VarKeyword or
            SyntaxKind.VerbKeyword or
            SyntaxKind.WhileKeyword or
            SyntaxKind.WorldKeyword
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
