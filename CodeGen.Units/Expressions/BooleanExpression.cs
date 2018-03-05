namespace CodeGen.Units.Expressions
{
    public class BooleanExpression : Expression
    {
        private BooleanExpression() : base(new Reference())
        {
        }

        public BooleanOperatorKind BooleanOperatorKind { get; private set; }
        public Expression Left { get; private set; }
        public Expression Right { get; private set; }

        public bool Value { get; private set; }

        public static BooleanExpression Binary(BooleanOperatorKind booleanOperatorKind, Expression left,
            Expression right)
        {
            return new BooleanExpression
            {
                BooleanOperatorKind = booleanOperatorKind,
                Left = left,
                Right = right
            };
        }

        public static BooleanExpression BoolValue(bool value)
        {
            return new BooleanExpression
            {
                Value = value
            };
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {
            codeVisitor.Visit(this);
        }
    }
}