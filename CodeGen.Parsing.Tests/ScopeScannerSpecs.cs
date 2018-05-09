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
            var sut = new SymbolTableVisitor();
            ast.Accept(sut);
            var scope = sut.SymbolTable;
            
            AssertScope(ast, scope);
        }

        [Fact]
        public void Function()
        {
            var function = new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block());

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
            var @void = ReturnType.Void;
            var function = new Function(new FunctionFirm("main", @void, new List<Argument>()), new Block(new List<Statement>()
            {
                new AssignmentStatement("c", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
            }));

            var addFirm = new FunctionFirm("add", @void, new List<Argument> {new Argument(PrimitiveType.Int, "a"), new Argument(PrimitiveType.Int, "b")});
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
            mainScope.AnnotateImplicit("T1");
            mainScope.AnnotateImplicit("T2");
            mainScope.AnnotateImplicit("T3");
            mainScope.AnnotateImplicit("c");

            var addScope = scope.CreateChildScope(add);
            addScope.AnnotateImplicit("T4");
            addScope.Annotate("a", PrimitiveType.Int);
            addScope.Annotate("b", PrimitiveType.Int);            
            
            AssertScope(ast, scope);
        }

        [Fact]
        public void MainWithConstants()
        {
            var function = new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block(new List<Statement>()
            {
                new AssignmentStatement("c", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
            }));
            

            var ast = new Program(new List<Function>()
            {
                function,
            });
            
            var scope = new SymbolTable();
            var mainScope = scope.CreateChildScope(function);
            mainScope.AnnotateImplicit("T1");
            mainScope.AnnotateImplicit("T2");
            mainScope.AnnotateImplicit("T3");
            mainScope.AnnotateImplicit("c");

            AssertScope(ast, scope);
        }

        [Fact]
        public void FunctionWithStatements()
        {
            var function = new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block(new List<Statement>()
            {
                new AssignmentStatement("a", new ExpressionNode(Operator.Add, (ReferenceExpression)"b", (ReferenceExpression)"c"))
            }));
            var ast = new Program(new List<Function> { function });
            
            var scope = new SymbolTable();
            var child = scope.CreateChildScope(function);
            child.AnnotateImplicit("T1");
            child.AnnotateImplicit("b");
            child.AnnotateImplicit("c");
            child.AnnotateImplicit("a");

            AssertScope(ast, scope);
        }

        [Fact]
        public void Assignment()
        {
            var ast = new AssignmentStatement("a", new ConstantExpression(1));

            var scope = new SymbolTable();
            scope.AnnotateImplicit("T1");
            scope.AnnotateImplicit("a");

            AssertScope(ast, scope);
        }

        [Fact]
        public void Expression()
        {
            var scope = new SymbolTable();
            scope.AnnotateImplicit("T1");
            scope.AnnotateImplicit("a");
            scope.AnnotateImplicit("b");

            var ast = new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ReferenceExpression("b"));

            AssertScope(ast, scope);
        }

        [Fact]
        public void DeclarationWithInitialization()
        {
            var scope = new SymbolTable();
            scope.AnnotateImplicit("T1");
            scope.AnnotateImplicit("a");
            scope.AnnotateImplicit("b");
            scope.Annotate("c", PrimitiveType.Int);

            var ast = new DeclarationStatement(PrimitiveType.Int, "c", new DirectInitialization(new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ReferenceExpression("b"))));

            AssertScope(ast, scope);
        }

        private static void AssertScope(ICodeUnit expressionNode, SymbolTable expectedScope)
        {
            var sut = new SymbolTableVisitor();

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

