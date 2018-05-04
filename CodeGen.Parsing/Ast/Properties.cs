namespace CodeGen.Parsing.Ast
{
    public class Properties
    {
        public ReferenceType Type { get; private set; }

        public void AssignType(ReferenceType type)
        {
            if (Type == null)
            {
                Type = type;
            }
        }
    }
}