namespace CodeGen.Intermediate.Codes.Common
{
    public class ArithmeticOperator
    {
        public int Value { get; }
        public string Symbol { get; }
        
        public static ArithmeticOperator Add = new ArithmeticOperator(1, "+");
        public static ArithmeticOperator Substract = new ArithmeticOperator(2, "-");
        public static ArithmeticOperator Mult = new ArithmeticOperator(3, "*");

        private ArithmeticOperator(int value, string symbol)
        {
            Value = value;
            Symbol = symbol;
        }
    }
}