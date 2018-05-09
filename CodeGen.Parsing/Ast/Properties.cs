namespace CodeGen.Parsing.Ast
{
    public class Properties
    {
        public PrimitiveType Type { get; private set; }

        public int Size { get; private set; } = 1;
        public int Offset { get; set; }

        public void AssignType(PrimitiveType type)
        {
            Type = type;
        }

        public void AssignLength(int arrayLength)
        {
            Size = arrayLength;
        }
    }
}