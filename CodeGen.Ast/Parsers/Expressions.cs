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
        static readonly TokenListParser<LangToken, string> Add = Token.EqualTo(LangToken.Plus).Value(Operators.Add);
        static readonly TokenListParser<LangToken, string> Subtract = Token.EqualTo(LangToken.Minus).Value(Operators.Subtract);
        static readonly TokenListParser<LangToken, string> Or = Token.EqualTo(LangToken.Or).Value(Operators.Or);
        static readonly TokenListParser<LangToken, string> And = Token.EqualTo(LangToken.And).Value(Operators.And);
        static readonly TokenListParser<LangToken, string> Multiply = Token.EqualTo(LangToken.Asterisk).Value(Operators.Multiply);
        static readonly TokenListParser<LangToken, string> Divide = Token.EqualTo(LangToken.Slash).Value(Operators.Divide);
        static readonly TokenListParser<LangToken, string> Modulo = Token.EqualTo(LangToken.Mod).Value(Operators.Module);
        static readonly TokenListParser<LangToken, string> Lte = Token.EqualTo(LangToken.LessThanOrEqual).Value(Operators.Lte);
        static readonly TokenListParser<LangToken, string> Lt = Token.EqualTo(LangToken.LessThan).Value(Operators.Lt);
        static readonly TokenListParser<LangToken, string> Gt = Token.EqualTo(LangToken.GreaterThan).Value(Operators.Gt);
        static readonly TokenListParser<LangToken, string> Gte = Token.EqualTo(LangToken.GreaterThanOrEqual).Value(Operators.Gte);
        static readonly TokenListParser<LangToken, string> Eq = Token.EqualTo(LangToken.DoubleEqual).Value(Operators.Eq);
        static readonly TokenListParser<LangToken, string> Neq = Token.EqualTo(LangToken.NotEqual).Value(Operators.Neq);
        static readonly TokenListParser<LangToken, string> Power = Token.EqualTo(LangToken.Caret).Value(Operators.Power);
        static readonly TokenListParser<LangToken, string> Not = Token.EqualTo(LangToken.Not).Value(Operators.Not);
        static readonly TokenListParser<LangToken, string> Negate = Token.EqualTo(LangToken.Minus).Value(Operators.Negate);

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
                .Select(x => (Expression)new ConstantExpression(x.ToStringValue().Substring(1, x.ToStringValue().Length-2)));
        
        private static readonly TokenListParser<LangToken, Expression> Literal =
            Number
                .Or(Identifier)
                .Or(Text)
                .Or(Token.EqualTo(LangToken.Null).Value((Expression) new ConstantExpression(null)))
                .Or(BooleanValue)
                .Named("literal");
        
        static readonly TokenListParser<LangToken, Expression> Item = Literal;

        private static readonly TokenListParser<LangToken, Expression> Factor =
            (from lparen in Token.EqualTo(LangToken.LeftParenthesis)
                from expr in Parse.Ref(() => Expr)
                from rparen in Token.EqualTo(LangToken.RightParenthesis)
                select expr).Or(Item);

        static readonly TokenListParser<LangToken, Expression> Operand =
            (from op in Negate.Or(Not)
                from factor in Factor
                select MakeUnary(op)).Or(Factor).Named("expression");

        static readonly TokenListParser<LangToken, Expression> InnerTerm = Parse.Chain(Power, Operand, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Term = Parse.Chain(Multiply.Or(Divide).Or(Modulo), InnerTerm, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Comparand = Parse.Chain(Add.Or(Subtract), Term, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Comparison = Parse.Chain(Lte.Or(Neq).Or(Lt).Or(Gte.Or(Gt)).Or(Eq), Comparand, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Conjunction = Parse.Chain(And, Comparison, MakeBinary);

        static readonly TokenListParser<LangToken, Expression> Disjunction = Parse.Chain(Or, Conjunction, MakeBinary);

        public static readonly TokenListParser<LangToken, Expression> Expr = Disjunction;

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