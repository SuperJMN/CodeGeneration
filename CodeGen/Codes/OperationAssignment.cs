using CodeGen.Ast.Units;
using CodeGen.Intermediate.Codes.Common;

namespace CodeGen.Intermediate.Codes
{
    public class OperationAssignment : Assignment
    {
        public OperationAssignment(OperationKind operation, Reference target, Reference left, Reference right) : base(target)
        {
            Operation = operation;
            Left = left;
            Right = right;
        }

        public OperationKind Operation { get; }
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