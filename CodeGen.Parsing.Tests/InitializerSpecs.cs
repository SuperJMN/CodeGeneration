using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class InitializerSpecs :  ParserSpecsBase<InitializationExpression>
    {
        [Fact]
        public void Number()
        {
            AssertCode("13", new DirectInitialization(new ConstantExpression(13)));
        }  
        
        [Fact]
        public void ListOfInts()
        {
            AssertCode("{1, 2, 3}", new ListInitialization(1, 2, 3));
        }

        protected override TokenListParser<LangToken, InitializationExpression> Parser => Parsers.InitializationExpression;
    }
}