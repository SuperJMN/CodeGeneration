using CodeGen.Units.Expressions;
using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast.Parsers
{
    public static class Basics
    {
        public static readonly TokenListParser<LangToken, int> Number = Token.EqualTo(LangToken.Number).Apply(Numerics.IntegerInt32);

        public static readonly TokenListParser<LangToken, string> Identifier = Token.EqualTo(LangToken.Text).Select(token => token.ToStringValue());

        public static readonly TokenListParser<LangToken, OperatorKind> Operator =
            OperatorParser(LangToken.Plus, OperatorKind.Add)
                .Or(OperatorParser(LangToken.Asterisk, OperatorKind.Mult))
                .Or(OperatorParser(LangToken.Slash, OperatorKind.Div));

        public static readonly TokenListParser<LangToken, BooleanOperatorKind> BooleanOperator =
            from eq in Token.EqualTo(LangToken.Equal)
            select BooleanOperatorKind.Equal;


        private static TokenListParser<LangToken, OperatorKind> OperatorParser(LangToken langToken, OperatorKind operatorKind)
        {
            return Token.EqualTo(langToken).Select(x => operatorKind);
        }
    }
}