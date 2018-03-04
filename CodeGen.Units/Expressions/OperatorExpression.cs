namespace CodeGen.Units.Expressions
{
    public class OperatorExpression : Expression
    {
        public OperatorKind Operator { get; }
        public Expression Left { get; }
        public Expression Right { get; }

        public OperatorExpression(OperatorKind @operator, Expression left, Expression right) : base(new Reference())
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {
            codeVisitor.Visit(this);
        }
    }
}