using System.Collections.Generic;
using System.Text;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace CodeGen.Ast
{
    public class Tokenizer : Tokenizer<Token>
    {
        private readonly IDictionary<char, Token> charToTokenDict =
            new Dictionary<char, Token>()
            {
                {':', Token.Colon},
                {'=', Token.Equal},
                {';', Token.Semicolon},
                {'+', Token.Plus},
                {'*', Token.Asterisk},
                {'/', Token.Slash},
                {'-', Token.Minus},
                {'#', Token.Hash},
                {',', Token.Comma},
                {'{', Token.LeftBrace},
                {'}', Token.RightBrace},
                {'(', Token.LeftParenthesis},
                {')', Token.RightParenthesis},
                {'\n', Token.NewLine},
            };

        private readonly IDictionary<string, Token> words = new Dictionary<string, Token>()
        {
            {"if", Token.If},
            {"while", Token.While},
            {"for", Token.For},
            {"do", Token.Do},
        };

        protected override IEnumerable<Result<Token>> Tokenize(TextSpan span)
        {
            var filtered = new TextSpan(span.Source.Replace("\r\n", "\n"));
            var cursor = SkipWhiteSpace(filtered);

            do
            {
                if (cursor.Value == 'R')
                {
                    var regNum = Numerics.Integer(cursor.Remainder);
                    yield return Result.Value(Token.Register, cursor.Location, regNum.Remainder);
                    cursor = regNum.Remainder.ConsumeChar();
                }
                else if (charToTokenDict.TryGetValue(cursor.Value, out var token))
                {
                    yield return Result.Value(token, cursor.Location, cursor.Remainder);
                    cursor = cursor.Remainder.ConsumeChar();
                }
                else if (char.IsWhiteSpace(cursor.Value))
                {
                    yield return Result.Value(Token.Whitespace, cursor.Location, cursor.Remainder);
                    cursor = SkipWhiteSpace(cursor.Remainder);
                }
                else if (char.IsDigit(cursor.Value))
                {
                    var integer = Numerics.Integer(cursor.Location);
                    yield return Result.Value(Token.Number, integer.Location, integer.Remainder);
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
                        yield return Result.Value(Token.Text, start, cursor.Location);
                    }
                }
                else
                {
                    yield return Result.Empty<Token>(cursor.Location, "Unexpected token");
                }

            } while (cursor.HasValue);
        }
    }
}