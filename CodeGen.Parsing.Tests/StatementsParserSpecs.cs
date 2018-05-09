using System;
using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using DeepEqual.Syntax;
using FluentAssertions;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class StatementsParserSpecs : ParserSpecsBase<Statement[]>
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(string str, Statement[] expectation)
        {
            var actual = Parse(str);
            actual.ShouldDeepEqual(expectation);
        }

        [Fact]
        public void If()
        {
            Statement[] expected =
            {
                new IfStatement(new ExpressionNode(Operator.Eq, new ReferenceExpression("a"),
                        new ConstantExpression(0)),
                    new AssignmentStatement("b", new ConstantExpression(0)))
            };

            AssertCode("if (a==0) b=0; ", expected);
        }

        [Fact]
        public void Func()
        {
            var assignment = new ExpressionNode(Operator.Add,
                new ReferenceExpression("a"),
                new Call("SomeFunc", new ReferenceExpression("b"), new ReferenceExpression("c")));

            Statement[] expected =
            {
                new AssignmentStatement("d", assignment), 
            };

            AssertCode("d=a+SomeFunc(b, c);", expected);
        }

        [Fact(Skip = "Not working")]
        public void String()
        {
            Statement[] expected =
            {
                new AssignmentStatement("a", new ConstantExpression("This is fine"))
            };

            AssertCode(@"a = ""This is fine"";", expected);
        }

        [Fact]
        public void While()
        {
            Statement[] expected =
            {
                new WhileStatement(new ExpressionNode(Operator.Lt, new ReferenceExpression("a"),
                        new ConstantExpression(4)),
                    new AssignmentStatement("b", new ExpressionNode(Operator.Add,
                        new ReferenceExpression("b"),
                        new ConstantExpression(1)))),
            };

            AssertCode("while (a<4) b=b+1;", expected);
        }

        [Fact]
        public void Do()
        {
            Statement[] expected =
            {
                new DoStatement(new ExpressionNode(Operator.Lt, new ReferenceExpression("a"),
                        new ConstantExpression(4)),
                    new AssignmentStatement("b", new ExpressionNode(Operator.Add,
                        new ReferenceExpression("b"),
                        new ConstantExpression(1)))),
            };

            AssertCode("do b=b+1; while (a < 4);", expected);
        }

        [Fact]
        public void DoWithoutSemicolon()
        {
            Action parse = () => Parse("do b=b+1; while (a < 4)");
            parse.Should().Throw<ParseException>();
        }

        [Fact]
        public void WhileWithoutSemicolon()
        {
            Action parse = () => Parse("while (a<4) b=b+1");
            parse.Should().Throw<ParseException>();
        }

        [Theory]
        [InlineData("+", Operator.Add)]
        [InlineData("-", Operator.Subtract)]
        [InlineData("*", Operator.Multiply)]
        [InlineData("==", Operator.Eq)]
        [InlineData("<", Operator.Lt)]
        [InlineData(">", Operator.Gt)]
        [InlineData(">=", Operator.Gte)]
        [InlineData("<=", Operator.Lte)]
        public void BinaryOperators(string symbol, string op)
        {
            Statement[] expected =
            {
                new AssignmentStatement("a",
                    new ExpressionNode(op, new ReferenceExpression("b"), new ReferenceExpression("c"))),
            };

            AssertCode($"a = b {symbol} c; ", expected);
        }

        [Fact]
        public void ForLoop()
        {
            var initialization = new AssignmentStatement("t", new ConstantExpression(0));
            var condition = new ExpressionNode(Operator.Lt, new ReferenceExpression("t"), new ConstantExpression(10));
            var step = new AssignmentStatement("t",
                new ExpressionNode(Operator.Add, new ReferenceExpression("t"), new ConstantExpression(1)));

            var forLoopOptions =
                new ForLoopHeader(initialization, condition, step);


            Statement[] expected =
            {
                new ForLoop(forLoopOptions, new AssignmentStatement("b", new ConstantExpression(123))),
            };

            AssertCode("for (t=0; t<10; t=t+1) b=123;", expected);
        }

        [Fact]
        public void Increment()
        {
            Statement[] expected =
            {
                new AssignmentOperatorStatement(Operator.Increment, "i"),
            };

            AssertCode("i++;", expected);
        }

        [Fact]
        public void IfElse()
        {
            Statement[] expected =
            {
                new IfStatement(new ExpressionNode(Operator.Eq, new ReferenceExpression("a"),
                        new ConstantExpression(0)),
                    new AssignmentStatement("b", new ConstantExpression(0)),
                    new AssignmentStatement("b", new ConstantExpression(1))),
            };

            AssertCode("if (a==0) b=0; else b=1; ", expected);
        }

        [Fact]
        public void IfElseWithBlocks()
        {
            Statement[] expected =
            {
                new IfStatement(new ExpressionNode(Operator.Eq, new ReferenceExpression("a"),
                        new ConstantExpression(0)),
                    new Block(new AssignmentStatement("b", new ConstantExpression(0))),
                    new Block(new AssignmentStatement("b", new ConstantExpression(1)))),
            };

            AssertCode("if (a==0) {b=0;} else {b=1;}", expected);
        }
      
        public static IEnumerable<object[]> TestData => new List<object[]>()
        {
            new object[]
                {"a=1;", new Statement[] {new AssignmentStatement("a", new ConstantExpression(1)),}},
            new object[] {"{}", new Statement[] {new Block()}},
            new object[]
            {
                "{ a=1; }",
                new Statement[] {new Block(new AssignmentStatement("a", new ConstantExpression(1)))}
            },
            new object[]
            {
                "{ {a=1;} }", new Statement[]
                {
                    new Block(new Block(new AssignmentStatement("a", new ConstantExpression(1)))),
                }
            },
            new object[]
            {
                "{ {a=1; b=2; } }", new Statement[]
                {
                    new Block(new Block(new AssignmentStatement("a", new ConstantExpression(1)),
                        new AssignmentStatement("b", new ConstantExpression(2))))

                }
            },
            new object[]
            {
                "{ {a=1;} {b=2;} }", new Statement[]
                {
                    new Block(new Block(new AssignmentStatement("a", new ConstantExpression(1))),
                        new Block(new AssignmentStatement("b", new ConstantExpression(2)))),
                }
            },
            new object[]
            {
                "if (a) b=3;", new Statement[]
                {
                    new IfStatement(new ReferenceExpression("a"),
                        new AssignmentStatement("b", new ConstantExpression(3))),
                }
            },
            new object[]
            {
                "a=b*c;", new Statement[]
                {
                    new AssignmentStatement("a",
                        new ExpressionNode(nameof(Operator.Multiply), new ReferenceExpression("b"),
                            new ReferenceExpression("c"))),
                }
            },
            new object[]
            {
                "a=b*(c+d);", new Statement[]
                {
                    new AssignmentStatement("a",
                        new ExpressionNode(nameof(Operator.Multiply), new ReferenceExpression("b"),
                            new ExpressionNode(nameof(Operator.Add), new ReferenceExpression("c"),
                                new ReferenceExpression("d")))),
                }
            },
            new object[]
            {
                "for (t=0;t==1;t=t+1) { a=3; }", new Statement[]
                {
                    new ForLoop(new ForLoopHeader(
                            new AssignmentStatement("t", new ConstantExpression(0)),
                            new ExpressionNode(Operator.Eq, new ReferenceExpression("t"),
                                new ConstantExpression(1)),
                            new AssignmentStatement("t",
                                new ExpressionNode(Operator.Add, new ReferenceExpression("t"),
                                    new ConstantExpression(1)))),
                        new Block(new AssignmentStatement("a", new ConstantExpression(3)))),
                }
            },
        };       

        protected override TokenListParser<LangToken, Statement[]> Parser => Parsers.Statements;
    }
}