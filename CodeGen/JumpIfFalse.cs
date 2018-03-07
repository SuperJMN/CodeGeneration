using CodeGen.Units;

namespace CodeGen.Intermediate
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
    }
}