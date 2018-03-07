using CodeGen.Intermediate.Codes.Common;
using CodeGen.Units;

namespace CodeGen.Intermediate.Codes
{
    public class BoolExpressionAssignment : Assignment
    {
        public BooleanOperation Operation { get; }
        public Reference Left { get; }
        public Reference Right { get; }

        public BoolExpressionAssignment(BooleanOperation operation, Reference destination, Reference left,
            Reference right) : base(destination)
        {
            Operation = operation;
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            return $"{Target} = {Left} {Operation.Symbol} {Right}";
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}