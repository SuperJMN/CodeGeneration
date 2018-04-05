using System.Collections.Generic;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class ScopeScannerSpecs
    {
        [Fact]
        public void Main()
        {
            var ast = new Program(new List<Function>());
            var sut = new ScopeScanner();
            ast.Accept(sut);
            var scope = sut.Scope;
            
            AssertScope(ast, scope);
        }

        [Fact]
        public void Function()
        {
            var function = new Function("main", VariableType.Void, new List<Argument>(), new Block());

            var ast = new Program(new List<Function>()
            {
                function
            });
            
            var scope = new Scope();
            scope.CreateChildScope(function);
            
            AssertScope(ast, scope);
        }

        [Fact]
        public void FunctionWithStatements()
        {
            var function = new Function("main", VariableType.Void, new List<Argument>(), new Block(new List<Statement>()
            {
                new AssignmentStatement("a", new ExpressionNode(Operator.Add, (ReferenceExpression)"b", (ReferenceExpression)"c"))
            }));
            var ast = new Program(new List<Function> { function });
            
            var scope = new Scope();
            var child = scope.CreateChildScope(function);
            child.AddReference("T1", 0);
            child.AddReference("b", 1);
            child.AddReference("c", 2);
            child.AddReference("a", 3);

            AssertScope(ast, scope);
        }

        [Fact]
        public void Assignment()
        {
            var ast = new AssignmentStatement("a", new ConstantExpression(1));

            var scope = new Scope();
            scope.AddReference("T1", 0);
            scope.AddReference("a", 1);

            AssertScope(ast, scope);
        }

        [Fact]
        public void Expression()
        {
            var scope = new Scope();
            scope.AddReference("T1", 0);
            scope.AddReference("a", 1);
            scope.AddReference("b", 2);

            var ast = new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ReferenceExpression("b"));

            AssertScope(ast, scope);
        }

        private static void AssertScope(ICodeUnit expressionNode, Scope expectedScope)
        {
            var sut = new ScopeScanner();

            var nameAssigner = new ImplicitReferenceNameAssigner();
            nameAssigner.AssignNames(expressionNode);

            expressionNode.Accept(sut);

            var actual = sut.Scope;

            actual.WithDeepEqual(expectedScope)
                .IgnoreProperty(r => r.Name == "Parent")
                .Assert();
        }
    }
}
