using CodeGen.Ast.Parsers;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;

namespace CodeGen.Ast.Units
{
    public interface ICodeVisitor
    {
        void Visit(ExpressionNode expressionNode);
        void Visit(ConstantExpression expression);
        void Visit(IfStatement expression);
        void Visit(ReferenceExpression expression);
        void Visit(AssignmentStatement expression);
        void Visit(ForLoop code);
    }
}