using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class DeclaratorSpecs :  ParserSpecsBase<Declarator>
    {
        [Fact]
        public void IdentifierOnly()
        {
            AssertCode("a", new Declarator("a"));
        }

        [Fact]
        public void EmptyArray()
        {
            AssertCode("a[]", new Declarator("a", new ArrayDeclarator(null)));
        }

        [Fact]
        public void FixedSizeArray()
        {
            AssertCode("a[10]", new Declarator("a", new ArrayDeclarator(10)));
        }

        protected override TokenListParser<LangToken, Declarator> Parser => Parsers.Declarator;
    }
}