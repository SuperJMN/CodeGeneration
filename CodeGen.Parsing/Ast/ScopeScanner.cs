using System;
using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class ScopeScanner : ICodeVisitor
    {
        public void Visit(ExpressionNode expressionNode)
        {
            foreach (var op in expressionNode.Operands)
            {
                op.Accept(this);
            }
        }

        public void Visit(ConstantExpression expression)
        {
        }

        public void Visit(IfStatement expression)
        {
        }

        public void Visit(ReferenceExpression expression)
        {
            AddReference(expression.Reference);
        }

        public void Visit(AssignmentStatement expression)
        {
            AddReference(expression.Target);
            expression.Assignment.Accept(this);
        }

        private void AddReference(Reference reference)
        {
            Current.AddReference(reference, currentAddress);
            currentAddress++;
        }

        public void Visit(ForLoop code)
        {
        }

        public void Visit(WhileStatement expressionNode)
        {
        }

        public void Visit(DoStatement expressionNode)
        {
        }

        public void Visit(AssignmentOperatorStatement statement)
        {
        }

        public void Visit(Function function)
        {
            CreateScope(function);
            function.Block.Accept(this);
        }

        private void CreateScope(ICodeUnit scopeOwner)
        {
            currentAddress = 0;
            Current = Current.CreateChildScope(scopeOwner);
        }

        public void Visit(DeclarationStatement expressionNode)
        {
        }

        public void Visit(VariableDeclaration expressionNode)
        {
        }

        public void Visit(Program program)
        {
            foreach (var function in program.Functions)
            {
                function.Accept(this);
            }
        }

        public void Visit(Call call)
        {
        }

        public void Visit(ReturnStatement returnStatement)
        {
        }

        public void Visit(Argument argument)
        {
        }

        public ScopeScanner()
        {
            Scope = new Scope();
            Current = Scope;
        }

        int currentAddress = 0;
        private Scope Current { get; set; }
        public Scope Scope { get; } 
    }
}