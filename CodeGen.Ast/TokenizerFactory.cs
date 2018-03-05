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
                .Match(Character.EqualTo('=').IgnoreThen(Character.EqualTo('=')), LangToken.DoubleEqual)
                .Match(Character.EqualTo('='), LangToken.Equal)
                .Build();
        }
    }
}