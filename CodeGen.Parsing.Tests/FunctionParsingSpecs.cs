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
            AssertCode("void main() {}", new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block()));
        }

        [Fact]
        public void FuncWithReturn()
        {
            AssertCode("int suma(int a, int b) { return a+b; }", new Function(new FunctionFirm("suma", ReturnType.Int, new List<Argument>
            {
                new Argument(PrimitiveType.Int, "a"),
                new Argument(PrimitiveType.Int, "b"),
            }), new Block(new List<Statement>()
            {
                new ReturnStatement(new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ReferenceExpression("b"))),
            })));
        }

        [Fact]
        public void MainWithDeclarations()
        {
            var declarationStatements = new List<DeclarationStatement>()
            {
                new DeclarationStatement(PrimitiveType.Int, "a"),
                new DeclarationStatement(PrimitiveType.Int, "b"),
            };

            var expected = new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block()
            {
                Declarations = declarationStatements,
            });

            AssertCode("void main() { int a; int b; }", expected);
        }

        [Fact]
        public void FuctionWithArrayArgument()
        {
            var expected = new Function(new FunctionFirm("function", ReturnType.Void, new List<Argument>()
            {
                new Argument(PrimitiveType.Int, new ArrayReferenceItem("array", new ConstantExpression(10)))
            }), new Block());

            AssertCode("void function(int array[10]) { }", expected);
        }

        protected override TokenListParser<LangToken, Function> Parser => Parsers.Function;
    }
}