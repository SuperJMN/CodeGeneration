namespace CodeGen.Parsing.Ast
{
    public class Properties
    {
        public ReturnType Type { get; set; }

        public int Size { get; set; } = 1;
        public int Offset { get; set; }

        public void AssignType(ReturnType type)
        {
            Type = type;
        }

        public void AssignLength(int arrayLength)
        {
            Size = arrayLength;
        }
    }
}