using System;
using System.Collections.Generic;
using System.Linq;
using CodeGen.Ast.Parsers;
using CodeGen.Ast.Units;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;
using CodeGen.Intermediate.Codes;

namespace CodeGen.Intermediate
{
    public class CodeGeneratingVisitor : ICodeVisitor
    {
        public IReadOnlyCollection<IntermediateCode> Code => InnerCode.AsReadOnly();

        private List<IntermediateCode> InnerCode { get; } = new List<IntermediateCode>();

        public void Visit(ExpressionNode expressionNode)
        {
            expressionNode.Operands.ToList().ForEach(x => x.Accept(this));

            IntermediateCode emitted;

            switch (expressionNode.OperatorName)
            {
                case nameof(Operators.Add):
                    emitted = IntermediateCode.Emit.Add(expressionNode.Reference, expressionNode.Operands[0].Reference,
                        expressionNode.Operands[1].Reference);
                    break;

                case nameof(Operators.Subtract):
                    emitted = IntermediateCode.Emit.Substract(expressionNode.Reference, expressionNode.Operands[0].Reference,
                        expressionNode.Operands[1].Reference);

                    break;
                case nameof(Operators.Multiply):
                    emitted = IntermediateCode.Emit.Mult(expressionNode.Reference, expressionNode.Operands[0].Reference,
                        expressionNode.Operands[1].Reference);
                    break;

                case nameof(Operators.Eq):
                    emitted = IntermediateCode.Emit.IsEqual(expressionNode.Reference, expressionNode.Operands[0].Reference,
                        expressionNode.Operands[1].Reference);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            InnerCode.Add(emitted);
        }

        public void Visit(ConstantExpression expression)
        {
            switch (expression.Value)
            {
                case int t:
                    InnerCode.Add(IntermediateCode.Emit.Set(expression.Reference, t));
                    break;
                case bool b:
                    InnerCode.Add(IntermediateCode.Emit.Set(expression.Reference, b));
                    break;
            }
        }

        public void Visit(IfStatement statement)
        {
            statement.Condition.Accept(this);
            var label = new Label();
            InnerCode.Add(IntermediateCode.Emit.JumpIfFalse(statement.Condition.Reference, label));
            statement.Statement.Accept(this);
            InnerCode.Add(IntermediateCode.Emit.Label(label));
        }

        public void Visit(ForLoop forLoop)
        {
            var exitLoopLabel = new Label();
            var continueLoopLabel = new Label();

            forLoop.Initialization.Accept(this);
            InnerCode.Add(IntermediateCode.Emit.Label(continueLoopLabel));
            InnerCode.Add(IntermediateCode.Emit.JumpIfFalse(forLoop.Condition.Reference, exitLoopLabel));
            forLoop.Statement.Accept(this);
            forLoop.Step.Accept(this);
            InnerCode.Add(new Jump(continueLoopLabel));
            InnerCode.Add(IntermediateCode.Emit.Label(exitLoopLabel));
        }

        public void Visit(ReferenceExpression expression)
        {
        }

        public void Visit(AssignmentStatement statement)
        {
            statement.Assignment.Accept(this);

            InnerCode.Add(IntermediateCode.Emit.Set(statement.Target, statement.Assignment.Reference));
        }
    }

    
}