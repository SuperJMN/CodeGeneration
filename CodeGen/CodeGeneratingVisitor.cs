using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Intermediate.Units.Expressions;
using CodeGen.Intermediate.Units.Sentences;

namespace CodeGen.Intermediate
{
    public class CodeGeneratingVisitor : ICodeVisitor
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

        public void Visit(AssignmentSentence expression)
        {
            expression.Assignment.Accept(this);

            InnerCode.Add(new IntermediateCode(IntermediateCodeType.Move, expression.Target, expression.Assignment.Reference, null));
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