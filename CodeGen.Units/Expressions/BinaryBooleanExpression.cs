namespace CodeGen.Units.Expressions
{
    public class BinaryBooleanExpression : BooleanExpression
    {
        public BinaryBooleanExpression(BooleanOperatorKind @operator, Expression left, Expression right) : base(
            new Reference())
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }

        public BooleanOperatorKind Operator { get; }
        public Expression Left { get; }
        public Expression Right { get; }


        public override void Accept(ICodeVisitor codeVisitor)
        {
            codeVisitor.Visit(this);
        }
    }
}