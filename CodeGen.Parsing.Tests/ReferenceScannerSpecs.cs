using System.Collections.Generic;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using FluentAssertions;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class ReferenceScannerSpecs
    {
        [Fact]
        public void Assignment()
        {
            var ast = new AssignmentStatement("a", new ConstantExpression(1));
            Assert(ast, 2);
        }

        [Fact]
        public void Declarations()
        {
            var ast = new DeclarationStatement(ReferenceType.Int, "a", new DirectInitialization(new ExpressionNode(Operator.Add, (ReferenceExpression)"b", (ReferenceExpression)"c")));

            Assert(ast, 4);
        }

        [Fact]
        public void Function()
        {
            var i = ReferenceType.Int;
            var ast = new Function(new FunctionFirm("add", i, new List<Argument>
            {
                new Argument(i, "a"),
                new Argument(i, "b"),
            }), new Block(new List<Statement>
            {
                new AssignmentStatement("c", new ExpressionNode(Operator.Add, (ReferenceExpression)"a", (ReferenceExpression)"b")),
                new ReturnStatement(new ReferenceExpression("c"))
            }, new List<DeclarationStatement>()));

            Assert(ast, 7);
        }

        [Fact]
        public void MethodCall()
        {
            var ast = new Call("main", new ReferenceExpression("b"));
            Assert(ast, 2);
        }

        private static void Assert(ICodeUnit ast, int expectedRefCount)
        {
            var sut = new ReferenceScanner();
            ast.Accept(sut);

            sut.References.Count.Should().Be(expectedRefCount);
        }
    }
}