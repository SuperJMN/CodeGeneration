namespace CodeGen.Intermediate.Expressions
{
    public abstract class Expression
    {
        protected Expression(Reference reference)
        {
            Reference = reference;
        }

        public Reference Reference { get; }

        public abstract void Accept(IExpressionVisitor expressionVisitor);
    }
}