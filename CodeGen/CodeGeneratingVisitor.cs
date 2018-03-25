using System;
using System.Collections.Generic;
using System.Linq;
using CodeGen.Ast.Parsers;
using CodeGen.Ast.Units;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;
using CodeGen.Intermediate.Codes;
using CodeGen.Intermediate.Codes.Common;

namespace CodeGen.Intermediate
{
    public class CodeGeneratingVisitor : ICodeVisitor
    {
        public IReadOnlyCollection<IntermediateCode> Code => InnerCode.AsReadOnly();

        private List<IntermediateCode> InnerCode { get; } = new List<IntermediateCode>();

        public void Visit(ExpressionNode expressionNode)
        {
            expressionNode.Operands.ToList().ForEach(x => x.Accept(this));

            var destination = expressionNode.Reference;
            var left = expressionNode.Operands[0].Reference;
            var right = expressionNode.Operands[1].Reference;

            IntermediateCode emitted;

            if (IsBoolean(expressionNode))
            {
                emitted = new BoolExpressionAssignment(expressionNode.OperatorName.ToBooleanOperator(), destination, left, right);
            }
            else
            {
                emitted = new ArithmeticAssignment(expressionNode.OperatorName.ToArithmeticOperator(), destination, left, right);
            }
            
            InnerCode.Add(emitted);           
        }

        private static bool IsBoolean(ExpressionNode expressionNode)
        {
            var booleanOperartors = new[]
            {
                Operators.Eq,
                Operators.Lt, 
                Operators.Gt, 
                Operators.Gte, 
                Operators.Not, 
                Operators.Lte
            };

            return booleanOperartors.Contains(expressionNode.OperatorName);
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