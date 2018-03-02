using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Intermediate.Units.Expressions;
using CodeGen.Intermediate.Units.Statements;

namespace CodeGen.Intermediate
{
    public class CodeGeneratingVisitor : ICodeVisitor
    {
        public IReadOnlyCollection<IntermediateCode> Code => new ReadOnlyCollection<IntermediateCode>(InnerCode);

        private IList<IntermediateCode> InnerCode { get; } = new List<IntermediateCode>();

        public void Visit(ReferenceExpression expression)
        {            
        }

        public void Visit(AddExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            InnerCode.Add(IntermediateCode.Emit.Add(expression.Reference, expression.Left.Reference, expression.Right.Reference));
        }

        public void Visit(AssignmentStatement statement)
        {
            statement.Assignment.Accept(this);

            InnerCode.Add(IntermediateCode.Emit.DirectAssignment(statement.Target, statement.Assignment.Reference));
        }

        public void Visit(MultExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            InnerCode.Add(IntermediateCode.Emit.Mult(expression.Reference, expression.Left.Reference, expression.Right.Reference));
        }

        public void Visit(IfStatement statement)
        {
            statement.Accept(this);
            var label = new Label();
            InnerCode.Add(IntermediateCode.Emit.JumpIfZero(statement.Condition.Reference, label));
            statement.Block.Accept(this);
            InnerCode.Add(IntermediateCode.Emit.Label(label));
        }

        public void Visit(Block block)
        {
            foreach (var line in block)
            {
                line.Accept(this);
            }
        }
    }
}