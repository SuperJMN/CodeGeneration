using CodeGen.Parsing.Tokenizer;
using DeepEqual.Syntax;
using Superpower;

namespace CodeGen.Parsing.Tests
{
    public abstract class ParserSpecsBase<T>
    {
        protected T Parse(string source)
        {
            return Parser.Parse(TokenizerFactory.Create().Tokenize(source));
        }

        public void AssertCode(string source, T expectation)
        {
            var ast = Parse(source);
            ast.ShouldDeepEqual(expectation);
        }

        protected abstract TokenListParser<LangToken, T> Parser { get; } 
    }
}