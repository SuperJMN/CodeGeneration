using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Intermediate.Units.Expressions;
using CodeGen.Intermediate.Units.Statements;

namespace CodeGen.Intermediate
{
    public class ReferenceExtractorVisitor : ICodeVisitor
    {
        private readonly IList<Reference> references = new List<Reference>();
        public IEnumerable<Reference> References => new ReadOnlyCollection<Reference>(references);

        public void Visit(AddExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            references.Add(expression.Reference);            
        }

        public void Visit(AssignmentStatement statement)
        {
            statement.Assignment.Accept(this);

            references.Add(statement.Target);            
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

        public void Visit(IfStatement statement)
        {
            
        }

        public void Visit(Block block)
        {            
        }
    }
}