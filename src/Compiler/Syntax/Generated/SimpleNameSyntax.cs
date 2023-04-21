namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a simple name.
/// </summary>
public sealed partial record SimpleNameSyntax(
    SyntaxToken Name,
    SyntaxKind Kind,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : NameSyntax(
        Kind: ValidateKind(Kind, nameof(Kind)),
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
{
    private static SyntaxKind ValidateKind(SyntaxKind value, string paramName)
        => value switch
        {
            SyntaxKind.SimpleName
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };

    private SyntaxToken _name = ValidateName(Name, nameof(Name));

    /// <summary>
    /// Gets the syntax token representing the identifier of this name.
    /// </summary>
    public SyntaxToken Name
    {
        get => _name;
        init => _name = ValidateName(value, nameof(Name));
    }

    private static SyntaxToken ValidateName(SyntaxToken value, string paramName)
        => value.Kind switch
        {
            SyntaxKind.AreaKeyword or
            SyntaxKind.AtomKeyword or
            SyntaxKind.ClientKeyword or
            SyntaxKind.ConstKeyword or
            SyntaxKind.DatabaseKeyword or
            SyntaxKind.DatumKeyword or
            SyntaxKind.FinalKeyword or
            SyntaxKind.GlobalKeyword or
            SyntaxKind.IconKeyword or
            SyntaxKind.IdentifierToken or
            SyntaxKind.ImageKeyword or
            SyntaxKind.ListKeyword or
            SyntaxKind.MatrixKeyword or
            SyntaxKind.MutableAppearanceKeyword or
            SyntaxKind.NewKeyword or
            SyntaxKind.ObjKeyword or
            SyntaxKind.OperatorKeyword or
            SyntaxKind.ProcKeyword or
            SyntaxKind.RegexKeyword or
            SyntaxKind.SoundKeyword or
            SyntaxKind.TextKeyword or
            SyntaxKind.TmpKeyword or
            SyntaxKind.TurfKeyword or
            SyntaxKind.VarKeyword or
            SyntaxKind.VerbKeyword or
            SyntaxKind.WorldKeyword
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value}' is not a supported kind.",
                paramName)
        };
}
