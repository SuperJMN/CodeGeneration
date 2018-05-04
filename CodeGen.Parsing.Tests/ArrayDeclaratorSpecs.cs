using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class ArrayDeclaratorSpecs : ParserSpecsBase<ArrayDeclarator>
    {
        [Fact]
        public void FixedSize()
        {
            AssertCode("[10]", new ArrayDeclarator(10));
        }

        [Fact]
        public void NoSize()
        {
            AssertCode("[]", new ArrayDeclarator(null));
        }
        
        protected override TokenListParser<LangToken, ArrayDeclarator> Parser => Parsers.ArrayDeclarator;
    }
}