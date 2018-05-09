using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class VarTypeParserSpecs : ParserSpecsBase<ReturnType>
    {
        [Fact]
        public void Int()
        {
            AssertCode("int", ReturnType.Int);
        }

        [Fact]
        public void Char()
        {
            AssertCode("char", ReturnType.Char);
        }

        [Fact]
        public void Void()
        {
            AssertCode("void", ReturnType.Void);
        }

        [Fact]
        public void IntPointer()
        {
            AssertCode("int *", ReturnType.IntPointer);
        }

        [Fact]
        public void CharPointer()
        {
            AssertCode("char *", ReturnType.CharPointer);
        }

        [Fact]
        public void VoidPointer()
        {
            AssertCode("void *", ReturnType.VoidPointer);
        }

        protected override TokenListParser<LangToken, ReturnType> Parser => Parsers.ReferenceType;
    }
}