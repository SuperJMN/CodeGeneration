using System.Collections.Generic;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using DeepEqual.Syntax;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class BlockParserSpecs : ParserSpecsBase<Statement>
    {
        [Fact]
        public void DeclarationsOnly()
        {
            var actual = Parse("{ int a;\nint b; }");

            var declarationStatements = new List<DeclarationStatement>()
            {
                new DeclarationStatement(VariableType.Int, new [] { new VariableDeclaration("a"),  }),
                new DeclarationStatement(VariableType.Int, new [] { new VariableDeclaration("b"),  }),
            };

            var expected = new Block(new List<Statement>(), declarationStatements);

            actual.ShouldDeepEqual(expected);
        }    

        [Fact]
        public void DeclarationsAndStatements()
        {
            var actual = Parse("{ int a;\nint b; a = a + 1; }");

            var declarationStatements = new List<DeclarationStatement>()
            {
                new DeclarationStatement(VariableType.Int, new [] { new VariableDeclaration("a"),  }),
                new DeclarationStatement(VariableType.Int, new [] { new VariableDeclaration("b"),  }),
            };

            var expected = new Block(new List<Statement>()
                {
                    new AssignmentStatement("a", new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ConstantExpression(1)))
                }, 
                declarationStatements);

            actual.ShouldDeepEqual(expected);           
        }

        [Fact]
        public void MethodCallAndAssignment()
        {
            var block = new Block(
                new Call("func", new ReferenceExpression("a"), new ReferenceExpression("b")),
                new AssignmentStatement("a",
                    new ExpressionNode(Operator.Add, new ReferenceExpression("b"), new ReferenceExpression("c"))));

            AssertCode("{ func(a, b); a=b+c; }", block);
        }

        [Theory]
        [InlineData("{a=b;}")]
        [InlineData("{a=b;c=d;}")]
        [InlineData("{a=b;c=d;e=f+g*3;}")]
        public void ToRefactor(string code)
        {
            Parse(code);
        }

        protected override TokenListParser<LangToken, Statement> Parser => Parsers.Block;
    }
}
