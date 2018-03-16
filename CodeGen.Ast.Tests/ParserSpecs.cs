using CodeGen.Ast.NewParsers;
using Superpower;
using Superpower.Model;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class ParserSpecs
    {
        [Theory]
        [InlineData("a+b*12")]
        public void OperatorExpression(string str)
        {
            Parse(str, Expressions.Expr);
        }

        [Theory]
        [InlineData("{a=b;}")]
        [InlineData("{a=b;c=d;}")]
        [InlineData("{a=b;c=d;e=f+g*3;}")]
        public void Block(string code)
        {
            Parse(code, Statements.Block);
        }


        [Theory]
        [InlineData("a=b")]
        [InlineData("a=b+c")]
        [InlineData("a=b+c+d")]
        public void Assignment(string code)
        {
            Parse(code, Statements.Assignment);
        }

        [Theory]
        [InlineData("a=b;")]
        [InlineData("a=b+c;")]
        public void Statement(string code)
        {
            Parse(code, Statements.Assignment);
        }

        [Theory]
        [InlineData("if (a==b) {c=3;}")]
        public void If(string code)
        {
            Parse(code, Statements.IfStatement);
        }

        [Theory]
        [InlineData("b==1")]
        [InlineData("true")]
        [InlineData("false")]
        public void Condition(string code)
        {
            Parse(code, Expressions.Expr);
        }

        private static void Parse<T>(string code, TokenListParser<LangToken, T> tokenListParser)
        {
            var tokenList = Tokenize(code);
            var result = tokenListParser.Parse(tokenList);
        }

        private static TokenList<LangToken> Tokenize(string str)
        {
            return TokenizerFactory.Create().Tokenize(str);
        }
    }    
}