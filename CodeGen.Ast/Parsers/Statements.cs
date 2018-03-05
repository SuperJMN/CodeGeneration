using CodeGen.Units;
using CodeGen.Units.Statements;
using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast.Parsers
{
    public static class Statements
    {
        public static readonly TokenListParser<LangToken, Statement> Assignment =
            from identifier in Basics.Identifier
            from eq in Token.EqualTo(LangToken.Equal)
            from expr in Expressions.ExpressionTree
            select (Statement) new AssignmentStatement(new Reference(identifier), expr);

        public static readonly TokenListParser<LangToken, Statement> FullStatement = 
            from statement in Assignment
            from semicolon in Token.EqualTo(LangToken.Semicolon)
            select statement;

        public static readonly TokenListParser<LangToken, Block> Block =
            from statements in FullStatement
                .Many()
                .Between(Token.EqualTo(LangToken.LeftBrace), Token.EqualTo(LangToken.RightBrace))
            select new Block(statements);

        public static readonly TokenListParser<LangToken, Statement> IfStatement =
            from keywork in Token.EqualTo(LangToken.If)
            from _ in Token.EqualTo(LangToken.Whitespace)
            from expr in Expressions.BooleanExpression.Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis))
            from white in Token.EqualTo(LangToken.Whitespace).Optional()
            from block in Block
            select (Statement) new IfStatement(expr, block);
    }
}