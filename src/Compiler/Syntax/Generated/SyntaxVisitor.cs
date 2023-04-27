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
        VisitNode(value.Left);
        VisitNode(value.OperatorToken);
        VisitNode(value.Right);
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
            VisitNode(value.Name);
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
        VisitNode(value.Left);
        VisitNode(value.OperatorToken);
        VisitNode(value.Right);
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
        VisitNode(value.OpenBraceToken);
        VisitSyntaxList(value.Statements);
        VisitNode(value.CloseBraceToken);
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
        VisitNode(value.Condition);
        VisitNode(value.QuestionToken);
        VisitNode(value.WhenTrue);
        VisitNode(value.ColonToken);
        VisitNode(value.WhenFalse);
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
        VisitNode(value.DefineKeyword);
        VisitNode(value.Name);
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
        VisitNode(value.HashToken);
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
        VisitNode(value.IncludeKeyword);
        VisitNode(value.File);
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
        VisitNode(value.StringStartToken);
        VisitNode(value.Text);
        VisitNode(value.StringEndToken);
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
        VisitNode(value.NewKeyword);
        if (value.Type != null)
            VisitNode(value.Type);
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
        VisitNode(value.Operand);
        VisitNode(value.OperatorToken);
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
        VisitNode(value.OperatorToken);
        VisitNode(value.Operand);
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
        VisitNode(value.PathRootToken);
        VisitNode(value.Name);
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
        VisitNode(value.Name);
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
        VisitNode(value.StringStartToken);
        VisitNode(value.StringEndToken);
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
}
