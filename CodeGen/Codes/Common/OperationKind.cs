namespace CodeGen.Intermediate.Codes.Common
{
    public class OperationKind
    {
        public int Value { get; }
        public string Symbol { get; }
        
        public static OperationKind Add = new OperationKind(1, "+");
        public static OperationKind Substract = new OperationKind(2, "-");
        public static OperationKind Mult = new OperationKind(3, "*");

        private OperationKind(int value, string symbol)
        {
            Value = value;
            Symbol = symbol;
        }
    }
}