using CodeGen.Units.Expressions;
using CodeGen.Units.New.Expressions;

namespace CodeGen.Units
{
    public interface ICodeVisitor
    {
        void Visit(ExpressionNode expressionNode);
        void Visit(ConstantExpression expression);
        void Visit(New.Statements.IfStatement expression);
        void Visit(New.Statements.Block block);
        void Visit(NewReferenceExpression expression);
        void Visit(New.Statements.AssignmentStatement expression);
    }
}