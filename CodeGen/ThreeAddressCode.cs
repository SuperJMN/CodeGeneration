namespace CodeGen
{
    public class ThreeAddressCode
    {
        public ThreeAddressCode(CodeType instruction, Reference reference, Reference left, Reference right)
        {
            this.Instruction = instruction;
            this.Left = left;
            this.Right = right;
            this.Reference = reference;
        }

        public Reference Right { get; }

        public Reference Left { get; }

        public Reference Reference { get; }

        public CodeType Instruction { get; }

        public override string ToString()
        {
            return Instruction + " " +
                   Reference + " " +
                   Left + " " + Right;
        }
    }
}