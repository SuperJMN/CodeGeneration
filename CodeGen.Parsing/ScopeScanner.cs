﻿using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing
{
    public class ScopeScanner : ICodeUnitVisitor
    {
        public ScopeScanner()
        {
            SymbolTable = new SymbolTable();
            CurrentSymbolTable = SymbolTable;
        }

        public SymbolTable SymbolTable { get; }

        private SymbolTable CurrentSymbolTable { get; set; }

        public void Visit(ExpressionNode expressionNode)
        {
            CurrentSymbolTable.AnnotateSymbol(expressionNode.Reference);

            foreach (var op in expressionNode.Operands)
            {
                op.Accept(this);
            }            
        }

        public void Visit(ConstantExpression expression)
        {
            CurrentSymbolTable.AnnotateSymbol(expression.Reference);
        }

        public void Visit(IfStatement expression)
        {
            expression.Condition.Accept(this);
            expression.Statement.Accept(this);
            expression.ElseStatement?.Accept(this);            
        }

        public void Visit(ReferenceExpression expression)
        {
            CurrentSymbolTable.AnnotateSymbol(expression.Reference);
        }

        public void Visit(AssignmentStatement expression)
        {
            expression.Assignment.Accept(this);
            CurrentSymbolTable.AnnotateSymbol(expression.Target);
        }

        public void Visit(ForLoop code)
        {
            code.Header.Initialization.Accept(this);
            code.Header.Condition.Accept(this);
            code.Header.Step.Accept(this);
            code.Statement.Accept(this);
        }

        public void Visit(WhileStatement expressionNode)
        {
            expressionNode.Condition.Accept(this);
            expressionNode.Statement.Accept(this);
        }

        public void Visit(DoStatement expressionNode)
        {
            expressionNode.Condition.Accept(this);
            expressionNode.Statement.Accept(this);
        }

        public void Visit(AssignmentOperatorStatement statement)
        {
        }

        public void Visit(Function function)
        {
            PushScope(function);

            foreach (var arg in function.Arguments)
            {
                arg.Accept(this);
            }

            function.Block.Accept(this);
            
            PopScope();
        }

        private void PopScope()
        {
            CurrentSymbolTable = CurrentSymbolTable.Parent;
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
            foreach (var p in call.Parameters)
            {
                p.Accept(this);
            }

            CurrentSymbolTable.AnnotateSymbol(call.Reference);
        }

        public void Visit(ReturnStatement returnStatement)
        {
            returnStatement.Expression?.Accept(this);            
        }

        public void Visit(Argument argument)
        {
            CurrentSymbolTable.AnnotateTypedSymbol(argument.Reference, argument.Type);
        }

        public void Visit(DeclarationStatement unit)
        {
            unit.Initialization?.Accept(this);

            CurrentSymbolTable.AnnotateTypedSymbol(unit.Identifier, unit.ReferenceType);
        }

        public void Visit(ListInitialization unit)
        {
        }

        public void Visit(DirectInitialization unit)
        {
            unit.Expression.Accept(this);
        }

        private void PushScope(ICodeUnit scopeOwner)
        {
            CurrentSymbolTable = CurrentSymbolTable.CreateChildScope(scopeOwner);
        }
    }
}