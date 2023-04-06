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
            SyntaxKind.GreaterThanGreaterThanToken or
            SyntaxKind.AsteriskEqualsToken or
            SyntaxKind.AmpersandToken or
            SyntaxKind.EqualsToken or
            SyntaxKind.MinusEqualsToken or
            SyntaxKind.PercentPercentEqualsToken or
            SyntaxKind.ColonColonToken or
            SyntaxKind.AsteriskToken or
            SyntaxKind.SlashToken or
            SyntaxKind.LessThanToken or
            SyntaxKind.ColonToken or
            SyntaxKind.GreaterThanGreaterThanEqualsToken or
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.SlashEqualsToken or
            SyntaxKind.QuestionDotToken or
            SyntaxKind.PercentEqualsToken or
            SyntaxKind.BarBarEqualsToken or
            SyntaxKind.BarToken or
            SyntaxKind.InKeyword or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.PercentPercentToken or
            SyntaxKind.CaretToken or
            SyntaxKind.CloseParenthesisToken or
            SyntaxKind.TildeExclamationToken or
            SyntaxKind.MinusMinusToken or
            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.PlusEqualsToken or
            SyntaxKind.OpenParenthesisToken or
            SyntaxKind.BarBarToken or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.MinusToken or
            SyntaxKind.CaretEqualsToken or
            SyntaxKind.LessThanLessThanEqualsToken or
            SyntaxKind.DotToken or
            SyntaxKind.ExclamationToken or
            SyntaxKind.AmpersandEqualsToken or
            SyntaxKind.AmpersandAmpersandEqualsToken or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.QuestionColonToken or
            SyntaxKind.ColonEqualsToken or
            SyntaxKind.AsteriskAsteriskToken or
            SyntaxKind.QuestionToken or
            SyntaxKind.BarEqualsToken or
            SyntaxKind.LessThanGreaterThanToken or
            SyntaxKind.TildeToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.EqualsEqualsToken or
            SyntaxKind.AmpersandAmpersandToken or
            SyntaxKind.PlusPlusToken or
            SyntaxKind.PercentToken or
            SyntaxKind.PlusToken
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
