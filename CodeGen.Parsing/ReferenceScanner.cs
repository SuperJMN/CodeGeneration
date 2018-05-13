using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing
{
    public class ReferenceScanner : ICodeUnitVisitor
    {
        private readonly List<Reference> references = new List<Reference>();
        public IReadOnlyList<Reference> References => references.AsReadOnly();

        public void Visit(ExpressionNode expressionNode)
        {
            foreach (var op in expressionNode.Operands)
            {
                op.Accept(this);
            }

            AddReference(expressionNode.Reference);
        }

        public void Visit(ConstantExpression expression)
        {
            AddReference(expression.Reference);
        }

        private void AddReference(Reference reference)
        {
            references.Add(reference);
        }

        public void Visit(IfStatement expression)
        {
            expression.Condition.Accept(this);
            expression.Statement.Accept(this);
            expression.ElseStatement?.Accept(this);
        }

        public void Visit(AssignmentStatement expression)
        {
            expression.Assignment.Accept(this);
            expression.Target.Accept(this);

            AddReference(expression.Target.Reference);
        }

        public void Visit(ForLoop code)
        {
            code.Header.Initialization.Accept(this);
            code.Header.Condition.Accept(this);
            code.Header.Step.Accept(this);
            code.Statement.Accept(this);
        }

        public void Visit(WhileStatement unit)
        {
            unit.Condition.Accept(this);
            unit.Statement.Accept(this);
        }

        public void Visit(DoStatement unit)
        {
            unit.Condition.Accept(this);
            unit.Statement.Accept(this);
        }

        public void Visit(AssignmentOperatorStatement statement)
        {
            AddReference(statement.Target);
        }

        public void Visit(Function function)
        {
            foreach (var argument in function.Arguments)
            {
                argument.Accept(this);
            }

            function.Block.Accept(this);
        }

        public void Visit(Program program)
        {
            foreach (var func in program.Functions)
            {
                func.Accept(this);
            }
        }

        public void Visit(Call call)
        {
            foreach (var param in call.Parameters)
            {
                param.Accept(this);
            }

            AddReference(call.Reference);
        }

        public void Visit(ReturnStatement returnStatement)
        {
            returnStatement.Expression?.Accept(this);
        }

        public void Visit(Argument argument)
        {
            AddReference(argument.AccessItem.Reference);
        }

        public void Visit(DeclarationStatement unit)
        {
            unit.Initialization?.Accept(this);
            AddReference(unit.Reference);
        }

        public void Visit(ListInitialization unit)
        {
        }

        public void Visit(DirectInitialization unit)
        {
            unit.Expression.Accept(this);
        }

        public void Visit(ReferenceAccessItem unit)
        {
            unit.AccessExpression?.Accept(this);
            AddReference(unit.Reference);
        }        
    }
}