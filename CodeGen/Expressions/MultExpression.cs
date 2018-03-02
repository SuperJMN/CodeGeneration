namespace CodeGen.Expressions
{
    public class MultExpression : Expression
    {
        public MultExpression(Expression left, Expression right) : base(new Reference())
        {
            this.Left = left;
            this.Right = right;
        }

        public Expression Left { get; }

        public Expression Right { get; }

        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }
    }
}