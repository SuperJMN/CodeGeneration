using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class VarTypeParserSpecs : ParserSpecsBase<VariableType>
    {
        [Fact]
        public void Int()
        {
            AssertCode("int", VariableType.Int);
        }

        [Fact]
        public void Char()
        {
            AssertCode("char", VariableType.Char);
        }

        [Fact]
        public void Void()
        {
            AssertCode("void", VariableType.Void);
        }

        [Fact]
        public void IntPointer()
        {
            AssertCode("int *", VariableType.IntPointer);
        }

        [Fact]
        public void CharPointer()
        {
            AssertCode("char *", VariableType.CharPointer);
        }

        [Fact]
        public void VoidPointer()
        {
            AssertCode("void *", VariableType.VoidPointer);
        }

        protected override TokenListParser<LangToken, VariableType> Parser => Parsers.VarType;
    }
}