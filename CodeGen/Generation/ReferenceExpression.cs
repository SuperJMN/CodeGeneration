namespace CodeGen.Generation
{
    public class ReferenceExpression : Expression
    {
        public ReferenceExpression(Reference r)
        {
            Reference = r;
        }

        public override Code Code => new Code();
    }
}