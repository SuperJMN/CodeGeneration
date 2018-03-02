using CodeGen.Intermediate.Units.Expressions;
using CodeGen.Intermediate.Units.Statements;

namespace CodeGen.Intermediate
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