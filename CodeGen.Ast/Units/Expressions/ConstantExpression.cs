namespace CodeGen.Ast.Units.Expressions
{
    public class ConstantExpression : Expression
    {
        public object Value { get; }

        public ConstantExpression(object value) : base(new Reference())
        {
            Value = value;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}