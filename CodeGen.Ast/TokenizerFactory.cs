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
                .Ignore(Span.WhiteSpace)
                .Match(Span.EqualTo("=="), LangToken.DoubleEqual)
                .Match(Span.EqualTo(">="), LangToken.GreaterThanOrEqual)
                .Match(Span.EqualTo("<="), LangToken.LessThanOrEqual)
                .Match(Span.EqualTo("!="), LangToken.NotEqual)
                .Match(Span.EqualTo("<"), LangToken.LessThan)
                .Match(Span.EqualTo(">"), LangToken.GreaterThan)
                .Match(Character.EqualTo('='), LangToken.Equal)
                .Match(Character.EqualTo('*'), LangToken.Asterisk)
                .Match(Character.EqualTo('+'), LangToken.Plus)
                .Match(Character.EqualTo('-'), LangToken.Minus)
                .Match(Character.EqualTo('('), LangToken.LeftParenthesis)
                .Match(Character.EqualTo(')'), LangToken.RightParenthesis)
                .Match(Character.EqualTo('{'), LangToken.LeftBrace)
                .Match(Character.EqualTo('}'), LangToken.RightBrace)
                .Match(Character.EqualTo(';'), LangToken.Semicolon)
                .Match(Span.EqualTo("if"), LangToken.If, true)
                .Match(Span.EqualTo("else"), LangToken.Else, true)
                .Match(Span.EqualTo("while"), LangToken.While, true)
                .Match(Span.EqualTo("for"), LangToken.For, true)
                .Match(Span.EqualTo("true"), LangToken.True, true)
                .Match(Span.EqualTo("false"), LangToken.False, true)
                .Match(Span.Regex(@"\d*"), LangToken.Number, true)
                .Match(Span.Regex(@"\w[\w\d]*"), LangToken.Identifier, true)
                .Build();
        }
    }
}