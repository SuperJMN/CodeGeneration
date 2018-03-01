namespace CodeGen.Generation
{
    public class AddExpression : Expression
    {
        private readonly Expression left;
        private readonly Expression right;

        public AddExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        public override Code Code
        {
            get
            {
                Reference = new Reference();

                var code = new Code();
                code.Add(left.Code);
                code.Add(right.Code);
                code.Add(new ThreeAddressInstruction(InstructionsTypes.Add, Reference, left.Reference, right.Reference));
                return code;
            }
        }
    }
}