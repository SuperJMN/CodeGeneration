using CodeGen.Expressions;

namespace CodeGen
{
    public interface IExpressionVisitor
    {
        void Visit(AddExpression expression);
        void Visit(AssignmentExpression expression);
        void Visit(MultExpression expression);
        void Visit(ReferenceExpression expression);
    }
}