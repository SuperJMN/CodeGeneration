using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;        
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class FunctionParsingSpecs : ParserSpecsBase<Function>
    {
        [Fact]
        public void Main()
        {
            AssertCode("void main() {}", new Function("main", VariableType.Void, new List<Argument>(), new Block()));
        }

        [Fact]
        public void FuncWithReturn()
        {
            AssertCode("int suma(int a, int b) { return a+b; }", new Function("suma", VariableType.Int, new List<Argument>
            {
                new Argument(VariableType.Int, "a"),
                new Argument(VariableType.Int, "b"),
            }, new Block(new List<Statement>()
            {
                new ReturnStatement(new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ReferenceExpression("b"))),
            })));
        }

        [Fact]
        public void MainWithDeclarations()
        {
            var declarationStatements = new List<DeclarationStatement>()
            {
                new DeclarationStatement(VariableType.Int, new [] { new VariableDeclaration("a"),  }),
                new DeclarationStatement(VariableType.Int, new [] { new VariableDeclaration("b"),  }),
            };

            var expected = new Function("main", VariableType.Void, new List<Argument>(), new Block()
            {
                Declarations = declarationStatements,
            });

            AssertCode("void main() { int a; int b; }", expected);
        }

        protected override TokenListParser<LangToken, Function> Parser => Parsers.Function;
    }
}