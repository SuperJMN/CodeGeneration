using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class ExpressionParserSpecs : ParserSpecsBase<Expression>
    {

        [Theory]
        [InlineData("b==1")]
        [InlineData("true")]
        [InlineData("false")]
        public void Condition(string code)
        {
            Parse(code);
        }

        [Theory]
        [InlineData("a+b*12")]
        public void OperatorExpression(string str)
        {
            Parse(str);
        }
        
        protected override TokenListParser<LangToken, Expression> Parser => Parsers.Expression;
    }
}