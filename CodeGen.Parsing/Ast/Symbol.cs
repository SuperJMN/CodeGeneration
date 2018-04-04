namespace CodeGen.Parsing.Ast
{
    public class Symbol
    {
        public Symbol(int address)
        {
            Address = address;
            Type = VariableType.Int;
        }

        public int Address { get; set; }
        public VariableType Type { get; set; }
    }
}