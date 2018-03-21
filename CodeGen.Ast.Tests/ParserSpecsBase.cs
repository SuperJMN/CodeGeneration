using Superpower;
using Superpower.Model;

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
}