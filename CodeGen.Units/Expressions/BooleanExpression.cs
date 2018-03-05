namespace CodeGen.Units.Expressions
{
    public class BooleanExpression : Expression
    {
        public BooleanOperatorKind BooleanOperatorKind { get; }
        public Expression Left { get; }
        public Expression Right { get; }

        public BooleanExpression(BooleanOperatorKind booleanOperatorKind, Expression left, Expression right) : base(new Reference())
        {
            BooleanOperatorKind = booleanOperatorKind;
            Left = left;
            Right = right;
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {
            codeVisitor.Visit(this);
        }
    }
}