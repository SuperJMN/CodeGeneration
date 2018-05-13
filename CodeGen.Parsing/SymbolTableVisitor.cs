﻿using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing
{
    public class SymbolTableVisitor : ICodeUnitVisitor
    {
        public SymbolTableVisitor()
        {
            SymbolTableBuilder = new SymbolTableBuilder();
            CurrentSymbolTableBuilder = SymbolTableBuilder;
        }

        public SymbolTableBuilder SymbolTableBuilder { get; }

        private SymbolTableBuilder CurrentSymbolTableBuilder { get; set; }

        public void Visit(ExpressionNode expressionNode)
        {
            CurrentSymbolTableBuilder.AddAppearanceForImplicit(expressionNode.Reference);

            foreach (var op in expressionNode.Operands)
            {
                op.Accept(this);
            }            
        }

        public void Visit(ConstantExpression expression)
        {
            CurrentSymbolTableBuilder.AddAppearanceForImplicit(expression.Reference);
        }

        public void Visit(IfStatement expression)
        {
            expression.Condition.Accept(this);
            expression.Statement.Accept(this);
            expression.ElseStatement?.Accept(this);            
        }

        public void Visit(ReferenceAccessItem accessItem)
        {
            CurrentSymbolTableBuilder.AddAppearanceForImplicit(accessItem.Reference);
        }

        public void Visit(AssignmentStatement expression)
        {
            expression.Assignment.Accept(this);
            expression.Target.Accept(this);            
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
            CurrentSymbolTableBuilder = CurrentSymbolTableBuilder.Parent;
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

            CurrentSymbolTableBuilder.AddAppearanceForImplicit(call.Reference);
        }

        public void Visit(ReturnStatement returnStatement)
        {
            returnStatement.Expression?.Accept(this);            
        }

        public void Visit(Argument argument)
        {
            CurrentSymbolTableBuilder.AddAppearance(argument.AccessItem.Reference, argument.Type);
        }

        public void Visit(DeclarationStatement unit)
        {
            unit.Initialization?.Accept(this);
            CurrentSymbolTableBuilder.AddAppearance(unit.Reference, unit.ReferenceType);
            
            //if (unit.ReferenceItem is ArrayReferenceItem ari)
            //{       
            //    if (ari.AccessExpression is ConstantExpression ct)
            //    {
            //        var ctValue = (int)ct.Value;
            //        CurrentSymbolTableBuilder.AddAppearance(ari.Source, unit.ReferenceType, ctValue);
            //    }
            //}
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
            CurrentSymbolTableBuilder = CurrentSymbolTableBuilder.AddChild(scopeOwner);
        }
    }
}