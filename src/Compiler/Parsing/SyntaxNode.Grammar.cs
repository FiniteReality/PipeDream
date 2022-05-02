using System.Collections.Immutable;

namespace PipeDream.Compiler.Parsing.Tree;

internal sealed record PreprocessorDefinitionNode(
    SyntaxNode DefineToken,
    SyntaxNode Name)
    : SyntaxNode(ComputeSpan(DefineToken, Name));

internal sealed record PreprocessorIfDefinitionNode(
    SyntaxNode IfDefToken,
    SyntaxNode Name)
    : SyntaxNode(ComputeSpan(IfDefToken, Name));

internal sealed record PreprocessorIfNode(
    SyntaxNode IfToken,
    SyntaxNode Name)
    : SyntaxNode(ComputeSpan(IfToken, Name));

internal sealed record PreprocessorInclusionNode(
    SyntaxNode IncludeToken,
    SyntaxNode FilePath)
    : SyntaxNode(ComputeSpan(IncludeToken, FilePath));

internal sealed record ValuedPreprocessorDefinitionNode(
    SyntaxNode Definition,
    ImmutableArray<SyntaxNode> Value)
    : SyntaxNode(ComputeSpan(Definition, Value));

internal sealed record BinaryOperationNode(
    SyntaxNode Left,
    SyntaxNode Operation,
    SyntaxNode Right)
    : SyntaxNode(ComputeSpan(Left, Right));

internal sealed record UnaryOperationNode(
    SyntaxNode Operation,
    SyntaxNode Right)
    : SyntaxNode(ComputeSpan(Operation, Right));

internal sealed record RootPathNode(
    SyntaxNode Slash,
    SyntaxNode Value)
    : SyntaxNode(ComputeSpan(Slash, Value));

internal sealed record AssignmentNode(
    SyntaxNode Left,
    SyntaxNode EqualSign,
    SyntaxNode Right)
    : SyntaxNode(ComputeSpan(Left, Right));

internal sealed record FunctionCallNode(
    SyntaxNode Name,
    SyntaxNode OpenParenthesis,
    ImmutableArray<SyntaxNode> Parameters,
    SyntaxNode CloseParenthesis)
    : SyntaxNode(ComputeSpan(Name, CloseParenthesis));