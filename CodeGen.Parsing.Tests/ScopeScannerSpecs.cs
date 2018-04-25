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
            var scope = sut.SymbolTable;
            
            AssertScope(ast, scope);
        }

        [Fact]
        public void Function()
        {
            var function = new Function(new FunctionFirm("main", VariableType.Void, new List<Argument>()), new Block());

            var ast = new Program(new List<Function>()
            {
                function
            });
            
            var scope = new SymbolTable();
            scope.CreateChildScope(function);
            
            AssertScope(ast, scope);
        }

        [Fact]
        public void TwoFunctions()
        {
            var function = new Function(new FunctionFirm("main", VariableType.Void, new List<Argument>()), new Block(new List<Statement>()
            {
                new AssignmentStatement("c", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
            }));

            var addFirm = new FunctionFirm("add", VariableType.Void, new List<Argument> {new Argument(VariableType.Int, "a"), new Argument(VariableType.Int, "b")});
            var add = new Function(addFirm, new Block(new List<Statement>(){
                    new ReturnStatement(new ExpressionNode(Operator.Add, new ReferenceExpression("a"),
                        new ReferenceExpression("b"))),
                }));


            var ast = new Program(new List<Function>()
            {
                function,
                add
            });
            
            var scope = new SymbolTable();
            var mainScope = scope.CreateChildScope(function);
            mainScope.AnnotateSymbol("T1");
            mainScope.AnnotateSymbol("T2");
            mainScope.AnnotateSymbol("T3");
            mainScope.AnnotateSymbol("c");

            var addScope = scope.CreateChildScope(add);
            addScope.AnnotateSymbol("T4");
            addScope.AnnotateSymbol("a");
            addScope.AnnotateSymbol("b");            
            
            AssertScope(ast, scope);
        }

        [Fact]
        public void MainWithConstants()
        {
            var function = new Function(new FunctionFirm("main", VariableType.Void, new List<Argument>()), new Block(new List<Statement>()
            {
                new AssignmentStatement("c", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
            }));
            

            var ast = new Program(new List<Function>()
            {
                function,
            });
            
            var scope = new SymbolTable();
            var mainScope = scope.CreateChildScope(function);
            mainScope.AnnotateSymbol("T1");
            mainScope.AnnotateSymbol("T2");
            mainScope.AnnotateSymbol("T3");
            mainScope.AnnotateSymbol("c");

            AssertScope(ast, scope);
        }

        [Fact]
        public void FunctionWithStatements()
        {
            var function = new Function(new FunctionFirm("main", VariableType.Void, new List<Argument>()), new Block(new List<Statement>()
            {
                new AssignmentStatement("a", new ExpressionNode(Operator.Add, (ReferenceExpression)"b", (ReferenceExpression)"c"))
            }));
            var ast = new Program(new List<Function> { function });
            
            var scope = new SymbolTable();
            var child = scope.CreateChildScope(function);
            child.AnnotateSymbol("T1");
            child.AnnotateSymbol("b");
            child.AnnotateSymbol("c");
            child.AnnotateSymbol("a");

            AssertScope(ast, scope);
        }

        [Fact]
        public void Assignment()
        {
            var ast = new AssignmentStatement("a", new ConstantExpression(1));

            var scope = new SymbolTable();
            scope.AnnotateSymbol("T1");
            scope.AnnotateSymbol("a");

            AssertScope(ast, scope);
        }

        [Fact]
        public void Expression()
        {
            var scope = new SymbolTable();
            scope.AnnotateSymbol("T1");
            scope.AnnotateSymbol("a");
            scope.AnnotateSymbol("b");

            var ast = new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ReferenceExpression("b"));

            AssertScope(ast, scope);
        }

        private static void AssertScope(ICodeUnit expressionNode, SymbolTable expectedScope)
        {
            var sut = new ScopeScanner();

            var nameAssigner = new ImplicitReferenceNameAssigner();
            nameAssigner.AssignNames(expressionNode);

            expressionNode.Accept(sut);

            var actual = sut.SymbolTable;

            actual.WithDeepEqual(expectedScope)
                .IgnoreProperty(r => r.Name == "Parent")
                .Assert();
        }
    }
}

