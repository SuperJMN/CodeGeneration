namespace CodeGen.Generation
{
    public class ReferenceExpression : Expression
    {
        public ReferenceExpression(Reference reference)
        {
            Reference = reference;
        }

        public override Code Code => new Code();
    }
}