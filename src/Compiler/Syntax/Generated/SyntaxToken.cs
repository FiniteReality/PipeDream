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
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.CatchKeyword or
            SyntaxKind.SavefileKeyword or
            SyntaxKind.DelKeyword or
            SyntaxKind.CloseParenthesisToken or
            SyntaxKind.PlusPlusToken or
            SyntaxKind.PercentEqualsToken or
            SyntaxKind.TildeToken or
            SyntaxKind.CaretEqualsToken or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.AreaKeyword or
            SyntaxKind.SleepKeyword or
            SyntaxKind.MobKeyword or
            SyntaxKind.IfKeyword or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.StepKeyword or
            SyntaxKind.PlusToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.MinusEqualsToken or
            SyntaxKind.ObjKeyword or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.GreaterThanGreaterThanEqualsToken or
            SyntaxKind.ColonColonToken or
            SyntaxKind.GotoKeyword or
            SyntaxKind.ImageKeyword or
            SyntaxKind.ContinueKeyword or
            SyntaxKind.EqualsEqualsToken or
            SyntaxKind.ClientKeyword or
            SyntaxKind.DoKeyword or
            SyntaxKind.TextKeyword or
            SyntaxKind.BarEqualsToken or
            SyntaxKind.SlashToken or
            SyntaxKind.DatabaseKeyword or
            SyntaxKind.MatrixKeyword or
            SyntaxKind.IconKeyword or
            SyntaxKind.OperatorKeyword or
            SyntaxKind.BreakKeyword or
            SyntaxKind.ListKeyword or
            SyntaxKind.LessThanToken or
            SyntaxKind.VerbKeyword or
            SyntaxKind.ReturnKeyword or
            SyntaxKind.WorldKeyword or
            SyntaxKind.OpenParenthesisToken or
            SyntaxKind.CaretToken or
            SyntaxKind.NewKeyword or
            SyntaxKind.AsteriskEqualsToken or
            SyntaxKind.DatumKeyword or
            SyntaxKind.SetKeyword or
            SyntaxKind.WhileKeyword or
            SyntaxKind.PercentPercentEqualsToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.ForKeyword or
            SyntaxKind.AtomKeyword or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.TmpKeyword or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.EqualsToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.TryKeyword or
            SyntaxKind.SlashEqualsToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.BarToken or
            SyntaxKind.InKeyword or
            SyntaxKind.ThrowKeyword or
            SyntaxKind.AmpersandEqualsToken or
            SyntaxKind.PercentToken or
            SyntaxKind.ColonToken or
            SyntaxKind.TurfKeyword or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.SpawnKeyword or
            SyntaxKind.VarKeyword or
            SyntaxKind.QuestionToken or
            SyntaxKind.GlobalKeyword or
            SyntaxKind.AsKeyword or
            SyntaxKind.AsteriskToken or
            SyntaxKind.CallKeyword or
            SyntaxKind.ProcKeyword or
            SyntaxKind.MutableAppearanceKeyword or
            SyntaxKind.AmpersandAmpersandEqualsToken or
            SyntaxKind.SwitchKeyword or
            SyntaxKind.DotToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.ToKeyword or
            SyntaxKind.QuestionColonToken or
            SyntaxKind.QuestionDotToken or
            SyntaxKind.NullKeyword or
            SyntaxKind.BarBarEqualsToken or
            SyntaxKind.RegexKeyword or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.ColonEqualsToken or
            SyntaxKind.SoundKeyword or
            SyntaxKind.MinusToken or
            SyntaxKind.PlusEqualsToken or
            SyntaxKind.FinalKeyword or
            SyntaxKind.LessThanLessThanEqualsToken or
            SyntaxKind.ExclamationToken or
            SyntaxKind.ConstKeyword or
            SyntaxKind.ElseKeyword or
            SyntaxKind.GreaterThanGreaterThanToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
