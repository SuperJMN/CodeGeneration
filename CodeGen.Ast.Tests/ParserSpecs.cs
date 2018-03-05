using CodeGen.Ast.Parsers;
using Superpower;
using Superpower.Model;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class ParserSpecs
    {
        [Fact]
        public void OperatorExpression()
        {
            var tokenList = Tokenize("a+b*12");
            var generated = Expressions.ExpressionTree.Parse(tokenList);
        }

        [Theory]
        [InlineData("{a=b;}")]
        [InlineData("{a=b;c=d;}")]
        [InlineData("{a=b;c=d;e=f+g*3;}")]
        public void Block(string code)
        {
            Parse(code, Parsers.Statements.Block);
        }


        [Theory]
        [InlineData("a=b")]
        [InlineData("a=b+c")]
        [InlineData("a=b+c+d")]
        public void Assignment(string code)
        {
            Parse(code, Parsers.Statements.Assignment);
        }

        [Theory]
        [InlineData("a=b;")]
        [InlineData("a=b+c;")]
        public void Statement(string code)
        {
            Parse(code, Parsers.Statements.Assignment);
        }

        [Theory]
        [InlineData("if (a) {b=3;}")]
        [InlineData("if (a==b) {c=3;}")]
        public void If(string code)
        {
            Parse(code, Parsers.Statements.IfStatement);
        }

        [Theory]
        [InlineData("b")]
        [InlineData("b+2")]
        [InlineData("b==1")]
        public void Condition(string code)
        {
            Parse(code, Expressions.BooleanExpression);
        }

        private static void Parse<T>(string code, TokenListParser<LangToken, T> tokenListParser)
        {
            var tokenList = Tokenize(code);
            var result = tokenListParser.Parse(tokenList);
        }

        private static TokenList<LangToken> Tokenize(string aBb)
        {
            return new Tokenizer().Tokenize(aBb);
        }
    }    
}
