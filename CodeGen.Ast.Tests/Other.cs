using CodeGen.Ast.Parsers;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class Other : ParserSpecsBase
    {
        [Theory]
        [InlineData("a+b*12")]
        public void OperatorExpression(string str)
        {
            Parse(str, Expressions.Expr);
        }

        [Theory]
        [InlineData("{a=b;}")]
        [InlineData("{a=b;c=d;}")]
        [InlineData("{a=b;c=d;e=f+g*3;}")]
        public void Block(string code)
        {
            Parse(code, Statements.Block);
        }
        
        [Theory]
        [InlineData("a=b;")]
        [InlineData("a=b+c;")]
        [InlineData("a=b+c+d;")]
        public void Assignment(string code)
        {
            Parse(code, Statements.AssignmentExpression);
        }

        [Theory]
        [InlineData("a=b;")]
        [InlineData("a=b+c;")]
        public void Statement(string code)
        {
            Parse(code, Statements.AssignmentExpression);
        }

        [Theory]
        [InlineData("if (a==b) {c=3;}")]
        public void If(string code)
        {
            Parse(code, Statements.ConditionalStatement);
        }

        [Theory]
        [InlineData("b==1")]
        [InlineData("true")]
        [InlineData("false")]
        public void Condition(string code)
        {
            Parse(code, Expressions.Expr);
        }
    }
}