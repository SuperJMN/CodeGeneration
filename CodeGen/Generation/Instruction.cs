namespace CodeGen.Generation
{
    public class Instruction
    {
        private readonly string instruction;
        private readonly Reference reference;
        private readonly Reference left;
        private readonly Reference right;

        public Instruction(string instruction, Reference reference, Reference left, Reference right)
        {
            this.instruction = instruction;
            this.left = left;
            this.right = right;
            this.reference = reference;
        }


        public override string ToString()
        {
            return instruction + " " +
                   reference + " " +
                   left + " " + right;
        }
    }
}