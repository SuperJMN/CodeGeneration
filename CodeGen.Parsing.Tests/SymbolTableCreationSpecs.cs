using System.Collections.Generic;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class SymbolTableCreationSpecs
    {
        [Fact]
        public void Assignment()
        {
            var ast = new AssignmentStatement("a", new ConstantExpression(1));

            var scope = new SymbolTable();
            scope.AnnotateImplicit("T1");
            scope.AnnotateImplicit("a");

            CheckAst(ast, scope);
        }

        [Fact]
        public void DeclarationWithInitialization()
        {
            var scope = new SymbolTable();
            scope.AnnotateImplicit("T1");
            scope.AnnotateImplicit("a");
            scope.AnnotateImplicit("b");
            scope.AddDeclaration("c", PrimitiveType.Int);

            var ast = new DeclarationStatement(PrimitiveType.Int, "c",
                new DirectInitialization(new ExpressionNode(Operator.Add, new ReferenceExpression("a"),
                    new ReferenceExpression("b"))));

            CheckAst(ast, scope);
        }

        [Fact]
        public void Expression()
        {
            var scope = new SymbolTable();
            scope.AnnotateImplicit("T1");
            scope.AnnotateImplicit("a");
            scope.AnnotateImplicit("b");

            var ast = new ExpressionNode(Operator.Add, new ReferenceExpression("a"), new ReferenceExpression("b"));

            CheckAst(ast, scope);
        }

        [Fact]
        public void Function()
        {
            var function = new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block());

            var unit = new Program(new List<Function>
            {
                function
            });

            var scope = new SymbolTable();
            scope.CreateChildScope(function);

            CheckAst(unit, scope);
        }

        [Fact]
        public void FunctionWithStatements()
        {
            var function = new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block(
                new List<Statement>
                {
                    new AssignmentStatement("a",
                        new ExpressionNode(Operator.Add, (ReferenceExpression) "b", (ReferenceExpression) "c"))
                }));
            var ast = new Program(new List<Function> {function});

            var scope = new SymbolTable();
            var child = scope.CreateChildScope(function);
            child.AnnotateImplicit("T1");
            child.AnnotateImplicit("b");
            child.AnnotateImplicit("c");
            child.AnnotateImplicit("a");

            CheckAst(ast, scope);
        }

        [Fact]
        public void Main()
        {
            var ast = new Program(new List<Function>());
            var sut = new SymbolTableCreator();

            CheckAst(ast, sut.Create(ast));
        }

        [Fact]
        public void MainWithConstants()
        {
            var function = new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block(
                new List<Statement>
                {
                    new AssignmentStatement("c", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
                }));


            var ast = new Program(new List<Function>
            {
                function
            });

            var scope = new SymbolTable();
            var mainScope = scope.CreateChildScope(function);
            mainScope.AnnotateImplicit("T1");
            mainScope.AnnotateImplicit("T2");
            mainScope.AnnotateImplicit("T3");
            mainScope.AnnotateImplicit("c");

            CheckAst(ast, scope);
        }

        [Fact]
        public void TwoFunctions()
        {
            var @void = ReturnType.Void;
            var function = new Function(new FunctionFirm("main", @void, new List<Argument>()), new Block(
                new List<Statement>
                {
                    new AssignmentStatement("c", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
                }));

            var addFirm = new FunctionFirm("add", @void,
                new List<Argument> {new Argument(PrimitiveType.Int, "a"), new Argument(PrimitiveType.Int, "b")});
            var add = new Function(addFirm, new Block(new List<Statement>
            {
                new ReturnStatement(new ExpressionNode(Operator.Add, new ReferenceExpression("a"),
                    new ReferenceExpression("b")))
            }));

            var ast = new Program(new List<Function>
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
            addScope.AddDeclaration("a", PrimitiveType.Int);
            addScope.AddDeclaration("b", PrimitiveType.Int);

            CheckAst(ast, scope);
        }

        [Fact]
        public void ArrayDeclaration()
        {
            var decl = new DeclarationStatement(PrimitiveType.Int, new ArrayReferenceItem("array", new ConstantExpression(10)));
            var symbolTable = new SymbolTable();
            symbolTable.AnnotateImplicit("T1");
            symbolTable.AddDeclaration("array", PrimitiveType.Int, 11);
            symbolTable.AnnotateImplicit("T2");
            
            CheckAst(decl, symbolTable);
        }

        private static SymbolTable Create(ICodeUnit unit)
        {
            return new SymbolTableCreator().Create(unit);
        }

        private void CheckAst(ICodeUnit unit, SymbolTable expected)
        {
            var actual = Create(unit.WithAssignedNames());

            actual
                .WithDeepEqual(expected)
                .IgnoreProperty(r => r.Name == "Parent")
                .Assert();
        }
    }
}