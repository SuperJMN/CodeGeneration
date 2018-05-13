using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
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

        [Fact]
        public void AssignmentToArray()
        {
            var actual = Parse("a[b+c]=123");
            var expected = new AssignmentStatement(new ReferenceAccessItem("a", new ExpressionNode(Operator.Add, (ReferenceAccessItem)"b", (ReferenceAccessItem)"c"), 0),  new ConstantExpression(123));
        }

        [Fact]
        public void ToPointerContents()
        {
            var actual = Parse("*a=123");
            var expected = new AssignmentStatement(new ReferenceAccessItem("a", null, 1), new ConstantExpression(123));
        }


        protected override TokenListParser<LangToken, Statement> Parser => Parsers.Assignment;
    }

    
}