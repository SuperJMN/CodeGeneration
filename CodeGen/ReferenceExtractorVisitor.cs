using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Intermediate.Expressions;

namespace CodeGen.Intermediate
{
    public class ReferenceExtractorVisitor : IExpressionVisitor
    {
        private readonly IList<Reference> references = new List<Reference>();
        public IEnumerable<Reference> References => new ReadOnlyCollection<Reference>(references);

        public void Visit(AddExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            references.Add(expression.Reference);            
        }

        public void Visit(AssignmentExpression expression)
        {
            expression.Assignment.Accept(this);

            references.Add(expression.Reference);            
        }

        public void Visit(MultExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            references.Add(expression.Reference);
        }

        public void Visit(ReferenceExpression expression)
        {
            references.Add(expression.Reference);
        }
    }
}