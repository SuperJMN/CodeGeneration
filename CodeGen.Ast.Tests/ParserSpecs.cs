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
            var generated = Parsers.CallExpression.Parse(tokenList);
        }

        [Theory]
        [InlineData("{a=b;}")]
        [InlineData("{a=b;c=d;}")]
        [InlineData("{a=b;c=d;e=f+g*3;}")]
        public void Block(string code)
        {
            Parse(code, Parsers.Block);
        }


        [Theory]
        [InlineData("a=b")]
        [InlineData("a=b+c")]
        [InlineData("a=b+c+d")]
        public void Assignment(string code)
        {
            Parse(code, Parsers.Assignment);
        }

        [Theory]
        [InlineData("a=b;")]
        [InlineData("a=b+c;")]
        public void Statement(string code)
        {
            Parse(code, Parsers.Assignment);
        }

        [Theory]
        [InlineData("if (a) {b=3;}")]
        [InlineData("if (a==b) {c=3;}")]
        public void If(string code)
        {
            Parse(code, Parsers.IfStatement);
        }

        private static void Parse<T>(string code, TokenListParser<Token, T> tokenListParser)
        {
            var tokenList = Tokenize(code);
            var result = tokenListParser.Parse(tokenList);
        }

        private static TokenList<Token> Tokenize(string aBb)
        {
            return new Tokenizer().Tokenize(aBb);
        }
    }    
}
