using CodeGen.Parsing.Tokenizer;
using Superpower;
using Superpower.Parsers;

namespace CodeGen.Parsing
{
    public static class ParserMixin
    {
        public static TokenListParser<LangToken, T> BetweenParenthesis<T>(this TokenListParser<LangToken, T> self)
        {
            return self.Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis));
        }

        public static TokenListParser<LangToken, T> BetweenBraces<T>(this TokenListParser<LangToken, T> self)
        {
            return self.Between(Token.EqualTo(LangToken.LeftBrace), Token.EqualTo(LangToken.RightBrace));
        }

        public static TokenListParser<LangToken, T> BetweenBrackets<T>(this TokenListParser<LangToken, T> self)
        {
            return self.Between(Token.EqualTo(LangToken.LeftBracket), Token.EqualTo(LangToken.RightBracket));
        }
    }
}