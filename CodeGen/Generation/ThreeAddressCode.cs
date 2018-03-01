namespace CodeGen.Generation
{
    public class ThreeAddressCode
    {
        private readonly CodeType instruction;
        private readonly Reference reference;
        private readonly Reference left;
        private readonly Reference right;

        public ThreeAddressCode(CodeType instruction, Reference reference, Reference left, Reference right)
        {
            this.instruction = instruction;
            this.left = left;
            this.right = right;
            this.reference = reference;
        }

        public Reference Right => right;

        public Reference Left => left;

        public Reference Reference => reference;

        public CodeType Instruction => instruction;

        public override string ToString()
        {
            return Instruction + " " +
                   Reference + " " +
                   Left + " " + Right;
        }
    }
}