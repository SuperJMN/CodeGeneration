using CodeGen.Intermediate.Units.Expressions;
using CodeGen.Intermediate.Units.Sentences;

namespace CodeGen.Intermediate
{
    public interface ICodeVisitor
    {
        void Visit(AddExpression expression);
        void Visit(AssignmentSentence expression);
        void Visit(MultExpression expression);
        void Visit(ReferenceExpression expression);
    }
}