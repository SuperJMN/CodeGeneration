namespace CodeGen.Generation
{
    public class MultExpression : Expression
    {
        private readonly Expression left;
        private readonly Expression right;

        public MultExpression(Expression left, Expression right) : base(new Reference())
        {
            this.left = left;
            this.right = right;
        }
        
        public override Code Code
        {
            get
            {
                var code = new Code();
                code.Add(left.Code);
                code.Add(right.Code);
                code.Add(new ThreeAddressCode(CodeType.Mult, Reference, left.Reference, right.Reference));
                return code;
            }
        }
    }
}