using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast
{
    public class Parsers
    {
        public static readonly TokenListParser<Token, int> Number = Superpower.Parsers.Token.EqualTo(Token.Number).Apply(Numerics.IntegerInt32);

        public static readonly TokenListParser<Token, string> Identifier = Superpower.Parsers.Token.EqualTo(Token.Text).Select(token => token.ToStringValue());

        public static readonly TokenListParser<Token, Expression> Reference =
            from identifier in Identifier
            select (Expression) new ReferenceExpression(identifier);

        public static readonly TokenListParser<Token, Expression> Constant =
            Number.Select(n => (Expression)new ConstantExpression(n));

        public static readonly TokenListParser<Token, OperatorKind> Operator =
            OperatorParser(Token.Plus, OperatorKind.Sum)
                .Or(OperatorParser(Token.Asterisk, OperatorKind.Mult))
                .Or(OperatorParser(Token.Slash, OperatorKind.Div));

        public static readonly TokenListParser<Token, Expression> CallExpression =
            Parse.Chain(
                Operator,
                Parse.Ref(() => ExpressionItem),
                (op, lhs, rhs) => new OperatorExpression(op, lhs, rhs));

        public static readonly TokenListParser<Token, Expression> ExpressionItem =
            Reference.Or(Constant);

        public static readonly TokenListParser<Token, Expression> Condition =
            ExpressionItem;
        
        public static readonly TokenListParser<Token, Statement> Assignment =
            from identifier in Identifier
            from eq in Superpower.Parsers.Token.EqualTo(Token.Equal)
            from expr in CallExpression
            select (Statement) new AssignmentStatement(identifier, expr);

        public static readonly TokenListParser<Token, Statement> Statement = 
            from statement in Assignment
            from semicolon in Superpower.Parsers.Token.EqualTo(Token.Semicolon)
            select statement;

        public static readonly TokenListParser<Token, Block> Block =
            from sentences in Statement
                .Many()
                .Between(Superpower.Parsers.Token.EqualTo(Token.LeftBrace), Superpower.Parsers.Token.EqualTo(Token.RightBrace))
            select new Block(sentences);

        private static TokenListParser<Token, OperatorKind> OperatorParser(Token token, OperatorKind operatorKind)
        {
            return Superpower.Parsers.Token.EqualTo(token).Select(x => operatorKind);
        }

        public static readonly TokenListParser<Token, Statement> IfStatement =
            from keywork in Superpower.Parsers.Token.EqualTo(Token.If)
            from _ in Superpower.Parsers.Token.EqualTo(Token.Whitespace)
            from expr in Condition.Between(Superpower.Parsers.Token.EqualTo(Token.LeftParenthesis), Superpower.Parsers.Token.EqualTo(Token.RightParenthesis))
            from white in Superpower.Parsers.Token.EqualTo(Token.Whitespace).Optional()
            from block in Block
            select (Statement) new IfStatement(expr, block);
    }
}