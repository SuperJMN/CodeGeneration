namespace CodeGen.Units.New.Expressions
{
    public class NewConstantExpression : Expression
    {
        public object Value { get; }

        public NewConstantExpression(object value) : base(new Reference())
        {
            Value = value;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}