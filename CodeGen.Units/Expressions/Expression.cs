namespace CodeGen.Units.New.Expressions
{
    public abstract class Expression : ICodeUnit
    {
        public Reference Reference { get; }

        public Expression(Reference reference)
        {
            Reference = reference;
        }

        public abstract void Accept(ICodeVisitor visitor);
    }
}