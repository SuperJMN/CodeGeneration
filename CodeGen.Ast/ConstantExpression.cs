namespace CodeGen.Ast
{
    public class ConstantExpression : Expression
    {
        public int Value { get; }

        public ConstantExpression(int value)
        {
            Value = value;
        }
    }
}