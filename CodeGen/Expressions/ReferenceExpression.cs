namespace CodeGen.Intermediate.Expressions
{
    public class ReferenceExpression : Expression
    {
        public ReferenceExpression(Reference reference) : base(reference)
        {
        }

        public override void Accept(IExpressionVisitor expressionVisitor)
        {            
            expressionVisitor.Visit(this);
        }
    }
}