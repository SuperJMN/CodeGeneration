using CodeGen.Units.Expressions;
using CodeGen.Units.Statements;

namespace CodeGen.Units
{
    public interface ICodeVisitor
    {
        void Visit(AddExpression expression);
        void Visit(AssignmentStatement statement);
        void Visit(MultExpression expression);
        void Visit(ReferenceExpression expression);
        void Visit(IfStatement statement);
        void Visit(Block block);
    }
}