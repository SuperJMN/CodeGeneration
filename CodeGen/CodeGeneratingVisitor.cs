using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Intermediate.Expressions;

namespace CodeGen.Intermediate
{
    public class CodeGeneratingVisitor : IExpressionVisitor
    {
        public IReadOnlyCollection<IntermediateCode> Code => new ReadOnlyCollection<IntermediateCode>(InnerCode);

        private IList<IntermediateCode> InnerCode { get; } = new List<IntermediateCode>();

        public void Visit(AddExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            InnerCode.Add(new IntermediateCode(IntermediateCodeType.Add, expression.Reference, expression.Left.Reference, expression.Right.Reference));
        }

        public void Visit()
        {            
        }

        public void Visit(AssignmentExpression expression)
        {
            expression.Assignment.Accept(this);

            InnerCode.Add(new IntermediateCode(IntermediateCodeType.Move, expression.Reference, expression.Assignment.Reference, null));
        }

        public void Visit(MultExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            InnerCode.Add(new IntermediateCode(IntermediateCodeType.Mult, expression.Reference, expression.Left.Reference, expression.Right.Reference));
        }

        public void Visit(ReferenceExpression expression)
        {            
        }
    }
}