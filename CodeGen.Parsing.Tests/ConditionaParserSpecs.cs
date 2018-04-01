using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class ConditionaParserSpecs : ParserSpecsBase<Statement>
    {
        [Theory]
        [InlineData("if (a==b) {c=3;}")]
        public void If(string code)
        {
            Parse(code);
        }

        protected override TokenListParser<LangToken, Statement> Parser => Parsers.ConditionalStatement;
    }
}