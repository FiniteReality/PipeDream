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
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.ColonColonToken or
            SyntaxKind.QuestionDotToken or
            SyntaxKind.MinusEqualsToken or
            SyntaxKind.PercentPercentEqualsToken or
            SyntaxKind.TryKeyword or
            SyntaxKind.VerbKeyword or
            SyntaxKind.MatrixKeyword or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.CatchKeyword or
            SyntaxKind.ExclamationToken or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.AmpersandAmpersandEqualsToken or
            SyntaxKind.PlusToken or
            SyntaxKind.IfKeyword or
            SyntaxKind.NewKeyword or
            SyntaxKind.SpawnKeyword or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.SlashEqualsToken or
            SyntaxKind.CaretToken or
            SyntaxKind.SetKeyword or
            SyntaxKind.GotoKeyword or
            SyntaxKind.BarBarToken or
            SyntaxKind.StepKeyword or
            SyntaxKind.SavefileKeyword or
            SyntaxKind.ConstKeyword or
            SyntaxKind.ContinueKeyword or
            SyntaxKind.PercentToken or
            SyntaxKind.AreaKeyword or
            SyntaxKind.PlusEqualsToken or
            SyntaxKind.ThrowKeyword or
            SyntaxKind.NullKeyword or
            SyntaxKind.TmpKeyword or
            SyntaxKind.BarToken or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.ReturnKeyword or
            SyntaxKind.GreaterThanGreaterThanToken or
            SyntaxKind.ForKeyword or
            SyntaxKind.DoKeyword or
            SyntaxKind.PercentEqualsToken or
            SyntaxKind.AsKeyword or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.AsteriskEqualsToken or
            SyntaxKind.WorldKeyword or
            SyntaxKind.InKeyword or
            SyntaxKind.OperatorKeyword or
            SyntaxKind.EqualsToken or
            SyntaxKind.SlashToken or
            SyntaxKind.LessThanLessThanEqualsToken or
            SyntaxKind.QuestionColonToken or
            SyntaxKind.ColonEqualsToken or
            SyntaxKind.ObjKeyword or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.MinusToken or
            SyntaxKind.MutableAppearanceKeyword or
            SyntaxKind.DatabaseKeyword or
            SyntaxKind.SwitchKeyword or
            SyntaxKind.AmpersandEqualsToken or
            SyntaxKind.RegexKeyword or
            SyntaxKind.CaretEqualsToken or
            SyntaxKind.TurfKeyword or
            SyntaxKind.BarEqualsToken or
            SyntaxKind.PlusPlusToken or
            SyntaxKind.SleepKeyword or
            SyntaxKind.TildeToken or
            SyntaxKind.OpenParenthesisToken or
            SyntaxKind.ToKeyword or
            SyntaxKind.FinalKeyword or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.WhileKeyword or
            SyntaxKind.ProcKeyword or
            SyntaxKind.VarKeyword or
            SyntaxKind.LessThanToken or
            SyntaxKind.BreakKeyword or
            SyntaxKind.CallKeyword or
            SyntaxKind.AsteriskToken or
            SyntaxKind.MobKeyword or
            SyntaxKind.ColonToken or
            SyntaxKind.ListKeyword or
            SyntaxKind.AtomKeyword or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.GlobalKeyword or
            SyntaxKind.QuestionToken or
            SyntaxKind.TextKeyword or
            SyntaxKind.SoundKeyword or
            SyntaxKind.ElseKeyword or
            SyntaxKind.DelKeyword or
            SyntaxKind.CloseParenthesisToken or
            SyntaxKind.BarBarEqualsToken or
            SyntaxKind.IconKeyword or
            SyntaxKind.GreaterThanGreaterThanEqualsToken or
            SyntaxKind.ImageKeyword or
            SyntaxKind.DotToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.DatumKeyword or
            SyntaxKind.ClientKeyword or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.EqualsEqualsToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
