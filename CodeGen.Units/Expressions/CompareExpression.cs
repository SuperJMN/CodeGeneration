namespace CodeGen.Units.Expressions
{
    public class CompareExpression : Expression
    {
        public Expression Left { get; }
        public Expression Right { get; }

        public CompareExpression(Expression left, Expression right) : base(new Reference())
        {
            Left = left;
            Right = right;
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {
            codeVisitor.Visit(this);
        }
    }
}