namespace CodeGen.Parsing.Ast
{
    public class Properties
    {
        public VariableType Type { get; private set; }

        public void AssignType(VariableType type)
        {
            if (Type == null)
            {
                Type = type;
            }
        }
    }
}