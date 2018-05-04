using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class DeclarationsParserSpecsNew : ParserSpecsBase<DeclStatement>
    {
        [Fact]
        public void Int()
        {
            AssertCode("int a;", new DeclStatement(PrimitiveType.Int, new DeclaratorAndInitializer(new Declarator("a", null), null)));
        }

        [Fact]
        public void IntPointer()
        {
            AssertCode("int *pointer;", new DeclStatement(PrimitiveType.Int, new DeclaratorAndInitializer(new Declarator("pointer", null, 1), null)));
        }

        [Fact]
        public void IntWithInitialization()
        {
            AssertCode("int a=12;", new DeclStatement(PrimitiveType.Int, new DeclaratorAndInitializer(new Declarator("a", null), new DirectInit(12))));
        }

        protected override TokenListParser<LangToken, DeclStatement> Parser => Parsers.DeclSt;
    }
}