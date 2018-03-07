namespace CodeGen.Intermediate.Codes.Common
{
    public class BooleanOperation
    {
        public int Value { get; }
        public string Symbol { get; }
        public string Description { get; }
        public static BooleanOperation IsEqual = new BooleanOperation(1, "==", "is equal to");

        private BooleanOperation(int value, string symbol, string description)
        {
            Value = value;
            Symbol = symbol;
            Description = description;
        }
    }
}