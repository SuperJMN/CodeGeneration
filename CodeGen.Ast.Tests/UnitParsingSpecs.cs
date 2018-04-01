using CodeGen.Ast.Parsers;
using CodeGen.Ast.Units.Statements;
using DeepEqual.Syntax;
using Superpower;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class UnitParsingSpecs : ParserSpecsBase
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
                },
                Statements = { }
            });

            AssertCode("Main() { int a; int b; }", expected);
        }

        private static void AssertCode(string source, Unit expected)
        {
            var tokenList = TokenizerFactory.Create().Tokenize(source);
            var actual = Parsers.Parsers.Unit.Parse(tokenList);
            actual.ShouldDeepEqual(expected);
        }
    }
}