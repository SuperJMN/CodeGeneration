using CodeGen.Core;
using CodeGen.Intermediate.Codes.Common;

namespace CodeGen.Intermediate.Codes
{
    public class ArithmeticAssignment : Assignment
    {
        public ArithmeticAssignment(ArithmeticOperator operation, Reference target, Reference left, Reference right) : base(target)
        {
            Operation = operation;
            Left = left;
            Right = right;
        }

        public ArithmeticOperator Operation { get; }
        public Reference Left { get; }
        public Reference Right { get; }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Target} = {Left} {Operation.Symbol} {Right}";
        }
    }
}