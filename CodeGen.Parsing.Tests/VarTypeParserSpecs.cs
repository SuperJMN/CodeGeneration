using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class VarTypeParserSpecs : ParserSpecsBase<ReferenceType>
    {
        [Fact]
        public void Int()
        {
            AssertCode("int", ReferenceType.Int);
        }

        [Fact]
        public void Char()
        {
            AssertCode("char", ReferenceType.Char);
        }

        [Fact]
        public void Void()
        {
            AssertCode("void", ReferenceType.Void);
        }

        [Fact]
        public void IntPointer()
        {
            AssertCode("int *", ReferenceType.IntPointer);
        }

        [Fact]
        public void CharPointer()
        {
            AssertCode("char *", ReferenceType.CharPointer);
        }

        [Fact]
        public void VoidPointer()
        {
            AssertCode("void *", ReferenceType.VoidPointer);
        }

        protected override TokenListParser<LangToken, ReferenceType> Parser => Parsers.ReferenceType;
    }
}