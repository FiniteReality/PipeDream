namespace PipeDream.Compiler.Syntax;

public abstract partial class SyntaxVisitor
{
    /// <summary>
    /// Visits the given <see cref="AssignmentExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="AssignmentExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitAssignmentExpressionSyntax(AssignmentExpressionSyntax value)
    {
        VisitExpressionSyntax(value);
        Visit(value.Left);
        Visit(value.OperatorToken);
        Visit(value.Right);
    }

    /// <summary>
    /// Visits the given <see cref="BadDirectiveTriviaSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BadDirectiveTriviaSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitBadDirectiveTriviaSyntax(BadDirectiveTriviaSyntax value)
    {
        VisitDirectiveTriviaSyntax(value);
        if (value.Name != null)
            Visit(value.Name);
    }

    /// <summary>
    /// Visits the given <see cref="BinaryExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BinaryExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitBinaryExpressionSyntax(BinaryExpressionSyntax value)
    {
        VisitExpressionSyntax(value);
        Visit(value.Left);
        Visit(value.OperatorToken);
        Visit(value.Right);
    }

    /// <summary>
    /// Visits the given <see cref="BlockSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BlockSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitBlockSyntax(BlockSyntax value)
    {
        VisitStatementSyntax(value);
        Visit(value.OpenBraceToken);
        VisitSyntaxList(value.Statements);
        Visit(value.CloseBraceToken);
    }

    /// <summary>
    /// Visits the given <see cref="CompilationUnitSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="CompilationUnitSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitCompilationUnitSyntax(CompilationUnitSyntax value)
    {
        VisitSyntaxNode(value);
        VisitSyntaxList(value.Members);
        Visit(value.EndOfFileToken);
    }

    /// <summary>
    /// Visits the given <see cref="ConditionalExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="ConditionalExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitConditionalExpressionSyntax(ConditionalExpressionSyntax value)
    {
        VisitExpressionSyntax(value);
        Visit(value.Condition);
        Visit(value.QuestionToken);
        Visit(value.WhenTrue);
        Visit(value.ColonToken);
        Visit(value.WhenFalse);
    }

    /// <summary>
    /// Visits the given <see cref="DefineDirectiveTriviaSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="DefineDirectiveTriviaSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitDefineDirectiveTriviaSyntax(DefineDirectiveTriviaSyntax value)
    {
        VisitDirectiveTriviaSyntax(value);
        Visit(value.DefineKeyword);
        Visit(value.Name);
    }

    /// <summary>
    /// Visits the given <see cref="DirectiveTriviaSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="DirectiveTriviaSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitDirectiveTriviaSyntax(DirectiveTriviaSyntax value)
    {
        VisitTriviaSyntax(value);
        Visit(value.HashToken);
    }

    /// <summary>
    /// Visits the given <see cref="EqualsValueClauseSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="EqualsValueClauseSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitEqualsValueClauseSyntax(EqualsValueClauseSyntax value)
    {
        VisitSyntaxNode(value);
        Visit(value.EqualsToken);
        Visit(value.Value);
    }

    /// <summary>
    /// Visits the given <see cref="ExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="ExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitExpressionSyntax(ExpressionSyntax value)
    {
        VisitSyntaxNode(value);
    }

    /// <summary>
    /// Visits the given <see cref="IncludeDirectiveTriviaSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="IncludeDirectiveTriviaSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitIncludeDirectiveTriviaSyntax(IncludeDirectiveTriviaSyntax value)
    {
        VisitDirectiveTriviaSyntax(value);
        Visit(value.IncludeKeyword);
        Visit(value.File);
    }

    /// <summary>
    /// Visits the given <see cref="LiteralStringSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="LiteralStringSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitLiteralStringSyntax(LiteralStringSyntax value)
    {
        VisitStringSyntax(value);
        Visit(value.StringStartToken);
        Visit(value.Text);
        Visit(value.StringEndToken);
    }

    /// <summary>
    /// Visits the given <see cref="MemberDeclarationSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="MemberDeclarationSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitMemberDeclarationSyntax(MemberDeclarationSyntax value)
    {
        VisitSyntaxNode(value);
    }

    /// <summary>
    /// Visits the given <see cref="NameSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="NameSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitNameSyntax(NameSyntax value)
    {
        VisitExpressionSyntax(value);
    }

    /// <summary>
    /// Visits the given <see cref="NewExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="NewExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitNewExpressionSyntax(NewExpressionSyntax value)
    {
        VisitExpressionSyntax(value);
        Visit(value.NewKeyword);
        if (value.Type != null)
            Visit(value.Type);
    }

    /// <summary>
    /// Visits the given <see cref="PathExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="PathExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitPathExpressionSyntax(PathExpressionSyntax value)
    {
        VisitExpressionSyntax(value);
        Visit(value.PathOperator);
    }

    /// <summary>
    /// Visits the given <see cref="PostfixUnaryExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="PostfixUnaryExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitPostfixUnaryExpressionSyntax(PostfixUnaryExpressionSyntax value)
    {
        VisitExpressionSyntax(value);
        Visit(value.Operand);
        Visit(value.OperatorToken);
    }

    /// <summary>
    /// Visits the given <see cref="PrefixUnaryExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="PrefixUnaryExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitPrefixUnaryExpressionSyntax(PrefixUnaryExpressionSyntax value)
    {
        VisitExpressionSyntax(value);
        Visit(value.OperatorToken);
        Visit(value.Operand);
    }

    /// <summary>
    /// Visits the given <see cref="PreprocessorExpressionSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="PreprocessorExpressionSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitPreprocessorExpressionSyntax(PreprocessorExpressionSyntax value)
    {
        VisitExpressionSyntax(value);
    }

    /// <summary>
    /// Visits the given <see cref="QualifiedNameSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="QualifiedNameSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitQualifiedNameSyntax(QualifiedNameSyntax value)
    {
        VisitNameSyntax(value);
        VisitSeparatedSyntaxList(value.Parts);
    }

    /// <summary>
    /// Visits the given <see cref="RootedNameSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="RootedNameSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitRootedNameSyntax(RootedNameSyntax value)
    {
        VisitNameSyntax(value);
        Visit(value.PathRootToken);
        Visit(value.Name);
    }

    /// <summary>
    /// Visits the given <see cref="SimpleNameSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SimpleNameSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitSimpleNameSyntax(SimpleNameSyntax value)
    {
        VisitNameSyntax(value);
        Visit(value.Name);
    }

    /// <summary>
    /// Visits the given <see cref="SimpleTriviaSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SimpleTriviaSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitSimpleTriviaSyntax(SimpleTriviaSyntax value)
    {
        VisitTriviaSyntax(value);
    }

    /// <summary>
    /// Visits the given <see cref="SkippedTokensTriviaSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SkippedTokensTriviaSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitSkippedTokensTriviaSyntax(SkippedTokensTriviaSyntax value)
    {
        VisitTriviaSyntax(value);
        VisitSyntaxList(value.Tokens);
    }

    /// <summary>
    /// Visits the given <see cref="StatementSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="StatementSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitStatementSyntax(StatementSyntax value)
    {
        VisitSyntaxNode(value);
    }

    /// <summary>
    /// Visits the given <see cref="StringSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="StringSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitStringSyntax(StringSyntax value)
    {
        VisitSyntaxNode(value);
        Visit(value.StringStartToken);
        Visit(value.StringEndToken);
    }

    /// <summary>
    /// Visits the given <see cref="SyntaxNode"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SyntaxNode"/> to visit.
    /// </param>
    protected internal virtual void VisitSyntaxNode(SyntaxNode value)
    {
        VisitSyntaxList(value.LeadingTrivia);
        VisitSyntaxList(value.TrailingTrivia);
    }

    /// <summary>
    /// Visits the given <see cref="SyntaxToken"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SyntaxToken"/> to visit.
    /// </param>
    protected internal virtual void VisitSyntaxToken(SyntaxToken value)
    {
        VisitSyntaxNode(value);
    }

    /// <summary>
    /// Visits the given <see cref="TriviaSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="TriviaSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitTriviaSyntax(TriviaSyntax value)
    {
        VisitSyntaxNode(value);
    }

    /// <summary>
    /// Visits the given <see cref="VariableDeclarationSyntax"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="VariableDeclarationSyntax"/> to visit.
    /// </param>
    protected internal virtual void VisitVariableDeclarationSyntax(VariableDeclarationSyntax value)
    {
        VisitMemberDeclarationSyntax(value);
        Visit(value.VarKeyword);
        Visit(value.Identifier);
        if (value.Initializer != null)
            Visit(value.Initializer);
    }
}
