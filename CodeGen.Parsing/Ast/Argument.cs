namespace CodeGen.Parsing.Ast
{
    internal class Argument
    {
        public VariableType Type { get; }
        public string Name { get; }

        public Argument(VariableType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}