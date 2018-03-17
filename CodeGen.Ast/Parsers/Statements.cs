using System.Collections.Generic;
using CodeGen.Ast.Units.Statements;
using CodeGen.Core;
using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast.Parsers
{
    public class Statements
    {
        public static readonly TokenListParser<LangToken, Statement> Assignment =
            from identifier in Basics.Identifier
            from eq in Token.EqualTo(LangToken.Equal)
            from expr in Expressions.Expr
            select (Statement) new AssignmentStatement(new Reference(identifier), expr);

        public static readonly TokenListParser<LangToken, Statement> FullStatement = 
            from statement in Assignment
            from semicolon in Token.EqualTo(LangToken.Semicolon)
            select statement;

        public static readonly TokenListParser<LangToken, Statement> Block =
            from statements in FullStatement
                .Many()
                .Between(Token.EqualTo(LangToken.LeftBrace), Token.EqualTo(LangToken.RightBrace))
            select (Statement)new Block(statements);

        public static readonly TokenListParser<LangToken, Statement> IfStatement =
            from keywork in Token.EqualTo(LangToken.If)
            from expr in Expressions.Expr.Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis))
            from block in Block
            select (Statement) new IfStatement(expr, block);

        public static readonly TokenListParser<LangToken, Statement> SingleStatement =
            IfStatement;

        public static readonly TokenListParser<LangToken, Statement> Statement =
            Block.Try().Or(SingleStatement);
        
        public static readonly TokenListParser<LangToken, Statement[]> ProgramParser =
            Statement.Many();
    }
}