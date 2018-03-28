using System.Collections.Generic;
using CodeGen.Ast.Parsers;
using CodeGen.Ast.Units;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;
using CodeGen.Core;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class ProgramParserTests : ParserSpecsBase
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(string str, Statement[] expectation)
        {
            var actual = Parse(str, Statements.ProgramParser);
            actual.ShouldDeepEqual(expectation);
        }

        [Fact]       
        public void If()
        {
            var expected = new[]
            {
                new IfStatement(new ExpressionNode(Operators.Eq, new ReferenceExpression("a"),
                        new ConstantExpression(0)),
                    new AssignmentStatement(new Reference("b"), new ConstantExpression(0)))
            };

            AssertCode("if (a==0) b=0; ", expected);
        }

        [Theory]
        [InlineData("+", Operators.Add)]
        [InlineData("-", Operators.Subtract)]
        [InlineData("*", Operators.Multiply)]
        [InlineData("==", Operators.Eq)]
        [InlineData("<", Operators.Lt)]
        [InlineData(">", Operators.Gt)]
        [InlineData(">=", Operators.Gte)]
        [InlineData("<=", Operators.Lte)]
        public void BinaryOperators(string symbol, string op)
        {
            var expected = new[]
            {
                new AssignmentStatement(new Reference("a"), new ExpressionNode(op, new ReferenceExpression("b"), new ReferenceExpression("c"))),
            };

            AssertCode($"a = b {symbol} c; ", expected);
        }

        [Fact]       
        public void ForLoop()
        {
            var initialization = new AssignmentStatement(new Reference("t"), new ConstantExpression(0));
            var condition = new ExpressionNode(Operators.Lt, new ReferenceExpression("t"),  new ConstantExpression(10));
            var step = new AssignmentStatement(new Reference("t"), new ExpressionNode(Operators.Add, new ReferenceExpression("t"), new ConstantExpression(1)));
            
            var forLoopOptions =
                new ForLoopOptions(initialization, condition, step);
                

            var expected = new[]
            {
                new ForLoop(forLoopOptions, new AssignmentStatement(new Reference("b"), new ConstantExpression(123))),
            };

            AssertCode("for (t=0; t<10; t=t+1) b=123;", expected);
        }

        [Fact]       
        public void IfElse()
        {
            var expected = new[]
            {
                new IfStatement(new ExpressionNode(Operators.Eq, new ReferenceExpression("a"),
                        new ConstantExpression(0)),
                    new AssignmentStatement(new Reference("b"), new ConstantExpression(0)),
                    new AssignmentStatement(new Reference("b"), new ConstantExpression(1))),
            };

            AssertCode("if (a==0) b=0; else b=1; ", expected);
        }

        [Fact]       
        public void IfElseWithBlocks()
        {
            var expected = new[]
            {
                new IfStatement(new ExpressionNode(Operators.Eq, new ReferenceExpression("a"),
                        new ConstantExpression(0)),
                    new Block(new AssignmentStatement(new Reference("b"), new ConstantExpression(0))),
                    new Block(new AssignmentStatement(new Reference("b"), new ConstantExpression(1)))),
            };

            AssertCode("if (a==0) {b=0;} else {b=1;}", expected);
        }

        private void AssertCode(string source, IEnumerable<Statement> expectedStatements)
        {
            Parse(source, Statements.ProgramParser).ShouldDeepEqual(expectedStatements);
        }

        public static IEnumerable<object[]> TestData => new List<object[]>()
        {
            new object[]
                {"a=1;", new Statement[] {new AssignmentStatement(new Reference("a"), new ConstantExpression(1)),}},
            new object[] {"{}", new Statement[] {new Block()}},
            new object[]
            {
                "{ a=1; }",
                new Statement[] {new Block(new AssignmentStatement(new Reference("a"), new ConstantExpression(1)))}
            },
            new object[]
            {
                "{ {a=1;} }", new Statement[]
                {
                    new Block(new Block(new AssignmentStatement(new Reference("a"), new ConstantExpression(1)))),
                }
            },
            new object[]
            {
                "{ {a=1; b=2; } }", new Statement[]
                {
                    new Block(new Block(new AssignmentStatement(new Reference("a"), new ConstantExpression(1)),
                        new AssignmentStatement(new Reference("b"), new ConstantExpression(2))))

                }
            },
            new object[]
            {
                "{ {a=1;} {b=2;} }", new Statement[]
                {
                    new Block(new Block(new AssignmentStatement(new Reference("a"), new ConstantExpression(1))),
                        new Block(new AssignmentStatement(new Reference("b"), new ConstantExpression(2)))),
                }
            },
            new object[]
            {
                "if (a) b=3;", new Statement[]
                {
                    new IfStatement(new ReferenceExpression("a"),
                        new AssignmentStatement(new Reference("b"), new ConstantExpression(3))),
                }
            },
            new object[]
            {
                "a=b*c;", new Statement[]
                {
                    new AssignmentStatement(new Reference("a"),
                        new ExpressionNode(nameof(Operators.Multiply), new ReferenceExpression("b"),
                            new ReferenceExpression("c"))),
                }
            },
            new object[]
            {
                "a=b*(c+d);", new Statement[]
                {
                    new AssignmentStatement(new Reference("a"),
                        new ExpressionNode(nameof(Operators.Multiply), new ReferenceExpression("b"),
                            new ExpressionNode(nameof(Operators.Add), new ReferenceExpression("c"),
                                new ReferenceExpression("d")))),
                }
            },
            new object[]
            {
                "for (t=0;t==1;t=t+1) { a=3; }", new Statement[]
                {
                    new ForLoop(new ForLoopOptions(
                            new AssignmentStatement(new Reference("t"), new ConstantExpression(0)),
                            new ExpressionNode(Operators.Eq, new ReferenceExpression("t"),
                                new ConstantExpression(1)),
                            new AssignmentStatement(new Reference("t"),
                                new ExpressionNode(Operators.Add, new ReferenceExpression("t"),
                                    new ConstantExpression(1)))),
                        new Block(new AssignmentStatement(new Reference("a"), new ConstantExpression(3)))),
                }
            },
        };
    }
}