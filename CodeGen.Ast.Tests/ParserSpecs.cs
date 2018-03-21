using System.Collections.Generic;
using CodeGen.Ast.Parsers;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;
using CodeGen.Core;
using DeepEqual.Syntax;
using Superpower;
using Superpower.Model;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class ParserSpecs
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void Powerful(string str, Statement[] expectation)
        {
            var actual = Parse(str, Statements.ProgramParser);
            actual.ShouldDeepEqual(expectation);
        }

        public static IEnumerable<object[]> TestData => new List<object[]>()
        {
            new object[] {"a=1;", new Statement[] {new AssignmentStatement(new Reference("a"), new ConstantExpression(1)), }},
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
        };

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
        [InlineData("a=b")]
        [InlineData("a=b+c")]
        [InlineData("a=b+c+d")]
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

        private static T Parse<T>(string code, TokenListParser<LangToken, T> tokenListParser)
        {
            var tokenList = Tokenize(code);
            return tokenListParser.Parse(tokenList);
        }

        private static TokenList<LangToken> Tokenize(string str)
        {
            return TokenizerFactory.Create().Tokenize(str);
        }
    }    
}