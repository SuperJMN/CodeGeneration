namespace CodeGen.Intermediate.Codes.Common
{
    public class BooleanOperation
    {
        public int Value { get; }
        public string Symbol { get; }
        public string Description { get; }
        

        public static BooleanOperation IsEqual = new BooleanOperation(2, "==", "is equal to");
        public static BooleanOperation IsLessThan = new BooleanOperation(1, "<", "is less than");
        public static BooleanOperation IsGreaterThan = new BooleanOperation(1, ">", "is greater than");
        public static BooleanOperation IsGreaterOrEqual  = new BooleanOperation(1, ">=", "is greater than or equal to");
        public static BooleanOperation IsLessOrEqual  = new BooleanOperation(1, "<=", "is less than or equal to");
        

        private BooleanOperation(int value, string symbol, string description)
        {
            Value = value;
            Symbol = symbol;
            Description = description;
        }
    }
}