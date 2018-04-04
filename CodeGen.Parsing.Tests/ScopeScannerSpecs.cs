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
            var actual = sut.Scope;

            var expected = new Scope();
            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Function()
        {
            var function = new Function("main", VariableType.Void, new List<Argument>(), new Block());

            var ast = new Program(new List<Function>()
            {
                function
            });

            var sut = new ScopeScanner();
            ast.Accept(sut);

            var actual = sut.Scope;

            var scope = new Scope();
            scope.CreateChildScope(function);
            
            var expected = scope;
            
            actual.ShouldDeepEqual(expected);
        }      
        
        [Fact]
        public void FunctionWithStatements()
        {
            var function = new Function("main", VariableType.Void, new List<Argument>(), new Block(new List<Statement>()
            {
                new AssignmentStatement("a", new ExpressionNode(Operator.Add, (ReferenceExpression)"b", (ReferenceExpression)"c"))
            }));
            var ast = new Program(new List<Function> {function});
            var sut = new ScopeScanner();

            ast.Accept(sut);

            var actual = sut.Scope;
            var scope = new Scope();
            var child = scope.CreateChildScope(function);
            child.AddReference("a", 0);
            child.AddReference("b", 1);
            child.AddReference("c", 2);
            var expected = scope;
            actual.ShouldDeepEqual(expected);
        }      
    }
}