using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using DeepEqual.Syntax;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class UnitParsingSpecs : ParserSpecsBase<Unit>
    {
        [Fact]
        public void Main()
        {
            AssertCode("Main() {}", new Unit("Main", new Block()));
        }

        [Fact]
        public void MainWithDeclarations()
        {
            var expected = new Unit("Main", new Block()
            {
                Declarations = new[]
                {
                    new DeclarationStatement(VariableType.Int, "a"),
                    new DeclarationStatement(VariableType.Int, "b")
                }
            });

            AssertCode("Main() { int a; int b; }", expected);
        }

        private static void AssertCode(string source, Unit expected)
        {
            var tokenList = TokenizerFactory.Create().Tokenize(source);
            var actual = Parsers.Unit.Parse(tokenList);
            actual.ShouldDeepEqual(expected);
        }

        protected override TokenListParser<LangToken, Unit> Parser => Parsers.Unit;
    }
}