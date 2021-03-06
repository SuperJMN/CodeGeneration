﻿using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace CodeGen.Parsing.Tokenizer
{
    public static class TokenizerFactory
    {
        public static Tokenizer<LangToken> Create()
        {
            var stringParser = Span.Regex("[\\w\\s]*").Between(Character.EqualTo('"'), Character.EqualTo('"'));

            return new TokenizerBuilder<LangToken>()
                .Match(stringParser, LangToken.Text)
                .Ignore(Span.WhiteSpace)
                .Match(Span.EqualTo("++"), LangToken.DoublePlus)
                .Match(Span.EqualTo("--"), LangToken.DoubleMinus)
                .Match(Span.EqualTo("=="), LangToken.DoubleEqual)
                .Match(Span.EqualTo(">="), LangToken.GreaterThanOrEqual)
                .Match(Span.EqualTo("<="), LangToken.LessThanOrEqual)
                .Match(Span.EqualTo("!="), LangToken.NotEqual)
                .Match(Span.EqualTo("||"), LangToken.Or)
                .Match(Span.EqualTo("&&"), LangToken.And)
                .Match(Span.EqualTo("<"), LangToken.LessThan)
                .Match(Span.EqualTo(">"), LangToken.GreaterThan)
                .Match(Span.EqualTo("\""), LangToken.Quote)
                .Match(Character.EqualTo('='), LangToken.Equal)
                .Match(Character.EqualTo('*'), LangToken.Asterisk)
                .Match(Character.EqualTo('&'), LangToken.Ampersand)
                .Match(Character.EqualTo('+'), LangToken.Plus)
                .Match(Character.EqualTo('-'), LangToken.Minus)
                .Match(Character.EqualTo('('), LangToken.LeftParenthesis)
                .Match(Character.EqualTo(')'), LangToken.RightParenthesis)
                .Match(Character.EqualTo('{'), LangToken.LeftBrace)
                .Match(Character.EqualTo('}'), LangToken.RightBrace)
                .Match(Character.EqualTo(';'), LangToken.Semicolon)
                .Match(Character.EqualTo(':'), LangToken.Colon)
                .Match(Character.EqualTo(','), LangToken.Comma)
                .Match(Character.EqualTo('!'), LangToken.Exclamation)
                .Match(Span.EqualTo("if"), LangToken.If, true)
                .Match(Span.EqualTo("else"), LangToken.Else, true)
                .Match(Span.EqualTo("do"), LangToken.Do, true)
                .Match(Span.EqualTo("while"), LangToken.While, true)
                .Match(Span.EqualTo("for"), LangToken.For, true)
                .Match(Span.EqualTo("return"), LangToken.Return, true)
                .Match(Span.EqualTo("true"), LangToken.True, true)
                .Match(Span.EqualTo("false"), LangToken.False, true)
                .Match(Span.EqualTo("void"), LangToken.Void, true)
                .Match(Span.EqualTo("int"), LangToken.Int, true)
                .Match(Span.EqualTo("char"), LangToken.Char, true)
                
                .Match(Span.Regex(@"\d*"), LangToken.Number, true)
                .Match(Span.Regex(@"\w[\w\d]*"), LangToken.Identifier, true)                
                .Build();
        }
    }
}