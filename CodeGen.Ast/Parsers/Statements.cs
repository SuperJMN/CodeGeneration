﻿using System.Security.Cryptography;
using CodeGen.Ast.Units.Expressions;
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
            select (Statement)new AssignmentStatement(new Reference(identifier), expr);

        public static readonly TokenListParser<LangToken, Statement> AssignmentExpression =
            from assignment in Assignment
            from sc in Token.EqualTo(LangToken.Semicolon)
            select assignment;

        public static readonly TokenListParser<LangToken, Statement> EmptyBlock =
            from lb in Token.EqualTo(LangToken.LeftBrace)
            from rb in Token.EqualTo(LangToken.RightBrace)
            select (Statement)new Block();

        public static readonly TokenListParser<LangToken, Statement> Block =
            EmptyBlock.Try()
                .Or(from statements in Parse.Ref(() => Statement)
                        .Many()
                        .Between(Token.EqualTo(LangToken.LeftBrace), Token.EqualTo(LangToken.RightBrace))
                    select (Statement)new Block(statements));

        private static readonly TokenListParser<LangToken, Statement>
            Else = from keyworkd in Token.EqualTo(LangToken.Else)
                from statement in Statement
                select statement;

        private static readonly TokenListParser<LangToken, Statement> IfStatement = from keywork in Token.EqualTo(LangToken.If)
            from cond in Condition
            from statement in Statement
            from elseStatement in Else.OptionalOrDefault()
            select (Statement)new IfStatement(cond, statement, elseStatement);

        private static readonly TokenListParser<LangToken, Statement> DoStatement = 
            from keywork in Token.EqualTo(LangToken.Do)
            from statement in Statement
            from keyword in Token.EqualTo(LangToken.While)
            from cond in Condition
            from sc in Token.EqualTo(LangToken.Semicolon)
            select (Statement)new DoStatement(cond, statement);

        private static readonly TokenListParser<LangToken, Expression> Condition = Expressions.Expr.Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis));

        private static readonly TokenListParser<LangToken, Statement> WhileStatement =
            from keywork in Token.EqualTo(LangToken.While)
            from cond in Condition
            from statement in Statement
            select (Statement) new WhileStatement(cond, statement);
            
        public static readonly TokenListParser<LangToken, Statement> 
            ConditionalStatement =
                IfStatement.Or(WhileStatement).Or(DoStatement);

        public static readonly TokenListParser<LangToken, Statement> 
            Loop =
                from keywork in Token.EqualTo(LangToken.For)
                from header in (
                    from initialization in Assignment
                    from sc1 in Token.EqualTo(LangToken.Semicolon)
                    from condition in Expressions.Expr
                    from sc2 in Token.EqualTo(LangToken.Semicolon)
                    from step in Assignment select new {initialization, condition, step})
                    .Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis))
                from statement in Statement
                select (Statement)new ForLoop(new ForLoopHeader(header.initialization, header.condition, header.step), statement);

        public static readonly TokenListParser<LangToken, Statement> 
            SingleStatement = ConditionalStatement.Or(AssignmentExpression).Or(Loop);

        public static readonly TokenListParser<LangToken, Statement> 
            Statement = Block.Or(SingleStatement);

        public static readonly TokenListParser<LangToken, Statement[]> 
            ProgramParser = Statement.Many();
    }
}