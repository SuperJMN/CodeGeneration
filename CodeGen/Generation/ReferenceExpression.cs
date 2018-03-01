namespace CodeGen.Generation
{
    public class ReferenceExpression : Expression
    {
        public ReferenceExpression(Reference reference) : base(reference)
        {
        }

        public override Code Code => new Code();
    }
}