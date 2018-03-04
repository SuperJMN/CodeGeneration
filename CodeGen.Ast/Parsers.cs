using CodeGen.Units;
using CodeGen.Units.Expressions;
using CodeGen.Units.Statements;
using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast
{
    public class Parsers
    {
        public static readonly TokenListParser<LangToken, int> Number = Token.EqualTo(LangToken.Number).Apply(Numerics.IntegerInt32);

        public static readonly TokenListParser<LangToken, string> Identifier = Token.EqualTo(LangToken.Text).Select(token => token.ToStringValue());

        public static readonly TokenListParser<LangToken, Expression> Reference =
            from identifier in Identifier
            select (Expression) new ReferenceExpression(new Reference(identifier));

        public static readonly TokenListParser<LangToken, Expression> Constant =
            Number.Select(n => (Expression)new ConstantExpression(n));

        public static readonly TokenListParser<LangToken, OperatorKind> Operator =
            OperatorParser(LangToken.Plus, OperatorKind.Add)
                .Or(OperatorParser(LangToken.Asterisk, OperatorKind.Mult))
                .Or(OperatorParser(LangToken.Slash, OperatorKind.Div));

        public static readonly TokenListParser<LangToken, Expression> ExpressionTree =
            Parse.Chain(
                Operator,
                Parse.Ref(() => ExpressionItem),
                (op, lhs, rhs) => new OperatorExpression(op, lhs, rhs));

        public static readonly TokenListParser<LangToken, Expression> ExpressionItem =
            Reference.Or(Constant);


        public static readonly TokenListParser<LangToken, Expression> EqualityCondition =
            from expr1 in ExpressionTree 
            from _ in Token.EqualTo(LangToken.Whitespace).Optional()
            from isEqual in Token.EqualTo(LangToken.DoubleEqual)
            from __ in Token.EqualTo(LangToken.Whitespace).Optional()
            from expr2 in ExpressionTree
            select (Expression)new CompareExpression(expr1, expr2);

        public static readonly TokenListParser<LangToken, Expression> ConditionExpression = EqualityCondition.Try().Or(ExpressionItem);

        public static readonly TokenListParser<LangToken, Statement> Assignment =
            from identifier in Identifier
            from eq in Token.EqualTo(LangToken.Equal)
            from expr in ExpressionTree
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

        private static TokenListParser<LangToken, OperatorKind> OperatorParser(LangToken langToken, OperatorKind operatorKind)
        {
            return Token.EqualTo(langToken).Select(x => operatorKind);
        }

        public static readonly TokenListParser<LangToken, Statement> IfStatement =
            from keywork in Token.EqualTo(LangToken.If)
            from _ in Token.EqualTo(LangToken.Whitespace)
            from expr in ConditionExpression.Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis))
            from white in Token.EqualTo(LangToken.Whitespace).Optional()
            from block in Block
            select (Statement) new IfStatement(expr, block);
    }
}