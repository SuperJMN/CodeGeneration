using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
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
            AssertCode("a", new Declarator("a", null));
        }

        [Fact]
        public void EmptyArray()
        {
            AssertCode("a[]", new Declarator("a", new ArrayDeclarator(0)));
        }

        [Fact]
        public void FixedSizeArray()
        {
            AssertCode("a[10]", new Declarator("a", new ArrayDeclarator(10)));
        }

        protected override TokenListParser<LangToken, Declarator> Parser => Parsers.Declarator;
    }

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

    public class DeclaratorAndInitializerSpecs :  ParserSpecsBase<DeclaratorAndInitializer>
    {
        [Fact]
        public void Simple()
        {
            AssertCode("a", new DeclaratorAndInitializer(new Declarator("a", null), null));
        }  
        
        [Fact]
        public void SimpleInitialization()
        {
            AssertCode("a=3", new DeclaratorAndInitializer(new Declarator("a", null), new DirectInit(3)));
        } 

        [Fact]
        public void Array()
        {
            AssertCode("a[3]={1, 2, 3}", new DeclaratorAndInitializer(new Declarator("a", new ArrayDeclarator(3)), new ListInit(1, 2, 3)));
        } 

        protected override TokenListParser<LangToken, DeclaratorAndInitializer> Parser => Parsers.DeclaratorAndInitializer;
    }

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