namespace PipeDream.Compiler.Syntax;

/// <summary>
/// Defines a record representing a simple name.
/// </summary>
public sealed partial record SimpleNameSyntax(
    SyntaxToken Name,
    SyntaxSpan Span,
    SyntaxList<TriviaSyntax> LeadingTrivia,
    SyntaxList<TriviaSyntax> TrailingTrivia)
    : NameSyntax(
        Kind: SyntaxKind.SimpleName,
        Span: Span,
        LeadingTrivia: LeadingTrivia,
        TrailingTrivia: TrailingTrivia)
    , IVisitable<SimpleNameSyntax>
{
    static void IVisitable<SimpleNameSyntax>.Accept<TVisitor>(SimpleNameSyntax node, TVisitor visitor)
        => visitor.VisitSimpleNameSyntax(node);

    void IVisitable.Accept<TVisitor>(TVisitor visitor)
        => visitor.VisitNode(this);

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
            SyntaxKind.DefineKeyword or
            SyntaxKind.ElifKeyword or
            SyntaxKind.EndIfKeyword or
            SyntaxKind.ErrorKeyword or
            SyntaxKind.FinalKeyword or
            SyntaxKind.GlobalKeyword or
            SyntaxKind.IconKeyword or
            SyntaxKind.IdentifierToken or
            SyntaxKind.IfDefKeyword or
            SyntaxKind.IfNDefKeyword or
            SyntaxKind.ImageKeyword or
            SyntaxKind.IncludeKeyword or
            SyntaxKind.ListKeyword or
            SyntaxKind.MatrixKeyword or
            SyntaxKind.MobKeyword or
            SyntaxKind.MutableAppearanceKeyword or
            SyntaxKind.NewKeyword or
            SyntaxKind.ObjKeyword or
            SyntaxKind.OperatorKeyword or
            SyntaxKind.PipeDreamKeyword or
            SyntaxKind.PragmaKeyword or
            SyntaxKind.ProcKeyword or
            SyntaxKind.RegexKeyword or
            SyntaxKind.SoundKeyword or
            SyntaxKind.TextKeyword or
            SyntaxKind.TmpKeyword or
            SyntaxKind.TurfKeyword or
            SyntaxKind.UndefKeyword or
            SyntaxKind.VarKeyword or
            SyntaxKind.VerbKeyword or
            SyntaxKind.WarnKeyword or
            SyntaxKind.WorldKeyword
                => value,
            _ => throw new ArgumentException(
                $"The kind '{value.Kind}' is not a supported kind.",
                paramName)
        };
}
