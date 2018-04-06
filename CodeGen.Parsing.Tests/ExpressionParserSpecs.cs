using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using DeepEqual.Syntax;
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

        [Fact]
        public void Func()
        {
            var actual = Parse("SomeFunc(a, b)");
            var expected = new Call("SomeFunc", new ReferenceExpression("a"), new ReferenceExpression("b"));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void NestedFunc()
        {
            var actual = Parse("SomeFunc(Other(a), b)");
            var expected = new Call("SomeFunc", new Call("Other", new ReferenceExpression("a")), new ReferenceExpression("b"));

            actual.ShouldDeepEqual(expected);
        }
        
        protected override TokenListParser<LangToken, Expression> Parser => Parsers.Expression;
    }
}