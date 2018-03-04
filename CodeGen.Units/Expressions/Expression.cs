namespace CodeGen.Units.Expressions
{
    public abstract class Expression
    {
        protected Expression(Reference reference)
        {
            Reference = reference;
        }

        public Reference Reference { get; }

        public abstract void Accept(ICodeVisitor codeVisitor);
    }

    public enum OperatorKind
    {
        Add,
        Mult,
        Div,
        Subs,
    }
}