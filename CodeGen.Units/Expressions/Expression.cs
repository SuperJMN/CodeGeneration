namespace CodeGen.Units.Expressions
{
    public abstract class Expression : ICodeUnit
    {
        protected Expression(Reference reference)
        {
            Reference = reference;
        }

        public Reference Reference { get; }

        public abstract void Accept(ICodeVisitor codeVisitor);
    }
}