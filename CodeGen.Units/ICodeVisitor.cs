using CodeGen.Units.Expressions;
using CodeGen.Units.New.Expressions;
using CodeGen.Units.Statements;

namespace CodeGen.Units
{
    public interface ICodeVisitor
    {
        void Visit(AssignmentStatement statement);
        void Visit(ReferenceExpression expression);
        void Visit(IfStatement statement);
        void Visit(Block block);
        void Visit(OperatorExpression expression);
        void Visit(ConstantExpression expression);
        void Visit(BinaryBooleanExpression expression);
        void Visit(BooleanValueExpression expression);
        void Visit(CallExpression expression);
        void Visit(NewConstantExpression expression);
        void Visit(New.Statements.IfStatement expression);
        void Visit(New.Statements.Block block);
        void Visit(NewReferenceExpression expression);
        void Visit(New.Statements.AssignmentStatement expression);
    }
}