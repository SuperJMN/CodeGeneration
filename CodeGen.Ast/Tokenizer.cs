using System.Collections.Generic;
using System.Text;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace CodeGen.Ast
{
    public class Tokenizer : Tokenizer<LangToken>
    {
        private readonly IDictionary<char, LangToken> charToTokenDict =
            new Dictionary<char, LangToken>()
            {
                {':', LangToken.Colon},
                {'=', LangToken.Equal},
                {';', LangToken.Semicolon},
                {'+', LangToken.Plus},
                {'*', LangToken.Asterisk},
                {'/', LangToken.Slash},
                {'-', LangToken.Minus},
                {'#', LangToken.Hash},
                {',', LangToken.Comma},
                {'{', LangToken.LeftBrace},
                {'}', LangToken.RightBrace},
                {'(', LangToken.LeftParenthesis},
                {')', LangToken.RightParenthesis},
                {'\n', LangToken.NewLine},
            };

        private readonly IDictionary<string, LangToken> words = new Dictionary<string, LangToken>()
        {
            {"if", LangToken.If},
            {"while", LangToken.While},
            {"for", LangToken.For},
            {"do", LangToken.Do},
        };

        protected override IEnumerable<Result<LangToken>> Tokenize(TextSpan span)
        {
            var filtered = new TextSpan(span.Source.Replace("\r\n", "\n"));
            var cursor = SkipWhiteSpace(filtered);

            do
            {
                if (cursor.Value == '=')
                {
                    var start = cursor;

                    cursor = cursor.Remainder.ConsumeChar();
                    if (cursor.HasValue && cursor.Value == '=')
                    {
                        yield return Result.Value(LangToken.DoubleEqual, start.Location, cursor.Remainder);
                        cursor = cursor.Remainder.ConsumeChar();
                    }
                    else
                    {
                        cursor = start;
                    }
                }

                if (cursor.Value == 'R')
                {
                    var regNum = Numerics.Integer(cursor.Remainder);
                    yield return Result.Value(LangToken.Register, cursor.Location, regNum.Remainder);
                    cursor = regNum.Remainder.ConsumeChar();
                }
                else if (charToTokenDict.TryGetValue(cursor.Value, out var token))
                {
                    yield return Result.Value(token, cursor.Location, cursor.Remainder);
                    cursor = cursor.Remainder.ConsumeChar();
                }
                else if (char.IsWhiteSpace(cursor.Value))
                {
                    yield return Result.Value(LangToken.Whitespace, cursor.Location, cursor.Remainder);
                    cursor = SkipWhiteSpace(cursor.Remainder);
                }
                else if (char.IsDigit(cursor.Value))
                {
                    var integer = Numerics.Integer(cursor.Location);
                    yield return Result.Value(LangToken.Number, integer.Location, integer.Remainder);
                    cursor = integer.Remainder.ConsumeChar();
                }
                else if (char.IsLetter(cursor.Value))
                {
                    var keywordBuilder = new StringBuilder();
                    var start = cursor.Location;
                    keywordBuilder.Append(cursor.Value);

                    do
                    {
                        cursor = cursor.Remainder.ConsumeChar();

                        if (cursor.HasValue && char.IsLetter(cursor.Value))
                        {
                            keywordBuilder.Append(cursor.Value);
                        }

                    } while (!words.Keys.Contains(keywordBuilder.ToString()) && cursor.HasValue &&
                             char.IsLetter(cursor.Value));

                    if (cursor.HasValue && char.IsLetter(cursor.Value))
                    {
                        cursor = cursor.Remainder.ConsumeChar();
                    }

                    var keyword = keywordBuilder.ToString();

                    if (words.Keys.Contains(keyword))
                    {
                        yield return Result.Value(words[keyword], start, cursor.Location);
                    }
                    else
                    {
                        yield return Result.Value(LangToken.Identifier, start, cursor.Location);
                    }
                }
                else
                {
                    yield return Result.Empty<LangToken>(cursor.Location, "Unexpected token");
                }

            } while (cursor.HasValue);
        }
    }
}