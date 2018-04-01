using CodeGen.Ast.Units;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Core;
using Superpower;
using Superpower.Parsers;
using Expression = CodeGen.Ast.Units.Expressions.Expression;

namespace CodeGen.Ast.Parsers
{
    public class Expressions
    {
        public static readonly TokenListParser<LangToken, string> Add = Token.EqualTo(LangToken.Plus).Value(Operators.Add);
        public static readonly TokenListParser<LangToken, string> Subtract = Token.EqualTo(LangToken.Minus).Value(Operators.Subtract);
        public static readonly TokenListParser<LangToken, string> Or = Token.EqualTo(LangToken.Or).Value(Operators.Or);
        public static readonly TokenListParser<LangToken, string> And = Token.EqualTo(LangToken.And).Value(Operators.And);
        public static readonly TokenListParser<LangToken, string> Multiply = Token.EqualTo(LangToken.Asterisk).Value(Operators.Multiply);
        public static readonly TokenListParser<LangToken, string> Divide = Token.EqualTo(LangToken.Slash).Value(Operators.Divide);
        public static readonly TokenListParser<LangToken, string> Modulo = Token.EqualTo(LangToken.Mod).Value(Operators.Module);
        public static readonly TokenListParser<LangToken, string> Lte = Token.EqualTo(LangToken.LessThanOrEqual).Value(Operators.Lte);
        public static readonly TokenListParser<LangToken, string> Lt = Token.EqualTo(LangToken.LessThan).Value(Operators.Lt);
        public static readonly TokenListParser<LangToken, string> Gt = Token.EqualTo(LangToken.GreaterThan).Value(Operators.Gt);
        public static readonly TokenListParser<LangToken, string> Gte = Token.EqualTo(LangToken.GreaterThanOrEqual).Value(Operators.Gte);
        public static readonly TokenListParser<LangToken, string> Eq = Token.EqualTo(LangToken.DoubleEqual).Value(Operators.Eq);
        public static readonly TokenListParser<LangToken, string> Neq = Token.EqualTo(LangToken.NotEqual).Value(Operators.Neq);
        public static readonly TokenListParser<LangToken, string> Power = Token.EqualTo(LangToken.Caret).Value(Operators.Power);
        public static readonly TokenListParser<LangToken, string> Not = Token.EqualTo(LangToken.Not).Value(Operators.Not);
        public static readonly TokenListParser<LangToken, string> Negate = Token.EqualTo(LangToken.Minus).Value(Operators.Negate);
        public static readonly TokenListParser<LangToken, string> Increment = Token.EqualTo(LangToken.DoublePlus).Value(Operators.Increment);
        public static readonly TokenListParser<LangToken, string> Decrement = Token.EqualTo(LangToken.DoubleMinus).Value(Operators.Decrement);

        private static readonly TokenListParser<LangToken, Expression> Number = Token.EqualTo(LangToken.Number).Apply(Numerics.IntegerInt32)
            .Select(d => (Expression)new ConstantExpression(d));

        private static readonly TokenListParser<LangToken, Expression> Identifier =
            Token.EqualTo(LangToken.Identifier)
                .Select(token => (Expression) new ReferenceExpression(token.ToStringValue()));

        private static readonly TokenListParser<LangToken, Expression> BooleanValue =
            Token.EqualTo(LangToken.True).Value((Expression) new ConstantExpression(true))
                .Or(Token.EqualTo(LangToken.False).Value((Expression) new ConstantExpression(false)))
            .Named("boolean");

        private static readonly TokenListParser<LangToken, Expression> Text = 
            Token.EqualTo(LangToken.Text)
                .Select(x => (Expression)new ConstantExpression(x));
        
        private static readonly TokenListParser<LangToken, Expression> Literal =
            Number
                .Or(Identifier)
                .Or(Text)
                .Or(Token.EqualTo(LangToken.Null).Value((Expression) new ConstantExpression(null)))
                .Or(BooleanValue)
                .Named("literal");
        
        static readonly TokenListParser<LangToken, Expression> Item = Literal;

        private static readonly TokenListParser<LangToken, Expression> Factor =
            Parse.Ref(() => Expr).Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis))
                .Or(Item);

        static readonly TokenListParser<LangToken, Expression> Operand =
            (from op in Negate.Or(Not).Or(Increment).Or(Decrement)
                from factor in Factor
                select MakeUnary(op)).Or(Factor).Named("expression");

        static readonly TokenListParser<LangToken, Expression> InnerTerm = Parse.Chain(Power, Operand, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Term = Parse.Chain(Multiply.Or(Divide).Or(Modulo), InnerTerm, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Comparand = Parse.Chain(Add.Or(Subtract), Term, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Comparison = Parse.Chain(Lte.Or(Neq).Or(Lt).Or(Gte.Or(Gt)).Or(Eq), Comparand, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Conjunction = Parse.Chain(And, Comparison, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Disjunction = Parse.Chain(Or, Conjunction, MakeBinary);

        public static readonly TokenListParser<LangToken, Expression> Expr = Disjunction;

        public static readonly TokenListParser<LangToken, Expression> Condition = Expr.Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis));

        private static Expression MakeBinary(string operatorName, Expression leftOperand, Expression rightOperand)
        {
            return new ExpressionNode(operatorName, leftOperand, rightOperand);
        }

        private static Expression MakeUnary(string operatorName)
        {
            return new ExpressionNode(operatorName);
        }
    }
}