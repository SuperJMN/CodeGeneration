using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Intermediate.Codes;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Intermediate
{
    public class CodeUnitGeneratingVisitor : ICodeUnitVisitor
    {
        public IEnumerable<IntermediateCode> Code => InnerCode.AsReadOnly();

        private List<IntermediateCode> InnerCode { get; } = new List<IntermediateCode>();

        public void Visit(ExpressionNode expressionNode)
        {
            foreach (var x in expressionNode.Operands)
            {
                x.Accept(this);
            }

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
                Operator.Eq,
                Operator.Lt,
                Operator.Gt,
                Operator.Gte,
                Operator.Not,
                Operator.Lte
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
            if (statement.ElseStatement == null)
            {
                statement.Condition.Accept(this);
                var label = new Label();
                InnerCode.Add(IntermediateCode.Emit.JumpIfFalse(statement.Condition.Reference, label));
                statement.Statement.Accept(this);
                InnerCode.Add(IntermediateCode.Emit.Label(label));
            }
            else
            {
                statement.Condition.Accept(this);
                var elseLabel = new Label();
                var endLabel = new Label();
                InnerCode.Add(IntermediateCode.Emit.JumpIfFalse(statement.Condition.Reference, elseLabel));
                statement.Statement.Accept(this);
                InnerCode.Add(new Jump(endLabel));
                InnerCode.Add(IntermediateCode.Emit.Label(elseLabel));
                statement.ElseStatement.Accept(this);
                InnerCode.Add(IntermediateCode.Emit.Label(endLabel));
            }
        }

        public void Visit(ForLoop forLoop)
        {
            var exitLoopLabel = new Label();
            var continueLoopLabel = new Label();

            forLoop.Header.Initialization.Accept(this);
            InnerCode.Add(IntermediateCode.Emit.Label(continueLoopLabel));
            forLoop.Header.Condition.Accept(this);
            InnerCode.Add(IntermediateCode.Emit.JumpIfFalse(forLoop.Header.Condition.Reference, exitLoopLabel));
            forLoop.Statement.Accept(this);
            forLoop.Header.Step.Accept(this);
            InnerCode.Add(new Jump(continueLoopLabel));
            InnerCode.Add(IntermediateCode.Emit.Label(exitLoopLabel));
        }

        public void Visit(WhileStatement whileStatement)
        {
            var exitLabel = new Label();
            var continueLabel = new Label();

            InnerCode.Add(IntermediateCode.Emit.Label(continueLabel));
            whileStatement.Condition.Accept(this);
            InnerCode.Add(IntermediateCode.Emit.JumpIfFalse(whileStatement.Condition.Reference, exitLabel));
            whileStatement.Statement.Accept(this);
            InnerCode.Add(new Jump(continueLabel));
            InnerCode.Add(IntermediateCode.Emit.Label(exitLabel));
        }

        public void Visit(DoStatement doStatement)
        {
            var exitLabel = new Label();
            var continueLabel = new Label();

            InnerCode.Add(IntermediateCode.Emit.Label(continueLabel));
            doStatement.Statement.Accept(this);
            doStatement.Condition.Accept(this);
            InnerCode.Add(IntermediateCode.Emit.JumpIfFalse(doStatement.Condition.Reference, exitLabel));
            InnerCode.Add(new Jump(continueLabel));
            InnerCode.Add(IntermediateCode.Emit.Label(exitLabel));
        }

        public void Visit(AssignmentOperatorStatement statement)
        {
        }

        public void Visit(Function function)
        {
            InnerCode.Add(IntermediateCode.Emit.FunctionDefinition(function));


            foreach (var decl in function.Block.Declarations)
            {
                decl.Accept(this);
            }

            foreach (var st in function.Block.Statements)
            {
                st.Accept(this);
            }
        }

        public void Visit(DeclarationStatement expressionNode)
        {
            foreach (var decl in expressionNode.Declarations)
            {
                decl.Accept(this);
            }
        }

        public void Visit(VariableDeclaration expressionNode)
        {
            if (expressionNode.Initialization == null)
            {
                return;
            }

            var assignment = new AssignmentStatement(expressionNode.Reference, expressionNode.Initialization);
            assignment.Accept(this);
        }

        public void Visit(Program program)
        {
            var main = program.Functions.Single(x => x.Name == "main");
            InnerCode.Add(IntermediateCode.Emit.Call(main.Name));

            foreach (var unit in program.Functions)
            {
                unit.Accept(this);
            }
        }

        public void Visit(Call call)
        {
            foreach (var parameter in call.Parameters)
            {                
                parameter.Accept(this);
                InnerCode.Add(IntermediateCode.Emit.Parameter(parameter.Reference));
            }

            InnerCode.Add(IntermediateCode.Emit.Call(call.FunctionName, call.Reference));

            if (call.Target != null)
            {
                InnerCode.Add(IntermediateCode.Emit.Set(call.Target, call.Reference));
            }
        }

        public void Visit(ReturnStatement returnStatement)
        {
            InnerCode.Add(IntermediateCode.Emit.Return(returnStatement.Target));
        }

        public void Visit(Argument argument)
        {          
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