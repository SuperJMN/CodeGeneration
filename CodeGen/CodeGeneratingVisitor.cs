using System;
using System.Collections.Generic;
using CodeGen.Intermediate.Codes;
using CodeGen.Units;
using CodeGen.Units.Expressions;
using CodeGen.Units.Statements;

namespace CodeGen.Intermediate
{
    public class CodeGeneratingVisitor : ICodeVisitor
    {
        public IReadOnlyCollection<IntermediateCode> Code => InnerCode.AsReadOnly();

        private List<IntermediateCode> InnerCode { get; } = new List<IntermediateCode>();

        public void Visit(ReferenceExpression expression)
        {
        }

        public void Visit(AssignmentStatement statement)
        {
            statement.Assignment.Accept(this);

            InnerCode.Add(IntermediateCode.Emit.Set(statement.Target, statement.Assignment.Reference));
        }

        public void Visit(IfStatement statement)
        {
            statement.Condition.Accept(this);
            var label = new Label();
            InnerCode.Add(IntermediateCode.Emit.JumpIfFalse(statement.Condition.Reference, label));
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

        public void Visit(OperatorExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            IntermediateCode emitted;

            switch (expression.Operator)
            {
                case OperatorKind.Add:
                    emitted = IntermediateCode.Emit.Add(expression.Reference, expression.Left.Reference,
                        expression.Right.Reference);

                    break;
                case OperatorKind.Mult:
                    emitted = IntermediateCode.Emit.Mult(expression.Reference, expression.Left.Reference,
                        expression.Right.Reference);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            InnerCode.Add(emitted);
        }

        public void Visit(ConstantExpression expression)
        {
            InnerCode.Add(IntermediateCode.Emit.Set(expression.Reference, expression.Value));
        }

        public void Visit(BinaryBooleanExpression booleanExpression)
        {
            booleanExpression.Left.Accept(this);
            booleanExpression.Right.Accept(this);

            switch (booleanExpression.Operator)
            {
                case BooleanOperatorKind.Equal:

                    InnerCode.Add(IntermediateCode.Emit.IsEqual(booleanExpression.Reference, booleanExpression.Left.Reference, booleanExpression.Right.Reference));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public void Visit(BooleanValueExpression booleanExpression)
        {
            InnerCode.Add(IntermediateCode.Emit.Set(booleanExpression.Reference, booleanExpression.Value));
        }
    }
}