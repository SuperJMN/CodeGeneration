using System.Collections.Generic;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class DeclaratorStatementSpecs : ParserSpecsBase<IEnumerable<DeclarationStatement>>
    {
        [Fact]
        public void Int()
        {
            AssertCode("int a;", new []{ new DeclarationStatement(PrimitiveType.Int, "a"), });
        }

        [Fact]
        public void Multiple()
        {
            AssertCode("int a, b;", new []
            {
                new DeclarationStatement(PrimitiveType.Int, "a"),
                new DeclarationStatement(PrimitiveType.Int, "b"),
            });
        }

        [Fact]
        public void IntPointer()
        {
            AssertCode("int *pointer;", new []{new DeclarationStatement(PrimitiveType.Int, new PointerReferenceItem("pointer")) });
        }

        [Fact]
        public void DoublePointer()
        {
            AssertCode("int **pointer;",
                new[]
                {
                    new DeclarationStatement(PrimitiveType.Int,
                        new PointerReferenceItem(new PointerReferenceItem("pointer")))
                });
        }

        [Fact]
        public void IntWithInitialization()
        {
            AssertCode("int a=12;", new[] { new DeclarationStatement(PrimitiveType.Int, "a", new DirectInitialization(new ConstantExpression(12))) });
        }

        [Fact]
        public void ArrayInitialization()
        {
            AssertCode("int a[]={1, 2, 3};", new[] { new DeclarationStatement(PrimitiveType.Int, "a", new ListInitialization(1, 2, 3)) });
        }

        protected override TokenListParser<LangToken, IEnumerable<DeclarationStatement>> Parser => Parsers.DeclarationStatement;
    }
}