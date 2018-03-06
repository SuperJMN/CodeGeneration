namespace CodeGen.Units.Expressions
{
    public class BooleanValueExpression : BooleanExpression
    {
        public bool Value { get; }

        public BooleanValueExpression(bool value) : base(new Reference())
        {
            Value = value;
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {
            codeVisitor.Visit(this);
        }
    }
}