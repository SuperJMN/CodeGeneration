namespace CodeGen.Ast.NewParsers
{
    public class NewConstantExpression : Expression
    {
        public object Value { get; }

        public NewConstantExpression(object value)
        {
            Value = value;
        }
    }
}