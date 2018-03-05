using CodeGen.Units;
using CodeGen.Units.Expressions;
using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast.Parsers
{
    public static class Expressions
    {
        public static readonly TokenListParser<LangToken, Expression> Reference =
            from identifier in Basics.Identifier
            select (Expression) new ReferenceExpression(new Reference(identifier));

        public static readonly TokenListParser<LangToken, Expression> Constant =
            Basics.Number.Select(n => (Expression)new ConstantExpression(n));

        public static readonly TokenListParser<LangToken, Expression> ExpressionTree =
            Parse.Chain(
                Basics.Operator,
                Parse.Ref(() => ExpressionItem),
                (op, lhs, rhs) => new OperatorExpression(op, lhs, rhs));

        public static readonly TokenListParser<LangToken, Expression> ExpressionItem = Reference.Or(Constant);

        public static readonly TokenListParser<LangToken, Expression> BooleanCondition =
            from expr1 in ExpressionTree 
            from _ in Token.EqualTo(LangToken.Whitespace).Optional()
            from op in Basics.BooleanOperator
            from __ in Token.EqualTo(LangToken.Whitespace).Optional()
            from expr2 in ExpressionTree
            select (Expression)new BooleanExpression(op, expr1, expr2);

        public static readonly TokenListParser<LangToken, Expression> BooleanExpression = BooleanCondition.Try().Or(ExpressionItem);
    }
}