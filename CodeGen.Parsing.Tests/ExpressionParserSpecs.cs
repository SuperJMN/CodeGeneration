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
            var expected = new ExpressionNode(Operator.Eq, (ReferenceAccessItem)"b", new ConstantExpression(1));

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
            var mult = new ExpressionNode(Operator.Multiply, new ReferenceAccessItem("b"), new ConstantExpression(12));
            var expected = new ExpressionNode(Operator.Add, new ReferenceAccessItem("a"), mult);

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void AddressOfVariable()
        {
            var actual = Parse("*b + &variable");
            var pointerVal = new ExpressionNode(Operator.PointerValue, new ReferenceAccessItem("b"));
            var pointerRef = new ExpressionNode(Operator.PointerAddress, new ReferenceAccessItem("variable"));
            var expected = new ExpressionNode(Operator.Add, pointerVal, pointerRef);

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void ContentsOfReference()
        {
            var actual = Parse("*a");
            var expected = new ExpressionNode(Operator.PointerValue, new ReferenceAccessItem("a"));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void NegateReference()
        {
            var actual = Parse("!a");
            var expected = new ExpressionNode(Operator.Not, new ReferenceAccessItem("a"));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Negative()
        {
            var actual = Parse("-a");
            var expected = new ExpressionNode(Operator.Negate, new ReferenceAccessItem("a"));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Func()
        {
            var actual = Parse("SomeFunc(a, b)");
            var expected = new Call("SomeFunc", new ReferenceAccessItem("a"), new ReferenceAccessItem("b"));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void NestedFunc()
        {
            var actual = Parse("SomeFunc(Other(a), b)");
            var expected = new Call("SomeFunc", new Call("Other", new ReferenceAccessItem("a")), new ReferenceAccessItem("b"));

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void ArrayIndexedAccess()
        {
            var actual = Parse("array[30]");
            var expected = new ReferenceAccessItem("array", new ConstantExpression(30));

            actual.ShouldDeepEqual(expected);
        }

        protected override TokenListParser<LangToken, Expression> Parser => Parsers.Expression;
    }
}