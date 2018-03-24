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
                    new IfStatement(new ReferenceExpression(new Reference("a")),
                        new AssignmentStatement(new Reference("b"), new ConstantExpression(3))),
                }
            },
            new object[]
            {
                "a=b*c;", new Statement[]
                {
                    new AssignmentStatement(new Reference("a"),
                        new ExpressionNode(nameof(Operators.Multiply), new ReferenceExpression(new Reference("b")),
                            new ReferenceExpression(new Reference("c")))),
                }
            },
            new object[]
            {
                "a=b*(c+d);", new Statement[]
                {
                    new AssignmentStatement(new Reference("a"),
                        new ExpressionNode(nameof(Operators.Multiply), new ReferenceExpression(new Reference("b")),
                            new ExpressionNode(nameof(Operators.Add), new ReferenceExpression(new Reference("c")),
                                new ReferenceExpression(new Reference("d"))))),
                }
            },
        };
    }
}