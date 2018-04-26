using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class DeclarationsParserSpecs : ParserSpecsBase<DeclarationStatement>
    {
        [Fact]
        public void Int()
        {
            AssertCode("int a;", new DeclarationStatement(VariableType.Int, new VariableDeclaration("a")) );
        }

        [Fact]
        public void IntPointer()
        {
            AssertCode("int *pointer;", new DeclarationStatement(VariableType.IntPointer, new VariableDeclaration("pointer")) );
        }

        [Fact]
        public void IntWithInitialization()
        {
            AssertCode("int a=12;", new DeclarationStatement(VariableType.Int, new VariableDeclaration("a", new ConstantExpression(12))));
        }

        protected override TokenListParser<LangToken, DeclarationStatement> Parser => Parsers.DeclarationStatement;
    }
}