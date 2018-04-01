namespace CodeGen.Intermediate.Codes.Common
{
    public class BooleanValue
    {
        public int Value { get; }
        public string Name { get; set; }

        public static BooleanValue False = new BooleanValue(1, "false");
        public static BooleanValue True = new BooleanValue(0, "true");

        private BooleanValue(int value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}