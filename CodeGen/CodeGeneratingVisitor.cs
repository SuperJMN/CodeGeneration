using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Expressions;

namespace CodeGen
{
    public class CodeGeneratingVisitor : IExpressionVisitor
    {
        public IReadOnlyCollection<ThreeAddressCode> Code => new ReadOnlyCollection<ThreeAddressCode>(InnerCode);

        private IList<ThreeAddressCode> InnerCode { get; } = new List<ThreeAddressCode>();

        public void Visit(AddExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            InnerCode.Add(new ThreeAddressCode(CodeType.Add, expression.Reference, expression.Left.Reference, expression.Right.Reference));
        }

        public void Visit()
        {            
        }

        public void Visit(AssignmentExpression expression)
        {
            expression.Assignment.Accept(this);

            InnerCode.Add(new ThreeAddressCode(CodeType.Move, expression.Reference, expression.Assignment.Reference, null));
        }

        public void Visit(MultExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            InnerCode.Add(new ThreeAddressCode(CodeType.Mult, expression.Reference, expression.Left.Reference, expression.Right.Reference));
        }

        public void Visit(ReferenceExpression expression)
        {            
        }
    }
}