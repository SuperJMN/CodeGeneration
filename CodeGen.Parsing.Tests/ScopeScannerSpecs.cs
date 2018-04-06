using System.Collections.Generic;
using CodeGen.Core;
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
        public void TwoFunctions()
        {
            var function = new Function("main", VariableType.Void, new List<Argument>(), new Block(new List<Statement>()
            {
                new AssignmentStatement("c", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
            }));
            var add = new Function("add", VariableType.Void, new List<Argument> { new Argument(VariableType.Int, "a"), new Argument(VariableType.Int, "b")}, new Block(new List<Statement>()
            {
                new ReturnStatement(new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ReferenceExpression("b"))),
            }));


            var ast = new Program(new List<Function>()
            {
                function,
                add
            });
            
            var scope = new Scope();
            var mainScope = scope.CreateChildScope(function);
            mainScope.AddReference("T1");
            mainScope.AddReference("T2");
            mainScope.AddReference("T3");
            mainScope.AddReference("c");

            var addScope = scope.CreateChildScope(add);
            addScope.AddReference("T4");
            addScope.AddReference("a");
            addScope.AddReference("b");            
            
            AssertScope(ast, scope);
        }

        [Fact]
        public void MainWithConstants()
        {
            var function = new Function("main", VariableType.Void, new List<Argument>(), new Block(new List<Statement>()
            {
                new AssignmentStatement("c", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
            }));
            

            var ast = new Program(new List<Function>()
            {
                function,
            });
            
            var scope = new Scope();
            var mainScope = scope.CreateChildScope(function);
            mainScope.AddReference("T1");
            mainScope.AddReference("T2");
            mainScope.AddReference("T3");
            mainScope.AddReference("c");

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
            child.AddReference("T1");
            child.AddReference("b");
            child.AddReference("c");
            child.AddReference("a");

            AssertScope(ast, scope);
        }

        [Fact]
        public void Assignment()
        {
            var ast = new AssignmentStatement("a", new ConstantExpression(1));

            var scope = new Scope();
            scope.AddReference("T1");
            scope.AddReference("a");

            AssertScope(ast, scope);
        }

        [Fact]
        public void Expression()
        {
            var scope = new Scope();
            scope.AddReference("T1");
            scope.AddReference("a");
            scope.AddReference("b");

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

