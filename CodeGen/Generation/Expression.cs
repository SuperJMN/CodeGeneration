namespace CodeGen.Generation
{
    public abstract class Expression
    {
        protected Expression(Reference reference)
        {
            Reference = reference;
        }

        public Reference Reference { get; }
        public abstract Code Code { get; }
    }
}