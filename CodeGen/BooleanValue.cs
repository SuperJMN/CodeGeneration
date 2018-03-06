namespace CodeGen.Intermediate
{
    public class BooleanValue
    {
        public int Value { get; }
        public static BooleanValue False = new BooleanValue(1);
        public static BooleanValue True = new BooleanValue(0);

        private BooleanValue(int value)
        {
            Value = value;
        }
    }
}