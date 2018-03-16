using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;

namespace CodeGen.Ast.Units
{
    public interface ICodeVisitor
    {
        void Visit(ExpressionNode expressionNode);
        void Visit(ConstantExpression expression);
        void Visit(IfStatement expression);
        void Visit(Block block);
        void Visit(NewReferenceExpression expression);
        void Visit(AssignmentStatement expression);
    }
}