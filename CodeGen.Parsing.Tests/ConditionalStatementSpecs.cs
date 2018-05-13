using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using DeepEqual.Syntax;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class ConditionalStatementSpecs : ParserSpecsBase<Statement>
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(string str, Statement expectation)
        {
            var actual = Parse(str);
            actual.ShouldDeepEqual(expectation);
        }

        public static IEnumerable<object[]> TestData => new List<object[]>()
        {
            new object[]
            {
                "if (a) b=3;",
                new IfStatement(new ReferenceAccessItem("a"),
                    new AssignmentStatement("b", new ConstantExpression(3))),
            },
            new object[]
            {
                "if (a) {b=3;}",
                new IfStatement(new ReferenceAccessItem("a"),
                    new Block(new AssignmentStatement("b", new ConstantExpression(3)))),

            },
        };

        protected override TokenListParser<LangToken, Statement> Parser => Parsers.ConditionalStatement;
    }
}