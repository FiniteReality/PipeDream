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
            SyntaxKind.IconKeyword or
            SyntaxKind.BreakKeyword or
            SyntaxKind.TildeToken or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.LessThanLessThanEqualsToken or
            SyntaxKind.QuestionDotToken or
            SyntaxKind.QuestionToken or
            SyntaxKind.WorldKeyword or
            SyntaxKind.ConstKeyword or
            SyntaxKind.SpawnKeyword or
            SyntaxKind.MinusEqualsToken or
            SyntaxKind.ToKeyword or
            SyntaxKind.VerbKeyword or
            SyntaxKind.DatabaseKeyword or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.ElseKeyword or
            SyntaxKind.MobKeyword or
            SyntaxKind.EqualsEqualsToken or
            SyntaxKind.DelKeyword or
            SyntaxKind.ForKeyword or
            SyntaxKind.GreaterThanGreaterThanToken or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.PlusToken or
            SyntaxKind.NewKeyword or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.CaretToken or
            SyntaxKind.QuestionColonToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.SwitchKeyword or
            SyntaxKind.CatchKeyword or
            SyntaxKind.MatrixKeyword or
            SyntaxKind.ColonToken or
            SyntaxKind.OpenParenthesisToken or
            SyntaxKind.TextKeyword or
            SyntaxKind.GlobalKeyword or
            SyntaxKind.AtomKeyword or
            SyntaxKind.ProcKeyword or
            SyntaxKind.AmpersandToken or
            SyntaxKind.PercentPercentEqualsToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.ListKeyword or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.FinalKeyword or
            SyntaxKind.SleepKeyword or
            SyntaxKind.PlusPlusToken or
            SyntaxKind.GotoKeyword or
            SyntaxKind.GreaterThanGreaterThanEqualsToken or
            SyntaxKind.IfKeyword or
            SyntaxKind.OperatorKeyword or
            SyntaxKind.StepKeyword or
            SyntaxKind.SlashToken or
            SyntaxKind.WhileKeyword or
            SyntaxKind.ReturnKeyword or
            SyntaxKind.PercentToken or
            SyntaxKind.BarEqualsToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.ClientKeyword or
            SyntaxKind.TryKeyword or
            SyntaxKind.DoKeyword or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.SetKeyword or
            SyntaxKind.EqualsToken or
            SyntaxKind.ColonEqualsToken or
            SyntaxKind.RegexKeyword or
            SyntaxKind.BarBarEqualsToken or
            SyntaxKind.AreaKeyword or
            SyntaxKind.CaretEqualsToken or
            SyntaxKind.AmpersandAmpersandEqualsToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.SavefileKeyword or
            SyntaxKind.ThrowKeyword or
            SyntaxKind.AsteriskEqualsToken or
            SyntaxKind.ObjKeyword or
            SyntaxKind.VarKeyword or
            SyntaxKind.AsKeyword or
            SyntaxKind.SoundKeyword or
            SyntaxKind.ImageKeyword or
            SyntaxKind.ExclamationToken or
            SyntaxKind.BarToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.ColonColonToken or
            SyntaxKind.DotToken or
            SyntaxKind.TmpKeyword or
            SyntaxKind.AmpersandEqualsToken or
            SyntaxKind.NullKeyword or
            SyntaxKind.AsteriskToken or
            SyntaxKind.TurfKeyword or
            SyntaxKind.SlashEqualsToken or
            SyntaxKind.CloseParenthesisToken or
            SyntaxKind.MutableAppearanceKeyword or
            SyntaxKind.PlusEqualsToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.ContinueKeyword or
            SyntaxKind.MinusToken or
            SyntaxKind.InKeyword or
            SyntaxKind.DatumKeyword or
            SyntaxKind.CallKeyword or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.PercentEqualsToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
