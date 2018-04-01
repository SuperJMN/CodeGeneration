using Superpower;
using Superpower.Model;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class ParserSpecsBase
    {
        protected static T Parse<T>(string code, TokenListParser<LangToken, T> tokenListParser)
        {
            var tokenList = Tokenize(code);
            return tokenListParser.Parse(tokenList);
        }

        private static TokenList<LangToken> Tokenize(string str)
        {
            return TokenizerFactory.Create().Tokenize(str);
        }
    }

    public class DeclarationsParserSpecs : ParserSpecsBase
    {
        [Fact]
        public void Test()
        {
            var source = "int a;\nint b;";
            var actual = Parse(source, Parsers.Parsers.Declarations);            
        }
    }

    public class BlockParserSpecs : ParserSpecsBase
    {
        [Fact]
        public void DeclarationsOnly()
        {
            var source = "{ int a;\nint b; }";
            var actual = Parse(source, Parsers.Parsers.Block);            
        }

        [Fact]
        public void DeclarationsAndStatements()
        {
            var source = "{ int a;\nint b; a = a + 1; }";
            var actual = Parse(source, Parsers.Parsers.Block);            
        }
    }
}