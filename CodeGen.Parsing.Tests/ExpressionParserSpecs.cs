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

        [Fact]
        public void BooleanExpression()
        {
            var actual = Parse("b==1");
            var expected = new ExpressionNode(Operator.Eq, (ReferenceExpression)"b", new ConstantExpression(1));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void True()
        {
            var actual = Parse("true");
            var expected = new ConstantExpression(true);

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Condition()
        {
            var actual = Parse("false");
            var expected = new ConstantExpression(false);

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void OperatorExpression()
        {
            var actual = Parse("a+b*12");
            var mult = new ExpressionNode(Operator.Multiply, new ReferenceExpression("b"), new ConstantExpression(12));
            var expected = new ExpressionNode(Operator.Add, new ReferenceExpression("a"), mult);

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void AddressOfVariable()
        {
            var actual = Parse("*b + &variable");
            var pointerVal = new ExpressionNode(Operator.PointerValue, new ReferenceExpression("b"));
            var pointerRef = new ExpressionNode(Operator.PointerAddress, new ReferenceExpression("variable"));
            var expected = new ExpressionNode(Operator.Add, pointerVal, pointerRef);

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void ContentsOfReference()
        {
            var actual = Parse("*a");
            var expected = new ExpressionNode(Operator.PointerValue, new ReferenceExpression("a"));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void NegateReference()
        {
            var actual = Parse("!a");
            var expected = new ExpressionNode(Operator.Not, new ReferenceExpression("a"));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Negative()
        {
            var actual = Parse("-a");
            var expected = new ExpressionNode(Operator.Negate, new ReferenceExpression("a"));

            actual.ShouldDeepEqual(expected);
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