namespace CodeGen.Intermediate
{
    public class OperationKind
    {
        public int Value { get; }
        public string Symbol { get; }
        public static OperationKind Add = new OperationKind(1, "+");
        public static OperationKind Mult = new OperationKind(2, "*");

        private OperationKind(int value, string symbol)
        {
            Value = value;
            Symbol = symbol;
        }
    }
}