using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class InitializerSpecs :  ParserSpecsBase<InitExpression>
    {
        [Fact]
        public void Number()
        {
            AssertCode("13", new DirectInit(13));
        }  
        
        [Fact]
        public void ListOfInts()
        {
            AssertCode("{1, 2, 3}", new ListInit(1, 2, 3));
        }

        protected override TokenListParser<LangToken, InitExpression> Parser => Parsers.InitExpr;
    }
}