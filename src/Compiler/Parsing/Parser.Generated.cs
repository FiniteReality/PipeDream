#nullable enable
using PipeDream.Compiler.Lexing;
using PipeDream.Compiler.Parsing.Tree;

namespace PipeDream.Compiler.Parsing;

public partial struct Parser
{
    private bool Decide((int state, SyntaxNode? node) current, Token lookahead)
        => (current.state, lookahead.Kind) switch
        {
            (0, SyntaxKind.PreprocessorDefine) // Shift (11, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, EndOfFile])
                => Shift(11),
            (0, SyntaxKind.PreprocessorEndIf) // Shift (13, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, EndOfFile])
                => Shift(13),
            (0, SyntaxKind.PreprocessorIf) // Shift (14, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, EndOfFile])
                => Shift(14),
            (0, SyntaxKind.PreprocessorIfDef) // Shift (15, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, EndOfFile])
                => Shift(15),
            (0, SyntaxKind.PreprocessorInclude) // Shift (16, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, EndOfFile])
                => Shift(16),
            (0, SyntaxKind.OpenBrace) // Shift (4, [Block -> · OpenBrace StatementList CloseBrace, EndOfFile])
                => Shift(4),
            (0, SyntaxKind.EndOfLine) // Shift (9, [StatementTerminator -> · EndOfLine, EndOfFile])
                => Shift(9),
            (0, SyntaxKind.Semicolon) // Shift (10, [StatementTerminator -> · Semicolon, EndOfFile])
                => Shift(10),
            (0, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (0, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (0, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (0, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (0, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (0, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (1, SyntaxKind.EndOfFile) // Accept ([S' -> CompilationUnit ·, EndOfFile])
                => Accept(),
            (2, SyntaxKind.EndOfFile) // Shift (22, [CompilationUnit -> StatementList · EndOfFile, EndOfFile])
                => Shift(22),
            (2, SyntaxKind.PreprocessorDefine) // Shift (11, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, EndOfFile])
                => Shift(11),
            (2, SyntaxKind.PreprocessorEndIf) // Shift (13, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, EndOfFile])
                => Shift(13),
            (2, SyntaxKind.PreprocessorIf) // Shift (14, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, EndOfFile])
                => Shift(14),
            (2, SyntaxKind.PreprocessorIfDef) // Shift (15, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, EndOfFile])
                => Shift(15),
            (2, SyntaxKind.PreprocessorInclude) // Shift (16, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, EndOfFile])
                => Shift(16),
            (2, SyntaxKind.OpenBrace) // Shift (4, [Block -> · OpenBrace StatementList CloseBrace, EndOfFile])
                => Shift(4),
            (2, SyntaxKind.EndOfLine) // Shift (9, [StatementTerminator -> · EndOfLine, EndOfFile])
                => Shift(9),
            (2, SyntaxKind.Semicolon) // Shift (10, [StatementTerminator -> · Semicolon, EndOfFile])
                => Shift(10),
            (2, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (2, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (2, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (2, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (2, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (2, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (3, SyntaxKind.EndOfFile) // Reduce (StatementList -> Block, [StatementList -> Block ·, EndOfFile])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, EndOfFile]")
                        )
                    },
            (3, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorDefine]")
                        )
                    },
            (3, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorEndIf]")
                        )
                    },
            (3, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorIf]")
                        )
                    },
            (3, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorIfDef]")
                        )
                    },
            (3, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorInclude]")
                        )
                    },
            (3, SyntaxKind.EndOfLine) // Reduce (StatementList -> Block, [StatementList -> Block ·, EndOfLine])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, EndOfLine]")
                        )
                    },
            (3, SyntaxKind.Semicolon) // Reduce (StatementList -> Block, [StatementList -> Block ·, Semicolon])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Semicolon]")
                        )
                    },
            (3, SyntaxKind.Dot) // Reduce (StatementList -> Block, [StatementList -> Block ·, Dot])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Dot]")
                        )
                    },
            (3, SyntaxKind.Identifier) // Reduce (StatementList -> Block, [StatementList -> Block ·, Identifier])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Identifier]")
                        )
                    },
            (3, SyntaxKind.Slash) // Reduce (StatementList -> Block, [StatementList -> Block ·, Slash])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Slash]")
                        )
                    },
            (3, SyntaxKind.Exclamation) // Reduce (StatementList -> Block, [StatementList -> Block ·, Exclamation])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Exclamation]")
                        )
                    },
            (3, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> Block, [StatementList -> Block ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, OpenParenthesis]")
                        )
                    },
            (3, SyntaxKind.DoubleDot) // Reduce (StatementList -> Block, [StatementList -> Block ·, DoubleDot])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, DoubleDot]")
                        )
                    },
            (3, SyntaxKind.OpenBrace) // Reduce (StatementList -> Block, [StatementList -> Block ·, OpenBrace])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, OpenBrace]")
                        )
                    },
            (4, SyntaxKind.PreprocessorDefine) // Shift (35, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(35),
            (4, SyntaxKind.PreprocessorEndIf) // Shift (36, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, CloseBrace])
                => Shift(36),
            (4, SyntaxKind.PreprocessorIf) // Shift (37, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, CloseBrace])
                => Shift(37),
            (4, SyntaxKind.PreprocessorIfDef) // Shift (38, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, CloseBrace])
                => Shift(38),
            (4, SyntaxKind.PreprocessorInclude) // Shift (39, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, CloseBrace])
                => Shift(39),
            (4, SyntaxKind.OpenBrace) // Shift (28, [Block -> · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(28),
            (4, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (4, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (4, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (4, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (4, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (4, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (4, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (4, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (5, SyntaxKind.Equals) // Shift (42, [Statement -> Expression · Equals Expression StatementTerminator, EndOfFile])
                => Shift(42),
            (5, SyntaxKind.OpenBrace) // Shift (40, [Block -> Expression · OpenBrace StatementList CloseBrace, EndOfFile])
                => Shift(40),
            (5, SyntaxKind.Slash) // Shift (43, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(43),
            (5, SyntaxKind.DoubleAmpersand) // Shift (44, [Expression -> Expression · DoubleAmpersand Expression, EndOfLine])
                => Shift(44),
            (5, SyntaxKind.EndOfLine) // Shift (9, [StatementTerminator -> · EndOfLine, EndOfFile])
                => Shift(9),
            (5, SyntaxKind.Semicolon) // Shift (10, [StatementTerminator -> · Semicolon, EndOfFile])
                => Shift(10),
            (6, SyntaxKind.EndOfFile) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, EndOfFile])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, EndOfFile]")
                        )
                    },
            (6, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorDefine]")
                        )
                    },
            (6, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorEndIf]")
                        )
                    },
            (6, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorIf]")
                        )
                    },
            (6, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorIfDef]")
                        )
                    },
            (6, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorInclude]")
                        )
                    },
            (6, SyntaxKind.EndOfLine) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, EndOfLine])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, EndOfLine]")
                        )
                    },
            (6, SyntaxKind.Semicolon) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Semicolon])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Semicolon]")
                        )
                    },
            (6, SyntaxKind.Dot) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Dot])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Dot]")
                        )
                    },
            (6, SyntaxKind.Identifier) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Identifier])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Identifier]")
                        )
                    },
            (6, SyntaxKind.Slash) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Slash])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Slash]")
                        )
                    },
            (6, SyntaxKind.Exclamation) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Exclamation])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Exclamation]")
                        )
                    },
            (6, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, OpenParenthesis]")
                        )
                    },
            (6, SyntaxKind.DoubleDot) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, DoubleDot])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, DoubleDot]")
                        )
                    },
            (6, SyntaxKind.OpenBrace) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, OpenBrace])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, OpenBrace]")
                        )
                    },
            (7, SyntaxKind.EndOfFile) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, EndOfFile])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, EndOfFile]")
                        )
                    },
            (7, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorDefine]")
                        )
                    },
            (7, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorEndIf]")
                        )
                    },
            (7, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorIf]")
                        )
                    },
            (7, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorIfDef]")
                        )
                    },
            (7, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorInclude]")
                        )
                    },
            (7, SyntaxKind.EndOfLine) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, EndOfLine])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, EndOfLine]")
                        )
                    },
            (7, SyntaxKind.Semicolon) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Semicolon])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Semicolon]")
                        )
                    },
            (7, SyntaxKind.Dot) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Dot])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Dot]")
                        )
                    },
            (7, SyntaxKind.Identifier) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Identifier])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Identifier]")
                        )
                    },
            (7, SyntaxKind.Slash) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Slash])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Slash]")
                        )
                    },
            (7, SyntaxKind.Exclamation) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Exclamation])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Exclamation]")
                        )
                    },
            (7, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, OpenParenthesis]")
                        )
                    },
            (7, SyntaxKind.DoubleDot) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, DoubleDot])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, DoubleDot]")
                        )
                    },
            (7, SyntaxKind.OpenBrace) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, OpenBrace])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, OpenBrace]")
                        )
                    },
            (8, SyntaxKind.EndOfFile) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, EndOfFile])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, EndOfFile]")
                        )
                    },
            (8, SyntaxKind.PreprocessorDefine) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorDefine]")
                        )
                    },
            (8, SyntaxKind.PreprocessorEndIf) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorEndIf]")
                        )
                    },
            (8, SyntaxKind.PreprocessorIf) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorIf]")
                        )
                    },
            (8, SyntaxKind.PreprocessorIfDef) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorIfDef]")
                        )
                    },
            (8, SyntaxKind.PreprocessorInclude) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorInclude]")
                        )
                    },
            (8, SyntaxKind.EndOfLine) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, EndOfLine])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, EndOfLine]")
                        )
                    },
            (8, SyntaxKind.Semicolon) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Semicolon])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Semicolon]")
                        )
                    },
            (8, SyntaxKind.Dot) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Dot])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Dot]")
                        )
                    },
            (8, SyntaxKind.Identifier) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Identifier])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Identifier]")
                        )
                    },
            (8, SyntaxKind.Slash) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Slash])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Slash]")
                        )
                    },
            (8, SyntaxKind.Exclamation) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Exclamation])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Exclamation]")
                        )
                    },
            (8, SyntaxKind.OpenParenthesis) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, OpenParenthesis]")
                        )
                    },
            (8, SyntaxKind.DoubleDot) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, DoubleDot])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, DoubleDot]")
                        )
                    },
            (8, SyntaxKind.OpenBrace) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, OpenBrace])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, OpenBrace]")
                        )
                    },
            (9, SyntaxKind.EndOfFile) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, EndOfFile])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, EndOfFile]")
                        )
                    },
            (9, SyntaxKind.PreprocessorDefine) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (9, SyntaxKind.PreprocessorEndIf) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (9, SyntaxKind.PreprocessorIf) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (9, SyntaxKind.PreprocessorIfDef) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (9, SyntaxKind.PreprocessorInclude) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (9, SyntaxKind.EndOfLine) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, EndOfLine])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, EndOfLine]")
                        )
                    },
            (9, SyntaxKind.Semicolon) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Semicolon])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Semicolon]")
                        )
                    },
            (9, SyntaxKind.Dot) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Dot])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Dot]")
                        )
                    },
            (9, SyntaxKind.Identifier) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Identifier])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Identifier]")
                        )
                    },
            (9, SyntaxKind.Slash) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Slash])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Slash]")
                        )
                    },
            (9, SyntaxKind.Exclamation) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Exclamation])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Exclamation]")
                        )
                    },
            (9, SyntaxKind.OpenParenthesis) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (9, SyntaxKind.DoubleDot) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, DoubleDot])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, DoubleDot]")
                        )
                    },
            (9, SyntaxKind.OpenBrace) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, OpenBrace])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, OpenBrace]")
                        )
                    },
            (10, SyntaxKind.EndOfFile) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, EndOfFile])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, EndOfFile]")
                        )
                    },
            (10, SyntaxKind.PreprocessorDefine) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorDefine]")
                        )
                    },
            (10, SyntaxKind.PreprocessorEndIf) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorEndIf]")
                        )
                    },
            (10, SyntaxKind.PreprocessorIf) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorIf]")
                        )
                    },
            (10, SyntaxKind.PreprocessorIfDef) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorIfDef]")
                        )
                    },
            (10, SyntaxKind.PreprocessorInclude) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorInclude]")
                        )
                    },
            (10, SyntaxKind.EndOfLine) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, EndOfLine])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, EndOfLine]")
                        )
                    },
            (10, SyntaxKind.Semicolon) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Semicolon])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Semicolon]")
                        )
                    },
            (10, SyntaxKind.Dot) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Dot])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Dot]")
                        )
                    },
            (10, SyntaxKind.Identifier) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Identifier])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Identifier]")
                        )
                    },
            (10, SyntaxKind.Slash) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Slash])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Slash]")
                        )
                    },
            (10, SyntaxKind.Exclamation) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Exclamation])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Exclamation]")
                        )
                    },
            (10, SyntaxKind.OpenParenthesis) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, OpenParenthesis]")
                        )
                    },
            (10, SyntaxKind.DoubleDot) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, DoubleDot])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, DoubleDot]")
                        )
                    },
            (10, SyntaxKind.OpenBrace) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, OpenBrace])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, OpenBrace]")
                        )
                    },
            (11, SyntaxKind.Identifier) // Shift (45, [PreprocessorStatement -> PreprocessorDefine · Identifier PreprocessorDefinitionValue EndOfLine, EndOfFile])
                => Shift(45),
            (12, SyntaxKind.EndOfLine) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, EndOfLine])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, EndOfLine]")
                        )
                    },
            (12, SyntaxKind.Semicolon) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Semicolon])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Semicolon]")
                        )
                    },
            (12, SyntaxKind.OpenParenthesis) // Shift (46, [Expression -> Identifier · OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(46),
            (12, SyntaxKind.Equals) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Equals])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Equals]")
                        )
                    },
            (12, SyntaxKind.OpenBrace) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, OpenBrace])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, OpenBrace]")
                        )
                    },
            (12, SyntaxKind.Slash) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Slash])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Slash]")
                        )
                    },
            (12, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, DoubleAmpersand]")
                        )
                    },
            (13, SyntaxKind.EndOfLine) // Shift (47, [PreprocessorStatement -> PreprocessorEndIf · EndOfLine, EndOfFile])
                => Shift(47),
            (14, SyntaxKind.Dot) // Shift (50, [Expression -> · Dot, EndOfLine])
                => Shift(50),
            (14, SyntaxKind.Identifier) // Shift (49, [Expression -> · Identifier, EndOfLine])
                => Shift(49),
            (14, SyntaxKind.Slash) // Shift (51, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(51),
            (14, SyntaxKind.Exclamation) // Shift (52, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(52),
            (14, SyntaxKind.OpenParenthesis) // Shift (53, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(53),
            (14, SyntaxKind.DoubleDot) // Shift (54, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(54),
            (15, SyntaxKind.Identifier) // Shift (55, [PreprocessorStatement -> PreprocessorIfDef · Identifier EndOfLine, EndOfFile])
                => Shift(55),
            (16, SyntaxKind.String) // Shift (56, [PreprocessorStatement -> PreprocessorInclude · String EndOfLine, EndOfFile])
                => Shift(56),
            (17, SyntaxKind.EndOfLine) // Reduce (Expression -> Dot, [Expression -> Dot ·, EndOfLine])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, EndOfLine]")
                        )
                    },
            (17, SyntaxKind.Semicolon) // Reduce (Expression -> Dot, [Expression -> Dot ·, Semicolon])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Semicolon]")
                        )
                    },
            (17, SyntaxKind.Equals) // Reduce (Expression -> Dot, [Expression -> Dot ·, Equals])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Equals]")
                        )
                    },
            (17, SyntaxKind.OpenBrace) // Reduce (Expression -> Dot, [Expression -> Dot ·, OpenBrace])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, OpenBrace]")
                        )
                    },
            (17, SyntaxKind.Slash) // Reduce (Expression -> Dot, [Expression -> Dot ·, Slash])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Slash]")
                        )
                    },
            (17, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Dot, [Expression -> Dot ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, DoubleAmpersand]")
                        )
                    },
            (18, SyntaxKind.Identifier) // Shift (57, [Expression -> Slash · Identifier, EndOfLine])
                => Shift(57),
            (19, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (19, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (19, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (19, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (19, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (19, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (20, SyntaxKind.Dot) // Shift (61, [Expression -> · Dot, CloseParenthesis])
                => Shift(61),
            (20, SyntaxKind.Identifier) // Shift (60, [Expression -> · Identifier, CloseParenthesis])
                => Shift(60),
            (20, SyntaxKind.Slash) // Shift (62, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(62),
            (20, SyntaxKind.Exclamation) // Shift (63, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(63),
            (20, SyntaxKind.OpenParenthesis) // Shift (64, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(64),
            (20, SyntaxKind.DoubleDot) // Shift (65, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(65),
            (21, SyntaxKind.OpenParenthesis) // Shift (66, [Expression -> DoubleDot · OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(66),
            (22, SyntaxKind.EndOfFile) // Reduce (CompilationUnit -> StatementList EndOfFile, [CompilationUnit -> StatementList EndOfFile ·, EndOfFile])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfFileTokenNode _0, StatementListNode _1)
                            => Reduce(
                                new CompilationUnitNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 1,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [CompilationUnit -> StatementList EndOfFile ·, EndOfFile]")
                        )
                    },
            (23, SyntaxKind.EndOfFile) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, EndOfFile])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, EndOfFile]")
                        )
                    },
            (23, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorDefine]")
                        )
                    },
            (23, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorEndIf]")
                        )
                    },
            (23, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorIf]")
                        )
                    },
            (23, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorIfDef]")
                        )
                    },
            (23, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorInclude]")
                        )
                    },
            (23, SyntaxKind.EndOfLine) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, EndOfLine]")
                        )
                    },
            (23, SyntaxKind.Semicolon) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Semicolon]")
                        )
                    },
            (23, SyntaxKind.Dot) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Dot]")
                        )
                    },
            (23, SyntaxKind.Identifier) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Identifier]")
                        )
                    },
            (23, SyntaxKind.Slash) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Slash]")
                        )
                    },
            (23, SyntaxKind.Exclamation) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Exclamation]")
                        )
                    },
            (23, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, OpenParenthesis]")
                        )
                    },
            (23, SyntaxKind.DoubleDot) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, DoubleDot]")
                        )
                    },
            (23, SyntaxKind.OpenBrace) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, OpenBrace]")
                        )
                    },
            (24, SyntaxKind.EndOfFile) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, EndOfFile])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, EndOfFile]")
                        )
                    },
            (24, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorDefine]")
                        )
                    },
            (24, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorEndIf]")
                        )
                    },
            (24, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorIf]")
                        )
                    },
            (24, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorIfDef]")
                        )
                    },
            (24, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorInclude]")
                        )
                    },
            (24, SyntaxKind.EndOfLine) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, EndOfLine]")
                        )
                    },
            (24, SyntaxKind.Semicolon) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Semicolon]")
                        )
                    },
            (24, SyntaxKind.Dot) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Dot]")
                        )
                    },
            (24, SyntaxKind.Identifier) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Identifier]")
                        )
                    },
            (24, SyntaxKind.Slash) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Slash]")
                        )
                    },
            (24, SyntaxKind.Exclamation) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Exclamation]")
                        )
                    },
            (24, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, OpenParenthesis]")
                        )
                    },
            (24, SyntaxKind.DoubleDot) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, DoubleDot]")
                        )
                    },
            (24, SyntaxKind.OpenBrace) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, OpenBrace]")
                        )
                    },
            (25, SyntaxKind.EndOfFile) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, EndOfFile])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, EndOfFile]")
                        )
                    },
            (25, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorDefine]")
                        )
                    },
            (25, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorEndIf]")
                        )
                    },
            (25, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorIf]")
                        )
                    },
            (25, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorIfDef]")
                        )
                    },
            (25, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorInclude]")
                        )
                    },
            (25, SyntaxKind.EndOfLine) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, EndOfLine]")
                        )
                    },
            (25, SyntaxKind.Semicolon) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Semicolon]")
                        )
                    },
            (25, SyntaxKind.Dot) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Dot]")
                        )
                    },
            (25, SyntaxKind.Identifier) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Identifier]")
                        )
                    },
            (25, SyntaxKind.Slash) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Slash]")
                        )
                    },
            (25, SyntaxKind.Exclamation) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Exclamation]")
                        )
                    },
            (25, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, OpenParenthesis]")
                        )
                    },
            (25, SyntaxKind.DoubleDot) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, DoubleDot]")
                        )
                    },
            (25, SyntaxKind.OpenBrace) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, OpenBrace]")
                        )
                    },
            (26, SyntaxKind.CloseBrace) // Shift (68, [Block -> OpenBrace StatementList · CloseBrace, EndOfFile])
                => Shift(68),
            (26, SyntaxKind.PreprocessorDefine) // Shift (35, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(35),
            (26, SyntaxKind.PreprocessorEndIf) // Shift (36, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, CloseBrace])
                => Shift(36),
            (26, SyntaxKind.PreprocessorIf) // Shift (37, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, CloseBrace])
                => Shift(37),
            (26, SyntaxKind.PreprocessorIfDef) // Shift (38, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, CloseBrace])
                => Shift(38),
            (26, SyntaxKind.PreprocessorInclude) // Shift (39, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, CloseBrace])
                => Shift(39),
            (26, SyntaxKind.OpenBrace) // Shift (28, [Block -> · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(28),
            (26, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (26, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (26, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (26, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (26, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (26, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (26, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (26, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (27, SyntaxKind.CloseBrace) // Reduce (StatementList -> Block, [StatementList -> Block ·, CloseBrace])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, CloseBrace]")
                        )
                    },
            (27, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorDefine]")
                        )
                    },
            (27, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorEndIf]")
                        )
                    },
            (27, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorIf]")
                        )
                    },
            (27, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorIfDef]")
                        )
                    },
            (27, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> Block, [StatementList -> Block ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, PreprocessorInclude]")
                        )
                    },
            (27, SyntaxKind.EndOfLine) // Reduce (StatementList -> Block, [StatementList -> Block ·, EndOfLine])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, EndOfLine]")
                        )
                    },
            (27, SyntaxKind.Semicolon) // Reduce (StatementList -> Block, [StatementList -> Block ·, Semicolon])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Semicolon]")
                        )
                    },
            (27, SyntaxKind.Dot) // Reduce (StatementList -> Block, [StatementList -> Block ·, Dot])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Dot]")
                        )
                    },
            (27, SyntaxKind.Identifier) // Reduce (StatementList -> Block, [StatementList -> Block ·, Identifier])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Identifier]")
                        )
                    },
            (27, SyntaxKind.Slash) // Reduce (StatementList -> Block, [StatementList -> Block ·, Slash])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Slash]")
                        )
                    },
            (27, SyntaxKind.Exclamation) // Reduce (StatementList -> Block, [StatementList -> Block ·, Exclamation])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, Exclamation]")
                        )
                    },
            (27, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> Block, [StatementList -> Block ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, OpenParenthesis]")
                        )
                    },
            (27, SyntaxKind.DoubleDot) // Reduce (StatementList -> Block, [StatementList -> Block ·, DoubleDot])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, DoubleDot]")
                        )
                    },
            (27, SyntaxKind.OpenBrace) // Reduce (StatementList -> Block, [StatementList -> Block ·, OpenBrace])
                => (PopNode()!) switch {
                        (BlockNode _0)
                            => Reduce(
                                new SingleBlockItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Block ·, OpenBrace]")
                        )
                    },
            (28, SyntaxKind.PreprocessorDefine) // Shift (35, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(35),
            (28, SyntaxKind.PreprocessorEndIf) // Shift (36, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, CloseBrace])
                => Shift(36),
            (28, SyntaxKind.PreprocessorIf) // Shift (37, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, CloseBrace])
                => Shift(37),
            (28, SyntaxKind.PreprocessorIfDef) // Shift (38, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, CloseBrace])
                => Shift(38),
            (28, SyntaxKind.PreprocessorInclude) // Shift (39, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, CloseBrace])
                => Shift(39),
            (28, SyntaxKind.OpenBrace) // Shift (28, [Block -> · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(28),
            (28, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (28, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (28, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (28, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (28, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (28, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (28, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (28, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (29, SyntaxKind.Equals) // Shift (74, [Statement -> Expression · Equals Expression StatementTerminator, CloseBrace])
                => Shift(74),
            (29, SyntaxKind.OpenBrace) // Shift (72, [Block -> Expression · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(72),
            (29, SyntaxKind.Slash) // Shift (43, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(43),
            (29, SyntaxKind.DoubleAmpersand) // Shift (44, [Expression -> Expression · DoubleAmpersand Expression, EndOfLine])
                => Shift(44),
            (29, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (29, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (30, SyntaxKind.CloseBrace) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, CloseBrace])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, CloseBrace]")
                        )
                    },
            (30, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorDefine]")
                        )
                    },
            (30, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorEndIf]")
                        )
                    },
            (30, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorIf]")
                        )
                    },
            (30, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorIfDef]")
                        )
                    },
            (30, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, PreprocessorInclude]")
                        )
                    },
            (30, SyntaxKind.EndOfLine) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, EndOfLine])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, EndOfLine]")
                        )
                    },
            (30, SyntaxKind.Semicolon) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Semicolon])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Semicolon]")
                        )
                    },
            (30, SyntaxKind.Dot) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Dot])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Dot]")
                        )
                    },
            (30, SyntaxKind.Identifier) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Identifier])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Identifier]")
                        )
                    },
            (30, SyntaxKind.Slash) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Slash])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Slash]")
                        )
                    },
            (30, SyntaxKind.Exclamation) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, Exclamation])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, Exclamation]")
                        )
                    },
            (30, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, OpenParenthesis]")
                        )
                    },
            (30, SyntaxKind.DoubleDot) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, DoubleDot])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, DoubleDot]")
                        )
                    },
            (30, SyntaxKind.OpenBrace) // Reduce (StatementList -> Statement, [StatementList -> Statement ·, OpenBrace])
                => (PopNode()!) switch {
                        (StatementNode _0)
                            => Reduce(
                                new SingleStatementItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> Statement ·, OpenBrace]")
                        )
                    },
            (31, SyntaxKind.CloseBrace) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, CloseBrace])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, CloseBrace]")
                        )
                    },
            (31, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorDefine]")
                        )
                    },
            (31, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorEndIf]")
                        )
                    },
            (31, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorIf]")
                        )
                    },
            (31, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorIfDef]")
                        )
                    },
            (31, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, PreprocessorInclude]")
                        )
                    },
            (31, SyntaxKind.EndOfLine) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, EndOfLine])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, EndOfLine]")
                        )
                    },
            (31, SyntaxKind.Semicolon) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Semicolon])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Semicolon]")
                        )
                    },
            (31, SyntaxKind.Dot) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Dot])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Dot]")
                        )
                    },
            (31, SyntaxKind.Identifier) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Identifier])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Identifier]")
                        )
                    },
            (31, SyntaxKind.Slash) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Slash])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Slash]")
                        )
                    },
            (31, SyntaxKind.Exclamation) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, Exclamation])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, Exclamation]")
                        )
                    },
            (31, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, OpenParenthesis]")
                        )
                    },
            (31, SyntaxKind.DoubleDot) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, DoubleDot])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, DoubleDot]")
                        )
                    },
            (31, SyntaxKind.OpenBrace) // Reduce (StatementList -> PreprocessorStatement, [StatementList -> PreprocessorStatement ·, OpenBrace])
                => (PopNode()!) switch {
                        (PreprocessorStatementNode _0)
                            => Reduce(
                                new SinglePreprocessorItemStatementListNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> PreprocessorStatement ·, OpenBrace]")
                        )
                    },
            (32, SyntaxKind.CloseBrace) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, CloseBrace])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, CloseBrace]")
                        )
                    },
            (32, SyntaxKind.PreprocessorDefine) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorDefine]")
                        )
                    },
            (32, SyntaxKind.PreprocessorEndIf) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorEndIf]")
                        )
                    },
            (32, SyntaxKind.PreprocessorIf) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorIf]")
                        )
                    },
            (32, SyntaxKind.PreprocessorIfDef) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorIfDef]")
                        )
                    },
            (32, SyntaxKind.PreprocessorInclude) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, PreprocessorInclude]")
                        )
                    },
            (32, SyntaxKind.EndOfLine) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, EndOfLine])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, EndOfLine]")
                        )
                    },
            (32, SyntaxKind.Semicolon) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Semicolon])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Semicolon]")
                        )
                    },
            (32, SyntaxKind.Dot) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Dot])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Dot]")
                        )
                    },
            (32, SyntaxKind.Identifier) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Identifier])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Identifier]")
                        )
                    },
            (32, SyntaxKind.Slash) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Slash])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Slash]")
                        )
                    },
            (32, SyntaxKind.Exclamation) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, Exclamation])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, Exclamation]")
                        )
                    },
            (32, SyntaxKind.OpenParenthesis) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, OpenParenthesis]")
                        )
                    },
            (32, SyntaxKind.DoubleDot) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, DoubleDot])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, DoubleDot]")
                        )
                    },
            (32, SyntaxKind.OpenBrace) // Reduce (Statement -> StatementTerminator, [Statement -> StatementTerminator ·, OpenBrace])
                => (PopNode()!) switch {
                        (StatementTerminatorNode _0)
                            => Reduce(
                                new EmptyStatementNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> StatementTerminator ·, OpenBrace]")
                        )
                    },
            (33, SyntaxKind.CloseBrace) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, CloseBrace])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, CloseBrace]")
                        )
                    },
            (33, SyntaxKind.PreprocessorDefine) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (33, SyntaxKind.PreprocessorEndIf) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (33, SyntaxKind.PreprocessorIf) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (33, SyntaxKind.PreprocessorIfDef) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (33, SyntaxKind.PreprocessorInclude) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (33, SyntaxKind.EndOfLine) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, EndOfLine])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, EndOfLine]")
                        )
                    },
            (33, SyntaxKind.Semicolon) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Semicolon])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Semicolon]")
                        )
                    },
            (33, SyntaxKind.Dot) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Dot])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Dot]")
                        )
                    },
            (33, SyntaxKind.Identifier) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Identifier])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Identifier]")
                        )
                    },
            (33, SyntaxKind.Slash) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Slash])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Slash]")
                        )
                    },
            (33, SyntaxKind.Exclamation) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, Exclamation])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, Exclamation]")
                        )
                    },
            (33, SyntaxKind.OpenParenthesis) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (33, SyntaxKind.DoubleDot) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, DoubleDot])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, DoubleDot]")
                        )
                    },
            (33, SyntaxKind.OpenBrace) // Reduce (StatementTerminator -> EndOfLine, [StatementTerminator -> EndOfLine ·, OpenBrace])
                => (PopNode()!) switch {
                        (EndOfLineTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> EndOfLine ·, OpenBrace]")
                        )
                    },
            (34, SyntaxKind.CloseBrace) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, CloseBrace])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, CloseBrace]")
                        )
                    },
            (34, SyntaxKind.PreprocessorDefine) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorDefine])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorDefine]")
                        )
                    },
            (34, SyntaxKind.PreprocessorEndIf) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorEndIf])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorEndIf]")
                        )
                    },
            (34, SyntaxKind.PreprocessorIf) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorIf])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorIf]")
                        )
                    },
            (34, SyntaxKind.PreprocessorIfDef) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorIfDef])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorIfDef]")
                        )
                    },
            (34, SyntaxKind.PreprocessorInclude) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, PreprocessorInclude])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, PreprocessorInclude]")
                        )
                    },
            (34, SyntaxKind.EndOfLine) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, EndOfLine])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, EndOfLine]")
                        )
                    },
            (34, SyntaxKind.Semicolon) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Semicolon])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Semicolon]")
                        )
                    },
            (34, SyntaxKind.Dot) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Dot])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Dot]")
                        )
                    },
            (34, SyntaxKind.Identifier) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Identifier])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Identifier]")
                        )
                    },
            (34, SyntaxKind.Slash) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Slash])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Slash]")
                        )
                    },
            (34, SyntaxKind.Exclamation) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, Exclamation])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, Exclamation]")
                        )
                    },
            (34, SyntaxKind.OpenParenthesis) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, OpenParenthesis])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, OpenParenthesis]")
                        )
                    },
            (34, SyntaxKind.DoubleDot) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, DoubleDot])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, DoubleDot]")
                        )
                    },
            (34, SyntaxKind.OpenBrace) // Reduce (StatementTerminator -> Semicolon, [StatementTerminator -> Semicolon ·, OpenBrace])
                => (PopNode()!) switch {
                        (SemicolonTokenNode _0)
                            => Reduce(
                                new StatementTerminatorNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 8, 2 => 8, 4 => 32, 5 => 41, 26 => 32, 28 => 32, 29 => 73, 40 => 32, 71 => 32,
                                    72 => 32, 80 => 32, 81 => 131, 123 => 32, 124 => 164,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementTerminator -> Semicolon ·, OpenBrace]")
                        )
                    },
            (35, SyntaxKind.Identifier) // Shift (75, [PreprocessorStatement -> PreprocessorDefine · Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(75),
            (36, SyntaxKind.EndOfLine) // Shift (76, [PreprocessorStatement -> PreprocessorEndIf · EndOfLine, CloseBrace])
                => Shift(76),
            (37, SyntaxKind.Dot) // Shift (50, [Expression -> · Dot, EndOfLine])
                => Shift(50),
            (37, SyntaxKind.Identifier) // Shift (49, [Expression -> · Identifier, EndOfLine])
                => Shift(49),
            (37, SyntaxKind.Slash) // Shift (51, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(51),
            (37, SyntaxKind.Exclamation) // Shift (52, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(52),
            (37, SyntaxKind.OpenParenthesis) // Shift (53, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(53),
            (37, SyntaxKind.DoubleDot) // Shift (54, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(54),
            (38, SyntaxKind.Identifier) // Shift (78, [PreprocessorStatement -> PreprocessorIfDef · Identifier EndOfLine, CloseBrace])
                => Shift(78),
            (39, SyntaxKind.String) // Shift (79, [PreprocessorStatement -> PreprocessorInclude · String EndOfLine, CloseBrace])
                => Shift(79),
            (40, SyntaxKind.PreprocessorDefine) // Shift (35, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(35),
            (40, SyntaxKind.PreprocessorEndIf) // Shift (36, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, CloseBrace])
                => Shift(36),
            (40, SyntaxKind.PreprocessorIf) // Shift (37, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, CloseBrace])
                => Shift(37),
            (40, SyntaxKind.PreprocessorIfDef) // Shift (38, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, CloseBrace])
                => Shift(38),
            (40, SyntaxKind.PreprocessorInclude) // Shift (39, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, CloseBrace])
                => Shift(39),
            (40, SyntaxKind.OpenBrace) // Shift (28, [Block -> · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(28),
            (40, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (40, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (40, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (40, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (40, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (40, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (40, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (40, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (41, SyntaxKind.EndOfFile) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, EndOfFile])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, EndOfFile]")
                        )
                    },
            (41, SyntaxKind.PreprocessorDefine) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorDefine]")
                        )
                    },
            (41, SyntaxKind.PreprocessorEndIf) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorEndIf]")
                        )
                    },
            (41, SyntaxKind.PreprocessorIf) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorIf]")
                        )
                    },
            (41, SyntaxKind.PreprocessorIfDef) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorIfDef]")
                        )
                    },
            (41, SyntaxKind.PreprocessorInclude) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorInclude]")
                        )
                    },
            (41, SyntaxKind.EndOfLine) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, EndOfLine]")
                        )
                    },
            (41, SyntaxKind.Semicolon) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Semicolon]")
                        )
                    },
            (41, SyntaxKind.Dot) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Dot]")
                        )
                    },
            (41, SyntaxKind.Identifier) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Identifier]")
                        )
                    },
            (41, SyntaxKind.Slash) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Slash]")
                        )
                    },
            (41, SyntaxKind.Exclamation) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Exclamation]")
                        )
                    },
            (41, SyntaxKind.OpenParenthesis) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, OpenParenthesis]")
                        )
                    },
            (41, SyntaxKind.DoubleDot) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, DoubleDot]")
                        )
                    },
            (41, SyntaxKind.OpenBrace) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, OpenBrace]")
                        )
                    },
            (42, SyntaxKind.Dot) // Shift (83, [Expression -> · Dot, EndOfLine])
                => Shift(83),
            (42, SyntaxKind.Identifier) // Shift (82, [Expression -> · Identifier, EndOfLine])
                => Shift(82),
            (42, SyntaxKind.Slash) // Shift (84, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(84),
            (42, SyntaxKind.Exclamation) // Shift (85, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(85),
            (42, SyntaxKind.OpenParenthesis) // Shift (86, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(86),
            (42, SyntaxKind.DoubleDot) // Shift (87, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(87),
            (43, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (43, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (43, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (43, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (43, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (43, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (44, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (44, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (44, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (44, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (44, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (44, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (45, SyntaxKind.EndOfLine) // Shift (90, [PreprocessorStatement -> PreprocessorDefine Identifier · EndOfLine, EndOfFile])
                => Shift(90),
            (45, SyntaxKind.Dot) // Shift (94, [PreprocessorDefinitionValue -> · Dot, EndOfLine])
                => Shift(94),
            (45, SyntaxKind.Identifier) // Shift (91, [PreprocessorDefinitionValue -> · Identifier, EndOfLine])
                => Shift(91),
            (45, SyntaxKind.String) // Shift (93, [PreprocessorDefinitionValue -> · String, EndOfLine])
                => Shift(93),
            (46, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (46, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (46, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (46, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (46, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (46, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (47, SyntaxKind.EndOfFile) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, EndOfFile])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, EndOfFile]")
                        )
                    },
            (47, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (47, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (47, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (47, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (47, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (47, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, EndOfLine]")
                        )
                    },
            (47, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Semicolon]")
                        )
                    },
            (47, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Dot]")
                        )
                    },
            (47, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Identifier]")
                        )
                    },
            (47, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Slash]")
                        )
                    },
            (47, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Exclamation]")
                        )
                    },
            (47, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (47, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, DoubleDot]")
                        )
                    },
            (47, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, OpenBrace]")
                        )
                    },
            (48, SyntaxKind.EndOfLine) // Shift (103, [PreprocessorStatement -> PreprocessorIf Expression · EndOfLine, EndOfFile])
                => Shift(103),
            (48, SyntaxKind.Slash) // Shift (104, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(104),
            (48, SyntaxKind.DoubleAmpersand) // Shift (105, [Expression -> Expression · DoubleAmpersand Expression, EndOfLine])
                => Shift(105),
            (49, SyntaxKind.EndOfLine) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, EndOfLine])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, EndOfLine]")
                        )
                    },
            (49, SyntaxKind.OpenParenthesis) // Shift (106, [Expression -> Identifier · OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(106),
            (49, SyntaxKind.Slash) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Slash])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Slash]")
                        )
                    },
            (49, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, DoubleAmpersand]")
                        )
                    },
            (50, SyntaxKind.EndOfLine) // Reduce (Expression -> Dot, [Expression -> Dot ·, EndOfLine])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, EndOfLine]")
                        )
                    },
            (50, SyntaxKind.Slash) // Reduce (Expression -> Dot, [Expression -> Dot ·, Slash])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Slash]")
                        )
                    },
            (50, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Dot, [Expression -> Dot ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, DoubleAmpersand]")
                        )
                    },
            (51, SyntaxKind.Identifier) // Shift (107, [Expression -> Slash · Identifier, EndOfLine])
                => Shift(107),
            (52, SyntaxKind.Dot) // Shift (50, [Expression -> · Dot, EndOfLine])
                => Shift(50),
            (52, SyntaxKind.Identifier) // Shift (49, [Expression -> · Identifier, EndOfLine])
                => Shift(49),
            (52, SyntaxKind.Slash) // Shift (51, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(51),
            (52, SyntaxKind.Exclamation) // Shift (52, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(52),
            (52, SyntaxKind.OpenParenthesis) // Shift (53, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(53),
            (52, SyntaxKind.DoubleDot) // Shift (54, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(54),
            (53, SyntaxKind.Dot) // Shift (61, [Expression -> · Dot, CloseParenthesis])
                => Shift(61),
            (53, SyntaxKind.Identifier) // Shift (60, [Expression -> · Identifier, CloseParenthesis])
                => Shift(60),
            (53, SyntaxKind.Slash) // Shift (62, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(62),
            (53, SyntaxKind.Exclamation) // Shift (63, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(63),
            (53, SyntaxKind.OpenParenthesis) // Shift (64, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(64),
            (53, SyntaxKind.DoubleDot) // Shift (65, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(65),
            (54, SyntaxKind.OpenParenthesis) // Shift (110, [Expression -> DoubleDot · OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(110),
            (55, SyntaxKind.EndOfLine) // Shift (111, [PreprocessorStatement -> PreprocessorIfDef Identifier · EndOfLine, EndOfFile])
                => Shift(111),
            (56, SyntaxKind.EndOfLine) // Shift (112, [PreprocessorStatement -> PreprocessorInclude String · EndOfLine, EndOfFile])
                => Shift(112),
            (57, SyntaxKind.EndOfLine) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, EndOfLine]")
                        )
                    },
            (57, SyntaxKind.Semicolon) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Semicolon]")
                        )
                    },
            (57, SyntaxKind.Equals) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Equals])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Equals]")
                        )
                    },
            (57, SyntaxKind.OpenBrace) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, OpenBrace]")
                        )
                    },
            (57, SyntaxKind.Slash) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Slash]")
                        )
                    },
            (57, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, DoubleAmpersand]")
                        )
                    },
            (58, SyntaxKind.EndOfLine) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, EndOfLine]")
                        )
                    },
            (58, SyntaxKind.Semicolon) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Semicolon]")
                        )
                    },
            (58, SyntaxKind.Equals) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Equals])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Equals]")
                        )
                    },
            (58, SyntaxKind.OpenBrace) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, OpenBrace]")
                        )
                    },
            (58, SyntaxKind.Slash) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Slash]")
                        )
                    },
            (58, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, DoubleAmpersand]")
                        )
                    },
            (59, SyntaxKind.CloseParenthesis) // Shift (115, [Expression -> OpenParenthesis Expression · CloseParenthesis, EndOfLine])
                => Shift(115),
            (59, SyntaxKind.Slash) // Shift (113, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(113),
            (59, SyntaxKind.DoubleAmpersand) // Shift (114, [Expression -> Expression · DoubleAmpersand Expression, CloseParenthesis])
                => Shift(114),
            (60, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, CloseParenthesis])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, CloseParenthesis]")
                        )
                    },
            (60, SyntaxKind.OpenParenthesis) // Shift (116, [Expression -> Identifier · OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(116),
            (60, SyntaxKind.Slash) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Slash])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Slash]")
                        )
                    },
            (60, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, DoubleAmpersand]")
                        )
                    },
            (61, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Dot, [Expression -> Dot ·, CloseParenthesis])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, CloseParenthesis]")
                        )
                    },
            (61, SyntaxKind.Slash) // Reduce (Expression -> Dot, [Expression -> Dot ·, Slash])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Slash]")
                        )
                    },
            (61, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Dot, [Expression -> Dot ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, DoubleAmpersand]")
                        )
                    },
            (62, SyntaxKind.Identifier) // Shift (117, [Expression -> Slash · Identifier, CloseParenthesis])
                => Shift(117),
            (63, SyntaxKind.Dot) // Shift (61, [Expression -> · Dot, CloseParenthesis])
                => Shift(61),
            (63, SyntaxKind.Identifier) // Shift (60, [Expression -> · Identifier, CloseParenthesis])
                => Shift(60),
            (63, SyntaxKind.Slash) // Shift (62, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(62),
            (63, SyntaxKind.Exclamation) // Shift (63, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(63),
            (63, SyntaxKind.OpenParenthesis) // Shift (64, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(64),
            (63, SyntaxKind.DoubleDot) // Shift (65, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(65),
            (64, SyntaxKind.Dot) // Shift (61, [Expression -> · Dot, CloseParenthesis])
                => Shift(61),
            (64, SyntaxKind.Identifier) // Shift (60, [Expression -> · Identifier, CloseParenthesis])
                => Shift(60),
            (64, SyntaxKind.Slash) // Shift (62, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(62),
            (64, SyntaxKind.Exclamation) // Shift (63, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(63),
            (64, SyntaxKind.OpenParenthesis) // Shift (64, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(64),
            (64, SyntaxKind.DoubleDot) // Shift (65, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(65),
            (65, SyntaxKind.OpenParenthesis) // Shift (120, [Expression -> DoubleDot · OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(120),
            (66, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (66, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (66, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (66, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (66, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (66, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (67, SyntaxKind.CloseBrace) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, CloseBrace])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, CloseBrace]")
                        )
                    },
            (67, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorDefine]")
                        )
                    },
            (67, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorEndIf]")
                        )
                    },
            (67, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorIf]")
                        )
                    },
            (67, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorIfDef]")
                        )
                    },
            (67, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, PreprocessorInclude]")
                        )
                    },
            (67, SyntaxKind.EndOfLine) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, EndOfLine]")
                        )
                    },
            (67, SyntaxKind.Semicolon) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Semicolon]")
                        )
                    },
            (67, SyntaxKind.Dot) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Dot]")
                        )
                    },
            (67, SyntaxKind.Identifier) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Identifier]")
                        )
                    },
            (67, SyntaxKind.Slash) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Slash]")
                        )
                    },
            (67, SyntaxKind.Exclamation) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, Exclamation]")
                        )
                    },
            (67, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, OpenParenthesis]")
                        )
                    },
            (67, SyntaxKind.DoubleDot) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, DoubleDot]")
                        )
                    },
            (67, SyntaxKind.OpenBrace) // Reduce (StatementList -> StatementList Block, [StatementList -> StatementList Block ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (BlockNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleBlockItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Block ·, OpenBrace]")
                        )
                    },
            (68, SyntaxKind.EndOfFile) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, EndOfFile])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, EndOfFile]")
                        )
                    },
            (68, SyntaxKind.PreprocessorDefine) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorDefine]")
                        )
                    },
            (68, SyntaxKind.PreprocessorEndIf) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorEndIf]")
                        )
                    },
            (68, SyntaxKind.PreprocessorIf) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorIf]")
                        )
                    },
            (68, SyntaxKind.PreprocessorIfDef) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorIfDef]")
                        )
                    },
            (68, SyntaxKind.PreprocessorInclude) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorInclude]")
                        )
                    },
            (68, SyntaxKind.EndOfLine) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, EndOfLine]")
                        )
                    },
            (68, SyntaxKind.Semicolon) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Semicolon]")
                        )
                    },
            (68, SyntaxKind.Dot) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Dot]")
                        )
                    },
            (68, SyntaxKind.Identifier) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Identifier]")
                        )
                    },
            (68, SyntaxKind.Slash) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Slash]")
                        )
                    },
            (68, SyntaxKind.Exclamation) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Exclamation]")
                        )
                    },
            (68, SyntaxKind.OpenParenthesis) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, OpenParenthesis]")
                        )
                    },
            (68, SyntaxKind.DoubleDot) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, DoubleDot]")
                        )
                    },
            (68, SyntaxKind.OpenBrace) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, OpenBrace]")
                        )
                    },
            (69, SyntaxKind.CloseBrace) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, CloseBrace])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, CloseBrace]")
                        )
                    },
            (69, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorDefine]")
                        )
                    },
            (69, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorEndIf]")
                        )
                    },
            (69, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorIf]")
                        )
                    },
            (69, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorIfDef]")
                        )
                    },
            (69, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, PreprocessorInclude]")
                        )
                    },
            (69, SyntaxKind.EndOfLine) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, EndOfLine]")
                        )
                    },
            (69, SyntaxKind.Semicolon) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Semicolon]")
                        )
                    },
            (69, SyntaxKind.Dot) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Dot]")
                        )
                    },
            (69, SyntaxKind.Identifier) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Identifier]")
                        )
                    },
            (69, SyntaxKind.Slash) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Slash]")
                        )
                    },
            (69, SyntaxKind.Exclamation) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, Exclamation]")
                        )
                    },
            (69, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, OpenParenthesis]")
                        )
                    },
            (69, SyntaxKind.DoubleDot) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, DoubleDot]")
                        )
                    },
            (69, SyntaxKind.OpenBrace) // Reduce (StatementList -> StatementList Statement, [StatementList -> StatementList Statement ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (StatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultipleStatementItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList Statement ·, OpenBrace]")
                        )
                    },
            (70, SyntaxKind.CloseBrace) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, CloseBrace])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, CloseBrace]")
                        )
                    },
            (70, SyntaxKind.PreprocessorDefine) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorDefine]")
                        )
                    },
            (70, SyntaxKind.PreprocessorEndIf) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorEndIf]")
                        )
                    },
            (70, SyntaxKind.PreprocessorIf) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorIf]")
                        )
                    },
            (70, SyntaxKind.PreprocessorIfDef) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorIfDef]")
                        )
                    },
            (70, SyntaxKind.PreprocessorInclude) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, PreprocessorInclude]")
                        )
                    },
            (70, SyntaxKind.EndOfLine) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, EndOfLine]")
                        )
                    },
            (70, SyntaxKind.Semicolon) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Semicolon]")
                        )
                    },
            (70, SyntaxKind.Dot) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Dot]")
                        )
                    },
            (70, SyntaxKind.Identifier) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Identifier]")
                        )
                    },
            (70, SyntaxKind.Slash) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Slash]")
                        )
                    },
            (70, SyntaxKind.Exclamation) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, Exclamation]")
                        )
                    },
            (70, SyntaxKind.OpenParenthesis) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, OpenParenthesis]")
                        )
                    },
            (70, SyntaxKind.DoubleDot) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, DoubleDot]")
                        )
                    },
            (70, SyntaxKind.OpenBrace) // Reduce (StatementList -> StatementList PreprocessorStatement, [StatementList -> StatementList PreprocessorStatement ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (PreprocessorStatementNode _0, StatementListNode _1)
                            => Reduce(
                                new MultiplePreprocessorItemStatementListNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 2, 4 => 26, 28 => 71, 40 => 80, 72 => 123,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [StatementList -> StatementList PreprocessorStatement ·, OpenBrace]")
                        )
                    },
            (71, SyntaxKind.CloseBrace) // Shift (122, [Block -> OpenBrace StatementList · CloseBrace, CloseBrace])
                => Shift(122),
            (71, SyntaxKind.PreprocessorDefine) // Shift (35, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(35),
            (71, SyntaxKind.PreprocessorEndIf) // Shift (36, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, CloseBrace])
                => Shift(36),
            (71, SyntaxKind.PreprocessorIf) // Shift (37, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, CloseBrace])
                => Shift(37),
            (71, SyntaxKind.PreprocessorIfDef) // Shift (38, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, CloseBrace])
                => Shift(38),
            (71, SyntaxKind.PreprocessorInclude) // Shift (39, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, CloseBrace])
                => Shift(39),
            (71, SyntaxKind.OpenBrace) // Shift (28, [Block -> · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(28),
            (71, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (71, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (71, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (71, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (71, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (71, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (71, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (71, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (72, SyntaxKind.PreprocessorDefine) // Shift (35, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(35),
            (72, SyntaxKind.PreprocessorEndIf) // Shift (36, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, CloseBrace])
                => Shift(36),
            (72, SyntaxKind.PreprocessorIf) // Shift (37, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, CloseBrace])
                => Shift(37),
            (72, SyntaxKind.PreprocessorIfDef) // Shift (38, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, CloseBrace])
                => Shift(38),
            (72, SyntaxKind.PreprocessorInclude) // Shift (39, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, CloseBrace])
                => Shift(39),
            (72, SyntaxKind.OpenBrace) // Shift (28, [Block -> · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(28),
            (72, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (72, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (72, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (72, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (72, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (72, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (72, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (72, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (73, SyntaxKind.CloseBrace) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, CloseBrace])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, CloseBrace]")
                        )
                    },
            (73, SyntaxKind.PreprocessorDefine) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorDefine]")
                        )
                    },
            (73, SyntaxKind.PreprocessorEndIf) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorEndIf]")
                        )
                    },
            (73, SyntaxKind.PreprocessorIf) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorIf]")
                        )
                    },
            (73, SyntaxKind.PreprocessorIfDef) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorIfDef]")
                        )
                    },
            (73, SyntaxKind.PreprocessorInclude) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, PreprocessorInclude]")
                        )
                    },
            (73, SyntaxKind.EndOfLine) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, EndOfLine]")
                        )
                    },
            (73, SyntaxKind.Semicolon) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Semicolon]")
                        )
                    },
            (73, SyntaxKind.Dot) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Dot]")
                        )
                    },
            (73, SyntaxKind.Identifier) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Identifier]")
                        )
                    },
            (73, SyntaxKind.Slash) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Slash]")
                        )
                    },
            (73, SyntaxKind.Exclamation) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, Exclamation]")
                        )
                    },
            (73, SyntaxKind.OpenParenthesis) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, OpenParenthesis]")
                        )
                    },
            (73, SyntaxKind.DoubleDot) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, DoubleDot]")
                        )
                    },
            (73, SyntaxKind.OpenBrace) // Reduce (Statement -> Expression StatementTerminator, [Statement -> Expression StatementTerminator ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1)
                            => Reduce(
                                new ExpressionStatementNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression StatementTerminator ·, OpenBrace]")
                        )
                    },
            (74, SyntaxKind.Dot) // Shift (83, [Expression -> · Dot, EndOfLine])
                => Shift(83),
            (74, SyntaxKind.Identifier) // Shift (82, [Expression -> · Identifier, EndOfLine])
                => Shift(82),
            (74, SyntaxKind.Slash) // Shift (84, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(84),
            (74, SyntaxKind.Exclamation) // Shift (85, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(85),
            (74, SyntaxKind.OpenParenthesis) // Shift (86, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(86),
            (74, SyntaxKind.DoubleDot) // Shift (87, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(87),
            (75, SyntaxKind.EndOfLine) // Shift (125, [PreprocessorStatement -> PreprocessorDefine Identifier · EndOfLine, CloseBrace])
                => Shift(125),
            (75, SyntaxKind.Dot) // Shift (94, [PreprocessorDefinitionValue -> · Dot, EndOfLine])
                => Shift(94),
            (75, SyntaxKind.Identifier) // Shift (91, [PreprocessorDefinitionValue -> · Identifier, EndOfLine])
                => Shift(91),
            (75, SyntaxKind.String) // Shift (93, [PreprocessorDefinitionValue -> · String, EndOfLine])
                => Shift(93),
            (76, SyntaxKind.CloseBrace) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, CloseBrace])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, CloseBrace]")
                        )
                    },
            (76, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (76, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (76, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (76, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (76, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (76, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, EndOfLine]")
                        )
                    },
            (76, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Semicolon]")
                        )
                    },
            (76, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Dot]")
                        )
                    },
            (76, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Identifier]")
                        )
                    },
            (76, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Slash]")
                        )
                    },
            (76, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, Exclamation]")
                        )
                    },
            (76, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (76, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, DoubleDot]")
                        )
                    },
            (76, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorEndIf EndOfLine, [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorEndIfTokenNode _1)
                            => Reduce(
                                new PreprocessorEndIfNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorEndIf EndOfLine ·, OpenBrace]")
                        )
                    },
            (77, SyntaxKind.EndOfLine) // Shift (127, [PreprocessorStatement -> PreprocessorIf Expression · EndOfLine, CloseBrace])
                => Shift(127),
            (77, SyntaxKind.Slash) // Shift (104, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(104),
            (77, SyntaxKind.DoubleAmpersand) // Shift (105, [Expression -> Expression · DoubleAmpersand Expression, EndOfLine])
                => Shift(105),
            (78, SyntaxKind.EndOfLine) // Shift (128, [PreprocessorStatement -> PreprocessorIfDef Identifier · EndOfLine, CloseBrace])
                => Shift(128),
            (79, SyntaxKind.EndOfLine) // Shift (129, [PreprocessorStatement -> PreprocessorInclude String · EndOfLine, CloseBrace])
                => Shift(129),
            (80, SyntaxKind.CloseBrace) // Shift (130, [Block -> Expression OpenBrace StatementList · CloseBrace, EndOfFile])
                => Shift(130),
            (80, SyntaxKind.PreprocessorDefine) // Shift (35, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(35),
            (80, SyntaxKind.PreprocessorEndIf) // Shift (36, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, CloseBrace])
                => Shift(36),
            (80, SyntaxKind.PreprocessorIf) // Shift (37, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, CloseBrace])
                => Shift(37),
            (80, SyntaxKind.PreprocessorIfDef) // Shift (38, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, CloseBrace])
                => Shift(38),
            (80, SyntaxKind.PreprocessorInclude) // Shift (39, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, CloseBrace])
                => Shift(39),
            (80, SyntaxKind.OpenBrace) // Shift (28, [Block -> · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(28),
            (80, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (80, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (80, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (80, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (80, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (80, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (80, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (80, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (81, SyntaxKind.Slash) // Shift (132, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(132),
            (81, SyntaxKind.DoubleAmpersand) // Shift (133, [Expression -> Expression · DoubleAmpersand Expression, EndOfLine])
                => Shift(133),
            (81, SyntaxKind.EndOfLine) // Shift (9, [StatementTerminator -> · EndOfLine, EndOfFile])
                => Shift(9),
            (81, SyntaxKind.Semicolon) // Shift (10, [StatementTerminator -> · Semicolon, EndOfFile])
                => Shift(10),
            (82, SyntaxKind.EndOfLine) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, EndOfLine])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, EndOfLine]")
                        )
                    },
            (82, SyntaxKind.Semicolon) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Semicolon])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Semicolon]")
                        )
                    },
            (82, SyntaxKind.OpenParenthesis) // Shift (134, [Expression -> Identifier · OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(134),
            (82, SyntaxKind.Slash) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Slash])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Slash]")
                        )
                    },
            (82, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, DoubleAmpersand]")
                        )
                    },
            (83, SyntaxKind.EndOfLine) // Reduce (Expression -> Dot, [Expression -> Dot ·, EndOfLine])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, EndOfLine]")
                        )
                    },
            (83, SyntaxKind.Semicolon) // Reduce (Expression -> Dot, [Expression -> Dot ·, Semicolon])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Semicolon]")
                        )
                    },
            (83, SyntaxKind.Slash) // Reduce (Expression -> Dot, [Expression -> Dot ·, Slash])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Slash]")
                        )
                    },
            (83, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Dot, [Expression -> Dot ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, DoubleAmpersand]")
                        )
                    },
            (84, SyntaxKind.Identifier) // Shift (135, [Expression -> Slash · Identifier, EndOfLine])
                => Shift(135),
            (85, SyntaxKind.Dot) // Shift (83, [Expression -> · Dot, EndOfLine])
                => Shift(83),
            (85, SyntaxKind.Identifier) // Shift (82, [Expression -> · Identifier, EndOfLine])
                => Shift(82),
            (85, SyntaxKind.Slash) // Shift (84, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(84),
            (85, SyntaxKind.Exclamation) // Shift (85, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(85),
            (85, SyntaxKind.OpenParenthesis) // Shift (86, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(86),
            (85, SyntaxKind.DoubleDot) // Shift (87, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(87),
            (86, SyntaxKind.Dot) // Shift (61, [Expression -> · Dot, CloseParenthesis])
                => Shift(61),
            (86, SyntaxKind.Identifier) // Shift (60, [Expression -> · Identifier, CloseParenthesis])
                => Shift(60),
            (86, SyntaxKind.Slash) // Shift (62, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(62),
            (86, SyntaxKind.Exclamation) // Shift (63, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(63),
            (86, SyntaxKind.OpenParenthesis) // Shift (64, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(64),
            (86, SyntaxKind.DoubleDot) // Shift (65, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(65),
            (87, SyntaxKind.OpenParenthesis) // Shift (138, [Expression -> DoubleDot · OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(138),
            (88, SyntaxKind.EndOfLine) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, EndOfLine]")
                        )
                    },
            (88, SyntaxKind.Semicolon) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Semicolon]")
                        )
                    },
            (88, SyntaxKind.Equals) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Equals])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Equals]")
                        )
                    },
            (88, SyntaxKind.OpenBrace) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, OpenBrace]")
                        )
                    },
            (88, SyntaxKind.Slash) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Slash]")
                        )
                    },
            (88, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, DoubleAmpersand]")
                        )
                    },
            (89, SyntaxKind.EndOfLine) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, EndOfLine]")
                        )
                    },
            (89, SyntaxKind.Semicolon) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, Semicolon]")
                        )
                    },
            (89, SyntaxKind.Equals) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, Equals])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, Equals]")
                        )
                    },
            (89, SyntaxKind.OpenBrace) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, OpenBrace]")
                        )
                    },
            (89, SyntaxKind.Slash) // Shift (43, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(43),
            (89, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand]")
                        )
                    },
            (90, SyntaxKind.EndOfFile) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, EndOfFile])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, EndOfFile]")
                        )
                    },
            (90, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (90, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (90, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (90, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (90, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (90, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, EndOfLine]")
                        )
                    },
            (90, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Semicolon]")
                        )
                    },
            (90, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Dot]")
                        )
                    },
            (90, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Identifier]")
                        )
                    },
            (90, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Slash]")
                        )
                    },
            (90, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Exclamation]")
                        )
                    },
            (90, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (90, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, DoubleDot]")
                        )
                    },
            (90, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, OpenBrace]")
                        )
                    },
            (91, SyntaxKind.EndOfLine) // Reduce (PreprocessorDefinitionValue -> Identifier, [PreprocessorDefinitionValue -> Identifier ·, EndOfLine])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> Identifier ·, EndOfLine]")
                        )
                    },
            (91, SyntaxKind.Dot) // Reduce (PreprocessorDefinitionValue -> Identifier, [PreprocessorDefinitionValue -> Identifier ·, Dot])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> Identifier ·, Dot]")
                        )
                    },
            (91, SyntaxKind.Identifier) // Reduce (PreprocessorDefinitionValue -> Identifier, [PreprocessorDefinitionValue -> Identifier ·, Identifier])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> Identifier ·, Identifier]")
                        )
                    },
            (91, SyntaxKind.String) // Reduce (PreprocessorDefinitionValue -> Identifier, [PreprocessorDefinitionValue -> Identifier ·, String])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> Identifier ·, String]")
                        )
                    },
            (92, SyntaxKind.EndOfLine) // Shift (139, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue · EndOfLine, EndOfFile])
                => Shift(139),
            (92, SyntaxKind.Dot) // Shift (142, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue · Dot, EndOfLine])
                => Shift(142),
            (92, SyntaxKind.Identifier) // Shift (140, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue · Identifier, EndOfLine])
                => Shift(140),
            (92, SyntaxKind.String) // Shift (141, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue · String, EndOfLine])
                => Shift(141),
            (93, SyntaxKind.EndOfLine) // Reduce (PreprocessorDefinitionValue -> String, [PreprocessorDefinitionValue -> String ·, EndOfLine])
                => (PopNode()!) switch {
                        (StringTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> String ·, EndOfLine]")
                        )
                    },
            (93, SyntaxKind.Dot) // Reduce (PreprocessorDefinitionValue -> String, [PreprocessorDefinitionValue -> String ·, Dot])
                => (PopNode()!) switch {
                        (StringTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> String ·, Dot]")
                        )
                    },
            (93, SyntaxKind.Identifier) // Reduce (PreprocessorDefinitionValue -> String, [PreprocessorDefinitionValue -> String ·, Identifier])
                => (PopNode()!) switch {
                        (StringTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> String ·, Identifier]")
                        )
                    },
            (93, SyntaxKind.String) // Reduce (PreprocessorDefinitionValue -> String, [PreprocessorDefinitionValue -> String ·, String])
                => (PopNode()!) switch {
                        (StringTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> String ·, String]")
                        )
                    },
            (94, SyntaxKind.EndOfLine) // Reduce (PreprocessorDefinitionValue -> Dot, [PreprocessorDefinitionValue -> Dot ·, EndOfLine])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> Dot ·, EndOfLine]")
                        )
                    },
            (94, SyntaxKind.Dot) // Reduce (PreprocessorDefinitionValue -> Dot, [PreprocessorDefinitionValue -> Dot ·, Dot])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> Dot ·, Dot]")
                        )
                    },
            (94, SyntaxKind.Identifier) // Reduce (PreprocessorDefinitionValue -> Dot, [PreprocessorDefinitionValue -> Dot ·, Identifier])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> Dot ·, Identifier]")
                        )
                    },
            (94, SyntaxKind.String) // Reduce (PreprocessorDefinitionValue -> Dot, [PreprocessorDefinitionValue -> Dot ·, String])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new SingleItemPreprocessorDefinitionValueNode(
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> Dot ·, String]")
                        )
                    },
            (95, SyntaxKind.CloseParenthesis) // Reduce (ExpressionList -> Expression, [ExpressionList -> Expression ·, CloseParenthesis])
                => (PopNode()!) switch {
                        (ExpressionNode _0)
                            => Reduce(
                                new SingleItemExpressionListNode(
                                    _0),
                                state => state switch
                                {
                                    46 => 98, 66 => 121, 106 => 154, 110 => 156, 116 => 159, 120 => 161, 134 => 168, 138 => 170, 145 => 173,
                                    151 => 176,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [ExpressionList -> Expression ·, CloseParenthesis]")
                        )
                    },
            (95, SyntaxKind.Slash) // Shift (143, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(143),
            (95, SyntaxKind.DoubleAmpersand) // Shift (144, [Expression -> Expression · DoubleAmpersand Expression, CloseParenthesis])
                => Shift(144),
            (95, SyntaxKind.Comma) // Reduce (ExpressionList -> Expression, [ExpressionList -> Expression ·, Comma])
                => (PopNode()!) switch {
                        (ExpressionNode _0)
                            => Reduce(
                                new SingleItemExpressionListNode(
                                    _0),
                                state => state switch
                                {
                                    46 => 98, 66 => 121, 106 => 154, 110 => 156, 116 => 159, 120 => 161, 134 => 168, 138 => 170, 145 => 173,
                                    151 => 176,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [ExpressionList -> Expression ·, Comma]")
                        )
                    },
            (96, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, CloseParenthesis])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, CloseParenthesis]")
                        )
                    },
            (96, SyntaxKind.OpenParenthesis) // Shift (145, [Expression -> Identifier · OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(145),
            (96, SyntaxKind.Slash) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Slash])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Slash]")
                        )
                    },
            (96, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, DoubleAmpersand]")
                        )
                    },
            (96, SyntaxKind.Comma) // Reduce (Expression -> Identifier, [Expression -> Identifier ·, Comma])
                => (PopNode()!) switch {
                        (IdentifierTokenNode _0)
                            => Reduce(
                                new IdentifierExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier ·, Comma]")
                        )
                    },
            (97, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Dot, [Expression -> Dot ·, CloseParenthesis])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, CloseParenthesis]")
                        )
                    },
            (97, SyntaxKind.Slash) // Reduce (Expression -> Dot, [Expression -> Dot ·, Slash])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Slash]")
                        )
                    },
            (97, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Dot, [Expression -> Dot ·, DoubleAmpersand])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, DoubleAmpersand]")
                        )
                    },
            (97, SyntaxKind.Comma) // Reduce (Expression -> Dot, [Expression -> Dot ·, Comma])
                => (PopNode()!) switch {
                        (DotTokenNode _0)
                            => Reduce(
                                new DotExpressionNode(
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Dot ·, Comma]")
                        )
                    },
            (98, SyntaxKind.CloseParenthesis) // Shift (147, [Expression -> Identifier OpenParenthesis ExpressionList · CloseParenthesis, EndOfLine])
                => Shift(147),
            (98, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (99, SyntaxKind.Identifier) // Shift (148, [Expression -> Slash · Identifier, CloseParenthesis])
                => Shift(148),
            (100, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (100, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (100, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (100, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (100, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (100, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (101, SyntaxKind.Dot) // Shift (61, [Expression -> · Dot, CloseParenthesis])
                => Shift(61),
            (101, SyntaxKind.Identifier) // Shift (60, [Expression -> · Identifier, CloseParenthesis])
                => Shift(60),
            (101, SyntaxKind.Slash) // Shift (62, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(62),
            (101, SyntaxKind.Exclamation) // Shift (63, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(63),
            (101, SyntaxKind.OpenParenthesis) // Shift (64, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(64),
            (101, SyntaxKind.DoubleDot) // Shift (65, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(65),
            (102, SyntaxKind.OpenParenthesis) // Shift (151, [Expression -> DoubleDot · OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(151),
            (103, SyntaxKind.EndOfFile) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, EndOfFile])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, EndOfFile]")
                        )
                    },
            (103, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (103, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (103, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (103, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (103, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (103, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, EndOfLine]")
                        )
                    },
            (103, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Semicolon]")
                        )
                    },
            (103, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Dot]")
                        )
                    },
            (103, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Identifier]")
                        )
                    },
            (103, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Slash]")
                        )
                    },
            (103, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Exclamation]")
                        )
                    },
            (103, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (103, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, DoubleDot]")
                        )
                    },
            (103, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, OpenBrace]")
                        )
                    },
            (104, SyntaxKind.Dot) // Shift (50, [Expression -> · Dot, EndOfLine])
                => Shift(50),
            (104, SyntaxKind.Identifier) // Shift (49, [Expression -> · Identifier, EndOfLine])
                => Shift(49),
            (104, SyntaxKind.Slash) // Shift (51, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(51),
            (104, SyntaxKind.Exclamation) // Shift (52, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(52),
            (104, SyntaxKind.OpenParenthesis) // Shift (53, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(53),
            (104, SyntaxKind.DoubleDot) // Shift (54, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(54),
            (105, SyntaxKind.Dot) // Shift (50, [Expression -> · Dot, EndOfLine])
                => Shift(50),
            (105, SyntaxKind.Identifier) // Shift (49, [Expression -> · Identifier, EndOfLine])
                => Shift(49),
            (105, SyntaxKind.Slash) // Shift (51, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(51),
            (105, SyntaxKind.Exclamation) // Shift (52, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(52),
            (105, SyntaxKind.OpenParenthesis) // Shift (53, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(53),
            (105, SyntaxKind.DoubleDot) // Shift (54, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(54),
            (106, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (106, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (106, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (106, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (106, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (106, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (107, SyntaxKind.EndOfLine) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, EndOfLine]")
                        )
                    },
            (107, SyntaxKind.Slash) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Slash]")
                        )
                    },
            (107, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, DoubleAmpersand]")
                        )
                    },
            (108, SyntaxKind.EndOfLine) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, EndOfLine]")
                        )
                    },
            (108, SyntaxKind.Slash) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Slash]")
                        )
                    },
            (108, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, DoubleAmpersand]")
                        )
                    },
            (109, SyntaxKind.CloseParenthesis) // Shift (155, [Expression -> OpenParenthesis Expression · CloseParenthesis, EndOfLine])
                => Shift(155),
            (109, SyntaxKind.Slash) // Shift (113, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(113),
            (109, SyntaxKind.DoubleAmpersand) // Shift (114, [Expression -> Expression · DoubleAmpersand Expression, CloseParenthesis])
                => Shift(114),
            (110, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (110, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (110, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (110, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (110, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (110, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (111, SyntaxKind.EndOfFile) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, EndOfFile])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, EndOfFile]")
                        )
                    },
            (111, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (111, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (111, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (111, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (111, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (111, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, EndOfLine]")
                        )
                    },
            (111, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Semicolon]")
                        )
                    },
            (111, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Dot]")
                        )
                    },
            (111, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Identifier]")
                        )
                    },
            (111, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Slash]")
                        )
                    },
            (111, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Exclamation]")
                        )
                    },
            (111, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (111, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, DoubleDot]")
                        )
                    },
            (111, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, OpenBrace]")
                        )
                    },
            (112, SyntaxKind.EndOfFile) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, EndOfFile])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, EndOfFile]")
                        )
                    },
            (112, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (112, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (112, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (112, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (112, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (112, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, EndOfLine]")
                        )
                    },
            (112, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Semicolon]")
                        )
                    },
            (112, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Dot]")
                        )
                    },
            (112, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Identifier]")
                        )
                    },
            (112, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Slash]")
                        )
                    },
            (112, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Exclamation]")
                        )
                    },
            (112, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (112, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, DoubleDot]")
                        )
                    },
            (112, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, OpenBrace]")
                        )
                    },
            (113, SyntaxKind.Dot) // Shift (61, [Expression -> · Dot, CloseParenthesis])
                => Shift(61),
            (113, SyntaxKind.Identifier) // Shift (60, [Expression -> · Identifier, CloseParenthesis])
                => Shift(60),
            (113, SyntaxKind.Slash) // Shift (62, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(62),
            (113, SyntaxKind.Exclamation) // Shift (63, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(63),
            (113, SyntaxKind.OpenParenthesis) // Shift (64, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(64),
            (113, SyntaxKind.DoubleDot) // Shift (65, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(65),
            (114, SyntaxKind.Dot) // Shift (61, [Expression -> · Dot, CloseParenthesis])
                => Shift(61),
            (114, SyntaxKind.Identifier) // Shift (60, [Expression -> · Identifier, CloseParenthesis])
                => Shift(60),
            (114, SyntaxKind.Slash) // Shift (62, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(62),
            (114, SyntaxKind.Exclamation) // Shift (63, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(63),
            (114, SyntaxKind.OpenParenthesis) // Shift (64, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(64),
            (114, SyntaxKind.DoubleDot) // Shift (65, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(65),
            (115, SyntaxKind.EndOfLine) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (115, SyntaxKind.Semicolon) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Semicolon]")
                        )
                    },
            (115, SyntaxKind.Equals) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Equals])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Equals]")
                        )
                    },
            (115, SyntaxKind.OpenBrace) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, OpenBrace]")
                        )
                    },
            (115, SyntaxKind.Slash) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash]")
                        )
                    },
            (115, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (116, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (116, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (116, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (116, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (116, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (116, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (117, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, CloseParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, CloseParenthesis]")
                        )
                    },
            (117, SyntaxKind.Slash) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Slash]")
                        )
                    },
            (117, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, DoubleAmpersand]")
                        )
                    },
            (118, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, CloseParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, CloseParenthesis]")
                        )
                    },
            (118, SyntaxKind.Slash) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Slash]")
                        )
                    },
            (118, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, DoubleAmpersand]")
                        )
                    },
            (119, SyntaxKind.CloseParenthesis) // Shift (160, [Expression -> OpenParenthesis Expression · CloseParenthesis, CloseParenthesis])
                => Shift(160),
            (119, SyntaxKind.Slash) // Shift (113, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(113),
            (119, SyntaxKind.DoubleAmpersand) // Shift (114, [Expression -> Expression · DoubleAmpersand Expression, CloseParenthesis])
                => Shift(114),
            (120, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (120, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (120, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (120, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (120, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (120, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (121, SyntaxKind.CloseParenthesis) // Shift (162, [Expression -> DoubleDot OpenParenthesis ExpressionList · CloseParenthesis, EndOfLine])
                => Shift(162),
            (121, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (122, SyntaxKind.CloseBrace) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, CloseBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, CloseBrace]")
                        )
                    },
            (122, SyntaxKind.PreprocessorDefine) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorDefine]")
                        )
                    },
            (122, SyntaxKind.PreprocessorEndIf) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorEndIf]")
                        )
                    },
            (122, SyntaxKind.PreprocessorIf) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorIf]")
                        )
                    },
            (122, SyntaxKind.PreprocessorIfDef) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorIfDef]")
                        )
                    },
            (122, SyntaxKind.PreprocessorInclude) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, PreprocessorInclude]")
                        )
                    },
            (122, SyntaxKind.EndOfLine) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, EndOfLine]")
                        )
                    },
            (122, SyntaxKind.Semicolon) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Semicolon]")
                        )
                    },
            (122, SyntaxKind.Dot) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Dot]")
                        )
                    },
            (122, SyntaxKind.Identifier) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Identifier]")
                        )
                    },
            (122, SyntaxKind.Slash) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Slash]")
                        )
                    },
            (122, SyntaxKind.Exclamation) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, Exclamation]")
                        )
                    },
            (122, SyntaxKind.OpenParenthesis) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, OpenParenthesis]")
                        )
                    },
            (122, SyntaxKind.DoubleDot) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, DoubleDot]")
                        )
                    },
            (122, SyntaxKind.OpenBrace) // Reduce (Block -> OpenBrace StatementList CloseBrace, [Block -> OpenBrace StatementList CloseBrace ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2)
                            => Reduce(
                                new NestedBlockNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> OpenBrace StatementList CloseBrace ·, OpenBrace]")
                        )
                    },
            (123, SyntaxKind.CloseBrace) // Shift (163, [Block -> Expression OpenBrace StatementList · CloseBrace, CloseBrace])
                => Shift(163),
            (123, SyntaxKind.PreprocessorDefine) // Shift (35, [PreprocessorStatement -> · PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, CloseBrace])
                => Shift(35),
            (123, SyntaxKind.PreprocessorEndIf) // Shift (36, [PreprocessorStatement -> · PreprocessorEndIf EndOfLine, CloseBrace])
                => Shift(36),
            (123, SyntaxKind.PreprocessorIf) // Shift (37, [PreprocessorStatement -> · PreprocessorIf Expression EndOfLine, CloseBrace])
                => Shift(37),
            (123, SyntaxKind.PreprocessorIfDef) // Shift (38, [PreprocessorStatement -> · PreprocessorIfDef Identifier EndOfLine, CloseBrace])
                => Shift(38),
            (123, SyntaxKind.PreprocessorInclude) // Shift (39, [PreprocessorStatement -> · PreprocessorInclude String EndOfLine, CloseBrace])
                => Shift(39),
            (123, SyntaxKind.OpenBrace) // Shift (28, [Block -> · OpenBrace StatementList CloseBrace, CloseBrace])
                => Shift(28),
            (123, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (123, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (123, SyntaxKind.Dot) // Shift (17, [Expression -> · Dot, EndOfLine])
                => Shift(17),
            (123, SyntaxKind.Identifier) // Shift (12, [Expression -> · Identifier, EndOfLine])
                => Shift(12),
            (123, SyntaxKind.Slash) // Shift (18, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(18),
            (123, SyntaxKind.Exclamation) // Shift (19, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(19),
            (123, SyntaxKind.OpenParenthesis) // Shift (20, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(20),
            (123, SyntaxKind.DoubleDot) // Shift (21, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(21),
            (124, SyntaxKind.Slash) // Shift (132, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(132),
            (124, SyntaxKind.DoubleAmpersand) // Shift (133, [Expression -> Expression · DoubleAmpersand Expression, EndOfLine])
                => Shift(133),
            (124, SyntaxKind.EndOfLine) // Shift (33, [StatementTerminator -> · EndOfLine, CloseBrace])
                => Shift(33),
            (124, SyntaxKind.Semicolon) // Shift (34, [StatementTerminator -> · Semicolon, CloseBrace])
                => Shift(34),
            (125, SyntaxKind.CloseBrace) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, CloseBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, CloseBrace]")
                        )
                    },
            (125, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (125, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (125, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (125, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (125, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (125, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, EndOfLine]")
                        )
                    },
            (125, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Semicolon]")
                        )
                    },
            (125, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Dot]")
                        )
                    },
            (125, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Identifier]")
                        )
                    },
            (125, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Slash]")
                        )
                    },
            (125, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, Exclamation]")
                        )
                    },
            (125, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (125, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, DoubleDot]")
                        )
                    },
            (125, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorDefineTokenNode _2)
                            => Reduce(
                                new UnvaluedPreprocessorDefinitionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier EndOfLine ·, OpenBrace]")
                        )
                    },
            (126, SyntaxKind.EndOfLine) // Shift (165, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue · EndOfLine, CloseBrace])
                => Shift(165),
            (126, SyntaxKind.Dot) // Shift (142, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue · Dot, EndOfLine])
                => Shift(142),
            (126, SyntaxKind.Identifier) // Shift (140, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue · Identifier, EndOfLine])
                => Shift(140),
            (126, SyntaxKind.String) // Shift (141, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue · String, EndOfLine])
                => Shift(141),
            (127, SyntaxKind.CloseBrace) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, CloseBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, CloseBrace]")
                        )
                    },
            (127, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (127, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (127, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (127, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (127, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (127, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, EndOfLine]")
                        )
                    },
            (127, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Semicolon]")
                        )
                    },
            (127, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Dot]")
                        )
                    },
            (127, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Identifier]")
                        )
                    },
            (127, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Slash]")
                        )
                    },
            (127, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, Exclamation]")
                        )
                    },
            (127, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (127, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, DoubleDot]")
                        )
                    },
            (127, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorIf Expression EndOfLine, [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, ExpressionNode _1, PreprocessorIfTokenNode _2)
                            => Reduce(
                                new PreprocessorIfNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIf Expression EndOfLine ·, OpenBrace]")
                        )
                    },
            (128, SyntaxKind.CloseBrace) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, CloseBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, CloseBrace]")
                        )
                    },
            (128, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (128, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (128, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (128, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (128, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (128, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, EndOfLine]")
                        )
                    },
            (128, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Semicolon]")
                        )
                    },
            (128, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Dot]")
                        )
                    },
            (128, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Identifier]")
                        )
                    },
            (128, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Slash]")
                        )
                    },
            (128, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, Exclamation]")
                        )
                    },
            (128, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (128, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, DoubleDot]")
                        )
                    },
            (128, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine, [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, IdentifierTokenNode _1, PreprocessorIfDefTokenNode _2)
                            => Reduce(
                                new PreprocessorIfDefNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorIfDef Identifier EndOfLine ·, OpenBrace]")
                        )
                    },
            (129, SyntaxKind.CloseBrace) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, CloseBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, CloseBrace]")
                        )
                    },
            (129, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (129, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (129, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (129, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (129, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (129, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, EndOfLine]")
                        )
                    },
            (129, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Semicolon]")
                        )
                    },
            (129, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Dot]")
                        )
                    },
            (129, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Identifier]")
                        )
                    },
            (129, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Slash]")
                        )
                    },
            (129, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, Exclamation]")
                        )
                    },
            (129, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (129, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, DoubleDot]")
                        )
                    },
            (129, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorInclude String EndOfLine, [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, StringTokenNode _1, PreprocessorIncludeTokenNode _2)
                            => Reduce(
                                new PreprocessorIncludeNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorInclude String EndOfLine ·, OpenBrace]")
                        )
                    },
            (130, SyntaxKind.EndOfFile) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, EndOfFile])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, EndOfFile]")
                        )
                    },
            (130, SyntaxKind.PreprocessorDefine) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorDefine]")
                        )
                    },
            (130, SyntaxKind.PreprocessorEndIf) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorEndIf]")
                        )
                    },
            (130, SyntaxKind.PreprocessorIf) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorIf]")
                        )
                    },
            (130, SyntaxKind.PreprocessorIfDef) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorIfDef]")
                        )
                    },
            (130, SyntaxKind.PreprocessorInclude) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorInclude]")
                        )
                    },
            (130, SyntaxKind.EndOfLine) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, EndOfLine]")
                        )
                    },
            (130, SyntaxKind.Semicolon) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Semicolon]")
                        )
                    },
            (130, SyntaxKind.Dot) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Dot]")
                        )
                    },
            (130, SyntaxKind.Identifier) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Identifier]")
                        )
                    },
            (130, SyntaxKind.Slash) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Slash]")
                        )
                    },
            (130, SyntaxKind.Exclamation) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Exclamation]")
                        )
                    },
            (130, SyntaxKind.OpenParenthesis) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, OpenParenthesis]")
                        )
                    },
            (130, SyntaxKind.DoubleDot) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, DoubleDot]")
                        )
                    },
            (130, SyntaxKind.OpenBrace) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, OpenBrace]")
                        )
                    },
            (131, SyntaxKind.EndOfFile) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, EndOfFile])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, EndOfFile]")
                        )
                    },
            (131, SyntaxKind.PreprocessorDefine) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorDefine]")
                        )
                    },
            (131, SyntaxKind.PreprocessorEndIf) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorEndIf]")
                        )
                    },
            (131, SyntaxKind.PreprocessorIf) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorIf]")
                        )
                    },
            (131, SyntaxKind.PreprocessorIfDef) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorIfDef]")
                        )
                    },
            (131, SyntaxKind.PreprocessorInclude) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorInclude]")
                        )
                    },
            (131, SyntaxKind.EndOfLine) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, EndOfLine]")
                        )
                    },
            (131, SyntaxKind.Semicolon) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Semicolon]")
                        )
                    },
            (131, SyntaxKind.Dot) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Dot]")
                        )
                    },
            (131, SyntaxKind.Identifier) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Identifier]")
                        )
                    },
            (131, SyntaxKind.Slash) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Slash]")
                        )
                    },
            (131, SyntaxKind.Exclamation) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Exclamation]")
                        )
                    },
            (131, SyntaxKind.OpenParenthesis) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, OpenParenthesis]")
                        )
                    },
            (131, SyntaxKind.DoubleDot) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, DoubleDot]")
                        )
                    },
            (131, SyntaxKind.OpenBrace) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, OpenBrace]")
                        )
                    },
            (132, SyntaxKind.Dot) // Shift (83, [Expression -> · Dot, EndOfLine])
                => Shift(83),
            (132, SyntaxKind.Identifier) // Shift (82, [Expression -> · Identifier, EndOfLine])
                => Shift(82),
            (132, SyntaxKind.Slash) // Shift (84, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(84),
            (132, SyntaxKind.Exclamation) // Shift (85, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(85),
            (132, SyntaxKind.OpenParenthesis) // Shift (86, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(86),
            (132, SyntaxKind.DoubleDot) // Shift (87, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(87),
            (133, SyntaxKind.Dot) // Shift (83, [Expression -> · Dot, EndOfLine])
                => Shift(83),
            (133, SyntaxKind.Identifier) // Shift (82, [Expression -> · Identifier, EndOfLine])
                => Shift(82),
            (133, SyntaxKind.Slash) // Shift (84, [Expression -> · Slash Identifier, EndOfLine])
                => Shift(84),
            (133, SyntaxKind.Exclamation) // Shift (85, [Expression -> · Exclamation Expression, EndOfLine])
                => Shift(85),
            (133, SyntaxKind.OpenParenthesis) // Shift (86, [Expression -> · OpenParenthesis Expression CloseParenthesis, EndOfLine])
                => Shift(86),
            (133, SyntaxKind.DoubleDot) // Shift (87, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, EndOfLine])
                => Shift(87),
            (134, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (134, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (134, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (134, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (134, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (134, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (135, SyntaxKind.EndOfLine) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, EndOfLine]")
                        )
                    },
            (135, SyntaxKind.Semicolon) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Semicolon]")
                        )
                    },
            (135, SyntaxKind.Slash) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Slash]")
                        )
                    },
            (135, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, DoubleAmpersand]")
                        )
                    },
            (136, SyntaxKind.EndOfLine) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, EndOfLine]")
                        )
                    },
            (136, SyntaxKind.Semicolon) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Semicolon])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Semicolon]")
                        )
                    },
            (136, SyntaxKind.Slash) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Slash]")
                        )
                    },
            (136, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, DoubleAmpersand]")
                        )
                    },
            (137, SyntaxKind.CloseParenthesis) // Shift (169, [Expression -> OpenParenthesis Expression · CloseParenthesis, EndOfLine])
                => Shift(169),
            (137, SyntaxKind.Slash) // Shift (113, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(113),
            (137, SyntaxKind.DoubleAmpersand) // Shift (114, [Expression -> Expression · DoubleAmpersand Expression, CloseParenthesis])
                => Shift(114),
            (138, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (138, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (138, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (138, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (138, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (138, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (139, SyntaxKind.EndOfFile) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, EndOfFile])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, EndOfFile]")
                        )
                    },
            (139, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (139, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (139, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (139, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (139, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (139, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, EndOfLine]")
                        )
                    },
            (139, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Semicolon]")
                        )
                    },
            (139, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Dot]")
                        )
                    },
            (139, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Identifier]")
                        )
                    },
            (139, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Slash]")
                        )
                    },
            (139, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Exclamation]")
                        )
                    },
            (139, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (139, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, DoubleDot]")
                        )
                    },
            (139, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, OpenBrace]")
                        )
                    },
            (140, SyntaxKind.EndOfLine) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier ·, EndOfLine]")
                        )
                    },
            (140, SyntaxKind.Dot) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier ·, Dot]")
                        )
                    },
            (140, SyntaxKind.Identifier) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier ·, Identifier]")
                        )
                    },
            (140, SyntaxKind.String) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier ·, String])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Identifier ·, String]")
                        )
                    },
            (141, SyntaxKind.EndOfLine) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue String, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue String ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (StringTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue String ·, EndOfLine]")
                        )
                    },
            (141, SyntaxKind.Dot) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue String, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue String ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (StringTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue String ·, Dot]")
                        )
                    },
            (141, SyntaxKind.Identifier) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue String, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue String ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (StringTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue String ·, Identifier]")
                        )
                    },
            (141, SyntaxKind.String) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue String, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue String ·, String])
                => (PopNode()!,PopNode()!) switch {
                        (StringTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue String ·, String]")
                        )
                    },
            (142, SyntaxKind.EndOfLine) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot ·, EndOfLine])
                => (PopNode()!,PopNode()!) switch {
                        (DotTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot ·, EndOfLine]")
                        )
                    },
            (142, SyntaxKind.Dot) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot ·, Dot])
                => (PopNode()!,PopNode()!) switch {
                        (DotTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot ·, Dot]")
                        )
                    },
            (142, SyntaxKind.Identifier) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot ·, Identifier])
                => (PopNode()!,PopNode()!) switch {
                        (DotTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot ·, Identifier]")
                        )
                    },
            (142, SyntaxKind.String) // Reduce (PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot, [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot ·, String])
                => (PopNode()!,PopNode()!) switch {
                        (DotTokenNode _0, PreprocessorDefinitionValueNode _1)
                            => Reduce(
                                new MultipleItemPreprocessorDefinitionValueNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    45 => 92, 75 => 126,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorDefinitionValue -> PreprocessorDefinitionValue Dot ·, String]")
                        )
                    },
            (143, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (143, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (143, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (143, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (143, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (143, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (144, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (144, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (144, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (144, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (144, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (144, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (145, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (145, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (145, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (145, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (145, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (145, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (146, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (146, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (146, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (146, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (146, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (146, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (147, SyntaxKind.EndOfLine) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (147, SyntaxKind.Semicolon) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Semicolon]")
                        )
                    },
            (147, SyntaxKind.Equals) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Equals])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Equals]")
                        )
                    },
            (147, SyntaxKind.OpenBrace) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, OpenBrace]")
                        )
                    },
            (147, SyntaxKind.Slash) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (147, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (148, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, CloseParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, CloseParenthesis]")
                        )
                    },
            (148, SyntaxKind.Slash) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Slash]")
                        )
                    },
            (148, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, DoubleAmpersand]")
                        )
                    },
            (148, SyntaxKind.Comma) // Reduce (Expression -> Slash Identifier, [Expression -> Slash Identifier ·, Comma])
                => (PopNode()!,PopNode()!) switch {
                        (IdentifierTokenNode _0, SlashTokenNode _1)
                            => Reduce(
                                new RootPathExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Slash Identifier ·, Comma]")
                        )
                    },
            (149, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, CloseParenthesis])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, CloseParenthesis]")
                        )
                    },
            (149, SyntaxKind.Slash) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Slash])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Slash]")
                        )
                    },
            (149, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, DoubleAmpersand]")
                        )
                    },
            (149, SyntaxKind.Comma) // Reduce (Expression -> Exclamation Expression, [Expression -> Exclamation Expression ·, Comma])
                => (PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, ExclamationTokenNode _1)
                            => Reduce(
                                new UnaryExpressionNode(
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Exclamation Expression ·, Comma]")
                        )
                    },
            (150, SyntaxKind.CloseParenthesis) // Shift (175, [Expression -> OpenParenthesis Expression · CloseParenthesis, CloseParenthesis])
                => Shift(175),
            (150, SyntaxKind.Slash) // Shift (113, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(113),
            (150, SyntaxKind.DoubleAmpersand) // Shift (114, [Expression -> Expression · DoubleAmpersand Expression, CloseParenthesis])
                => Shift(114),
            (151, SyntaxKind.Dot) // Shift (97, [Expression -> · Dot, CloseParenthesis])
                => Shift(97),
            (151, SyntaxKind.Identifier) // Shift (96, [Expression -> · Identifier, CloseParenthesis])
                => Shift(96),
            (151, SyntaxKind.Slash) // Shift (99, [Expression -> · Slash Identifier, CloseParenthesis])
                => Shift(99),
            (151, SyntaxKind.Exclamation) // Shift (100, [Expression -> · Exclamation Expression, CloseParenthesis])
                => Shift(100),
            (151, SyntaxKind.OpenParenthesis) // Shift (101, [Expression -> · OpenParenthesis Expression CloseParenthesis, CloseParenthesis])
                => Shift(101),
            (151, SyntaxKind.DoubleDot) // Shift (102, [Expression -> · DoubleDot OpenParenthesis ExpressionList CloseParenthesis, CloseParenthesis])
                => Shift(102),
            (152, SyntaxKind.EndOfLine) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, EndOfLine]")
                        )
                    },
            (152, SyntaxKind.Slash) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Slash]")
                        )
                    },
            (152, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, DoubleAmpersand]")
                        )
                    },
            (153, SyntaxKind.EndOfLine) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, EndOfLine]")
                        )
                    },
            (153, SyntaxKind.Slash) // Shift (104, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(104),
            (153, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand]")
                        )
                    },
            (154, SyntaxKind.CloseParenthesis) // Shift (177, [Expression -> Identifier OpenParenthesis ExpressionList · CloseParenthesis, EndOfLine])
                => Shift(177),
            (154, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (155, SyntaxKind.EndOfLine) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (155, SyntaxKind.Slash) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash]")
                        )
                    },
            (155, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (156, SyntaxKind.CloseParenthesis) // Shift (178, [Expression -> DoubleDot OpenParenthesis ExpressionList · CloseParenthesis, EndOfLine])
                => Shift(178),
            (156, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (157, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, CloseParenthesis]")
                        )
                    },
            (157, SyntaxKind.Slash) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Slash]")
                        )
                    },
            (157, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, DoubleAmpersand]")
                        )
                    },
            (158, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, CloseParenthesis]")
                        )
                    },
            (158, SyntaxKind.Slash) // Shift (113, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(113),
            (158, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand]")
                        )
                    },
            (159, SyntaxKind.CloseParenthesis) // Shift (179, [Expression -> Identifier OpenParenthesis ExpressionList · CloseParenthesis, CloseParenthesis])
                => Shift(179),
            (159, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (160, SyntaxKind.CloseParenthesis) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, CloseParenthesis]")
                        )
                    },
            (160, SyntaxKind.Slash) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash]")
                        )
                    },
            (160, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (161, SyntaxKind.CloseParenthesis) // Shift (180, [Expression -> DoubleDot OpenParenthesis ExpressionList · CloseParenthesis, CloseParenthesis])
                => Shift(180),
            (161, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (162, SyntaxKind.EndOfLine) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (162, SyntaxKind.Semicolon) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Semicolon]")
                        )
                    },
            (162, SyntaxKind.Equals) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Equals])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Equals]")
                        )
                    },
            (162, SyntaxKind.OpenBrace) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, OpenBrace]")
                        )
                    },
            (162, SyntaxKind.Slash) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (162, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (163, SyntaxKind.CloseBrace) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, CloseBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, CloseBrace]")
                        )
                    },
            (163, SyntaxKind.PreprocessorDefine) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorDefine]")
                        )
                    },
            (163, SyntaxKind.PreprocessorEndIf) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorEndIf]")
                        )
                    },
            (163, SyntaxKind.PreprocessorIf) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorIf]")
                        )
                    },
            (163, SyntaxKind.PreprocessorIfDef) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorIfDef]")
                        )
                    },
            (163, SyntaxKind.PreprocessorInclude) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, PreprocessorInclude]")
                        )
                    },
            (163, SyntaxKind.EndOfLine) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, EndOfLine]")
                        )
                    },
            (163, SyntaxKind.Semicolon) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Semicolon]")
                        )
                    },
            (163, SyntaxKind.Dot) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Dot]")
                        )
                    },
            (163, SyntaxKind.Identifier) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Identifier]")
                        )
                    },
            (163, SyntaxKind.Slash) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Slash]")
                        )
                    },
            (163, SyntaxKind.Exclamation) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, Exclamation]")
                        )
                    },
            (163, SyntaxKind.OpenParenthesis) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, OpenParenthesis]")
                        )
                    },
            (163, SyntaxKind.DoubleDot) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, DoubleDot]")
                        )
                    },
            (163, SyntaxKind.OpenBrace) // Reduce (Block -> Expression OpenBrace StatementList CloseBrace, [Block -> Expression OpenBrace StatementList CloseBrace ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseBraceTokenNode _0, StatementListNode _1, OpenBraceTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new ExpressionBlockNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 3, 2 => 23, 4 => 27, 26 => 67, 28 => 27, 40 => 27, 71 => 67, 72 => 27, 80 => 67,
                                    123 => 67,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Block -> Expression OpenBrace StatementList CloseBrace ·, OpenBrace]")
                        )
                    },
            (164, SyntaxKind.CloseBrace) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, CloseBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, CloseBrace]")
                        )
                    },
            (164, SyntaxKind.PreprocessorDefine) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorDefine]")
                        )
                    },
            (164, SyntaxKind.PreprocessorEndIf) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorEndIf]")
                        )
                    },
            (164, SyntaxKind.PreprocessorIf) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorIf]")
                        )
                    },
            (164, SyntaxKind.PreprocessorIfDef) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorIfDef]")
                        )
                    },
            (164, SyntaxKind.PreprocessorInclude) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, PreprocessorInclude]")
                        )
                    },
            (164, SyntaxKind.EndOfLine) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, EndOfLine]")
                        )
                    },
            (164, SyntaxKind.Semicolon) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Semicolon]")
                        )
                    },
            (164, SyntaxKind.Dot) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Dot]")
                        )
                    },
            (164, SyntaxKind.Identifier) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Identifier]")
                        )
                    },
            (164, SyntaxKind.Slash) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Slash]")
                        )
                    },
            (164, SyntaxKind.Exclamation) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, Exclamation]")
                        )
                    },
            (164, SyntaxKind.OpenParenthesis) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, OpenParenthesis]")
                        )
                    },
            (164, SyntaxKind.DoubleDot) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, DoubleDot]")
                        )
                    },
            (164, SyntaxKind.OpenBrace) // Reduce (Statement -> Expression Equals Expression StatementTerminator, [Statement -> Expression Equals Expression StatementTerminator ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (StatementTerminatorNode _0, ExpressionNode _1, EqualsTokenNode _2, ExpressionNode _3)
                            => Reduce(
                                new AssignmentStatementNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 6, 2 => 24, 4 => 30, 26 => 69, 28 => 30, 40 => 30, 71 => 69, 72 => 30, 80 => 69,
                                    123 => 69,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Statement -> Expression Equals Expression StatementTerminator ·, OpenBrace]")
                        )
                    },
            (165, SyntaxKind.CloseBrace) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, CloseBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, CloseBrace]")
                        )
                    },
            (165, SyntaxKind.PreprocessorDefine) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorDefine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorDefine]")
                        )
                    },
            (165, SyntaxKind.PreprocessorEndIf) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorEndIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorEndIf]")
                        )
                    },
            (165, SyntaxKind.PreprocessorIf) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorIf])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorIf]")
                        )
                    },
            (165, SyntaxKind.PreprocessorIfDef) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorIfDef])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorIfDef]")
                        )
                    },
            (165, SyntaxKind.PreprocessorInclude) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorInclude])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, PreprocessorInclude]")
                        )
                    },
            (165, SyntaxKind.EndOfLine) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, EndOfLine]")
                        )
                    },
            (165, SyntaxKind.Semicolon) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Semicolon]")
                        )
                    },
            (165, SyntaxKind.Dot) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Dot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Dot]")
                        )
                    },
            (165, SyntaxKind.Identifier) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Identifier])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Identifier]")
                        )
                    },
            (165, SyntaxKind.Slash) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Slash]")
                        )
                    },
            (165, SyntaxKind.Exclamation) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Exclamation])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, Exclamation]")
                        )
                    },
            (165, SyntaxKind.OpenParenthesis) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, OpenParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, OpenParenthesis]")
                        )
                    },
            (165, SyntaxKind.DoubleDot) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, DoubleDot])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, DoubleDot]")
                        )
                    },
            (165, SyntaxKind.OpenBrace) // Reduce (PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine, [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, OpenBrace])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (EndOfLineTokenNode _0, PreprocessorDefinitionValueNode _1, IdentifierTokenNode _2, PreprocessorDefineTokenNode _3)
                            => Reduce(
                                new ValuedPreprocessorDefinitionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 7, 2 => 25, 4 => 31, 26 => 70, 28 => 31, 40 => 31, 71 => 70, 72 => 31, 80 => 70,
                                    123 => 70,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [PreprocessorStatement -> PreprocessorDefine Identifier PreprocessorDefinitionValue EndOfLine ·, OpenBrace]")
                        )
                    },
            (166, SyntaxKind.EndOfLine) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, EndOfLine]")
                        )
                    },
            (166, SyntaxKind.Semicolon) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Semicolon]")
                        )
                    },
            (166, SyntaxKind.Slash) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Slash]")
                        )
                    },
            (166, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, DoubleAmpersand]")
                        )
                    },
            (167, SyntaxKind.EndOfLine) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, EndOfLine]")
                        )
                    },
            (167, SyntaxKind.Semicolon) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, Semicolon]")
                        )
                    },
            (167, SyntaxKind.Slash) // Shift (132, [Expression -> Expression · Slash Expression, EndOfLine])
                => Shift(132),
            (167, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand]")
                        )
                    },
            (168, SyntaxKind.CloseParenthesis) // Shift (181, [Expression -> Identifier OpenParenthesis ExpressionList · CloseParenthesis, EndOfLine])
                => Shift(181),
            (168, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (169, SyntaxKind.EndOfLine) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (169, SyntaxKind.Semicolon) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Semicolon]")
                        )
                    },
            (169, SyntaxKind.Slash) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash]")
                        )
                    },
            (169, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (170, SyntaxKind.CloseParenthesis) // Shift (182, [Expression -> DoubleDot OpenParenthesis ExpressionList · CloseParenthesis, EndOfLine])
                => Shift(182),
            (170, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (171, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, CloseParenthesis]")
                        )
                    },
            (171, SyntaxKind.Slash) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Slash]")
                        )
                    },
            (171, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, DoubleAmpersand]")
                        )
                    },
            (171, SyntaxKind.Comma) // Reduce (Expression -> Expression Slash Expression, [Expression -> Expression Slash Expression ·, Comma])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, SlashTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression Slash Expression ·, Comma]")
                        )
                    },
            (172, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, CloseParenthesis]")
                        )
                    },
            (172, SyntaxKind.Slash) // Shift (143, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(143),
            (172, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, DoubleAmpersand]")
                        )
                    },
            (172, SyntaxKind.Comma) // Reduce (Expression -> Expression DoubleAmpersand Expression, [Expression -> Expression DoubleAmpersand Expression ·, Comma])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, DoubleAmpersandTokenNode _1, ExpressionNode _2)
                            => Reduce(
                                new BinaryExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Expression DoubleAmpersand Expression ·, Comma]")
                        )
                    },
            (173, SyntaxKind.CloseParenthesis) // Shift (183, [Expression -> Identifier OpenParenthesis ExpressionList · CloseParenthesis, CloseParenthesis])
                => Shift(183),
            (173, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (174, SyntaxKind.CloseParenthesis) // Reduce (ExpressionList -> ExpressionList Comma Expression, [ExpressionList -> ExpressionList Comma Expression ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, CommaTokenNode _1, ExpressionListNode _2)
                            => Reduce(
                                new MultipleItemExpressionListNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    46 => 98, 66 => 121, 106 => 154, 110 => 156, 116 => 159, 120 => 161, 134 => 168, 138 => 170, 145 => 173,
                                    151 => 176,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [ExpressionList -> ExpressionList Comma Expression ·, CloseParenthesis]")
                        )
                    },
            (174, SyntaxKind.Comma) // Reduce (ExpressionList -> ExpressionList Comma Expression, [ExpressionList -> ExpressionList Comma Expression ·, Comma])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (ExpressionNode _0, CommaTokenNode _1, ExpressionListNode _2)
                            => Reduce(
                                new MultipleItemExpressionListNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    46 => 98, 66 => 121, 106 => 154, 110 => 156, 116 => 159, 120 => 161, 134 => 168, 138 => 170, 145 => 173,
                                    151 => 176,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [ExpressionList -> ExpressionList Comma Expression ·, Comma]")
                        )
                    },
            (174, SyntaxKind.Slash) // Shift (143, [Expression -> Expression · Slash Expression, CloseParenthesis])
                => Shift(143),
            (174, SyntaxKind.DoubleAmpersand) // Shift (144, [Expression -> Expression · DoubleAmpersand Expression, CloseParenthesis])
                => Shift(144),
            (175, SyntaxKind.CloseParenthesis) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, CloseParenthesis]")
                        )
                    },
            (175, SyntaxKind.Slash) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Slash]")
                        )
                    },
            (175, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (175, SyntaxKind.Comma) // Reduce (Expression -> OpenParenthesis Expression CloseParenthesis, [Expression -> OpenParenthesis Expression CloseParenthesis ·, Comma])
                => (PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionNode _1, OpenParenthesisTokenNode _2)
                            => Reduce(
                                new ParenthesizedExpressionNode(
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> OpenParenthesis Expression CloseParenthesis ·, Comma]")
                        )
                    },
            (176, SyntaxKind.CloseParenthesis) // Shift (184, [Expression -> DoubleDot OpenParenthesis ExpressionList · CloseParenthesis, CloseParenthesis])
                => Shift(184),
            (176, SyntaxKind.Comma) // Shift (146, [ExpressionList -> ExpressionList · Comma Expression, CloseParenthesis])
                => Shift(146),
            (177, SyntaxKind.EndOfLine) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (177, SyntaxKind.Slash) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (177, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (178, SyntaxKind.EndOfLine) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (178, SyntaxKind.Slash) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (178, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (179, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, CloseParenthesis]")
                        )
                    },
            (179, SyntaxKind.Slash) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (179, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (180, SyntaxKind.CloseParenthesis) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, CloseParenthesis]")
                        )
                    },
            (180, SyntaxKind.Slash) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (180, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (181, SyntaxKind.EndOfLine) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (181, SyntaxKind.Semicolon) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Semicolon]")
                        )
                    },
            (181, SyntaxKind.Slash) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (181, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (182, SyntaxKind.EndOfLine) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, EndOfLine]")
                        )
                    },
            (182, SyntaxKind.Semicolon) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Semicolon])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Semicolon]")
                        )
                    },
            (182, SyntaxKind.Slash) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (182, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (183, SyntaxKind.CloseParenthesis) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, CloseParenthesis]")
                        )
                    },
            (183, SyntaxKind.Slash) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (183, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (183, SyntaxKind.Comma) // Reduce (Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis, [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Comma])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, IdentifierTokenNode _3)
                            => Reduce(
                                new FunctionCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> Identifier OpenParenthesis ExpressionList CloseParenthesis ·, Comma]")
                        )
                    },
            (184, SyntaxKind.CloseParenthesis) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, CloseParenthesis])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, CloseParenthesis]")
                        )
                    },
            (184, SyntaxKind.Slash) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Slash]")
                        )
                    },
            (184, SyntaxKind.DoubleAmpersand) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, DoubleAmpersand]")
                        )
                    },
            (184, SyntaxKind.Comma) // Reduce (Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis, [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Comma])
                => (PopNode()!,PopNode()!,PopNode()!,PopNode()!) switch {
                        (CloseParenthesisTokenNode _0, ExpressionListNode _1, OpenParenthesisTokenNode _2, DoubleDotTokenNode _3)
                            => Reduce(
                                new ParentCallExpressionNode(
                                    _3,
                                    _2,
                                    _1,
                                    _0),
                                state => state switch
                                {
                                    0 => 5, 2 => 5, 4 => 29, 14 => 48, 19 => 58, 20 => 59, 26 => 29, 28 => 29, 37 => 77,
                                    40 => 29, 42 => 81, 43 => 88, 44 => 89, 46 => 95, 52 => 108, 53 => 109, 63 => 118, 64 => 119,
                                    66 => 95, 71 => 29, 72 => 29, 74 => 124, 80 => 29, 85 => 136, 86 => 137, 100 => 149, 101 => 150,
                                    104 => 152, 105 => 153, 106 => 95, 110 => 95, 113 => 157, 114 => 158, 116 => 95, 120 => 95, 123 => 29,
                                    132 => 166, 133 => 167, 134 => 95, 138 => 95, 143 => 171, 144 => 172, 145 => 95, 146 => 174, 151 => 95,
                                _ => -1
                            }),
                        (var dbg1, var dbg2, var dbg3, var dbg4) => Error(
                            new(default, "Unexpected syntax node when reducing [Expression -> DoubleDot OpenParenthesis ExpressionList CloseParenthesis ·, Comma]")
                        )
                    },
            var x => Error(ParseError.Unexpected(lookahead))
        };
}
