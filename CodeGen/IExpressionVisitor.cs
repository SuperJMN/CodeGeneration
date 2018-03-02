using CodeGen.Intermediate.Expressions;

namespace CodeGen.Intermediate
{
    public interface IExpressionVisitor
    {
        void Visit(AddExpression expression);
        void Visit(AssignmentExpression expression);
        void Visit(MultExpression expression);
        void Visit(ReferenceExpression expression);
    }
}