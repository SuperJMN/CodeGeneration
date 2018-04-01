using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class DeclarationsParserSpecs : ParserSpecsBase<DeclarationStatement>
    {
        [Fact]
        public void Test()
        {
            var source = "int a;";
            var actual = Parse(source);            
        }

        protected override TokenListParser<LangToken, DeclarationStatement> Parser => Parsers.Declaration;
    }
}