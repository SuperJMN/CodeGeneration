using System.Collections.Generic;
using CodeGen.Ast.Parsers;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;
using CodeGen.Core;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class ConditionalStatementSpecs : ParserSpecsBase
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(string str, Statement expectation)
        {
            var actual = Parse(str, Parsers.Parsers.ConditionalStatement);
            actual.ShouldDeepEqual(expectation);
        }

        public static IEnumerable<object[]> TestData => new List<object[]>()
        {
            new object[]
            {
                "if (a) b=3;",
                new IfStatement(new ReferenceExpression("a"),
                    new AssignmentStatement(new Reference("b"), new ConstantExpression(3))),
            },
            new object[]
            {
                "if (a) {b=3;}",
                new IfStatement(new ReferenceExpression("a"),
                    new Block(new AssignmentStatement(new Reference("b"), new ConstantExpression(3)))),

            },
        };
    }
}