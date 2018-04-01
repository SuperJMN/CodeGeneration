using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class AssignmentSpecs : ParserSpecsBase<Statement>
    {
        [Theory]
        [InlineData("a=b;")]
        [InlineData("a=b+c;")]
        [InlineData("a=b+c+d;")]
        public void Assignment(string code)
        {
            Parse(code);
        }

        protected override TokenListParser<LangToken, Statement> Parser => Parsers.Assignment;
    }
}