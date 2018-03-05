using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace CodeGen.Ast
{
    public static class TokenizerFactory
    {
        public static Tokenizer<LangToken> Create()
        {
            return new TokenizerBuilder<LangToken>()
                .Match(Character.WhiteSpace.AtLeastOnce(), LangToken.Whitespace)
                .Match(Span.EqualTo("=="), LangToken.DoubleEqual)
                .Match(Character.EqualTo('='), LangToken.Equal)
                .Match(Character.EqualTo('('), LangToken.LeftParenthesis)
                .Match(Character.EqualTo(')'), LangToken.RightParenthesis)
                .Match(Character.EqualTo('{'), LangToken.LeftBrace)
                .Match(Character.EqualTo('}'), LangToken.RightBrace)
                .Match(Character.EqualTo(';'), LangToken.Semicolon)
                .Match(Span.EqualTo("if"), LangToken.If, true)
                .Match(Span.EqualTo("while"), LangToken.While, true)
                .Match(Span.EqualTo("true"), LangToken.True, true)
                .Match(Span.EqualTo("false"), LangToken.False, true)
                .Match(Span.Regex(@"\w[\w\d]*"), LangToken.Identifier, true)
                .Build();
        }
    }
}