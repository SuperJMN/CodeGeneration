using CodeGen.Core;
using CodeGen.Intermediate.Codes.Common;

namespace CodeGen.Intermediate.Codes
{
    public class JumpIfFalse : IntermediateCode
    {
        public Reference Reference { get; }
        public Label Label { get; }

        public JumpIfFalse(Reference reference, Label label)
        {
            Reference = reference;
            Label = label;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"if {Reference} is {BooleanValue.False.Name} jump to {Label} ";
        }
    }
}