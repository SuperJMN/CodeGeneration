namespace CodeGen.Units.Expressions
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

        public override void Accept(ICodeVisitor codeVisitor)
        {
            codeVisitor.Visit(this);
        }
    }
}