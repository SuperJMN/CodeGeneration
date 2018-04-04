namespace CodeGen.Parsing.Ast
{
    public class VariableType
    {
        public static readonly VariableType Int = new VariableType("int");
        public static readonly VariableType Char = new VariableType("char");
        public static readonly VariableType Void = new VariableType("void");

        private VariableType(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }

        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}