namespace CodeGen.Units.Expressions
{
    public class ConstantExpression : Expression
    {
        public int Value { get; }

        public ConstantExpression(int value) : base(new Reference())
        {
            Value = value;
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {
            codeVisitor.Visit(this);
        }
    }
}