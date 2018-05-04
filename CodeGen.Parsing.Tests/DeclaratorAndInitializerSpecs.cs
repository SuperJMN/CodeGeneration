using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class DeclaratorAndInitializerSpecs :  ParserSpecsBase<DeclaratorAndInitializer>
    {
        [Fact]
        public void Simple()
        {
            AssertCode("a", new DeclaratorAndInitializer(new Declarator("a"), null));
        }  
        
        [Fact]
        public void SimpleInitialization()
        {
            AssertCode("a=3", new DeclaratorAndInitializer(new Declarator("a"), new DirectInitialization(new ConstantExpression(3))));
        } 

        [Fact]
        public void Array()
        {
            AssertCode("a[3]={1, 2, 3}", new DeclaratorAndInitializer(new Declarator("a", new ArrayDeclarator(3)), new ListInitialization(1, 2, 3)));
        } 

        protected override TokenListParser<LangToken, DeclaratorAndInitializer> Parser => Parsers.DeclaratorAndInitializer;
    }
}