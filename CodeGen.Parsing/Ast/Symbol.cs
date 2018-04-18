namespace CodeGen.Parsing.Ast
{
    public class Symbol
    {
        public Symbol()
        {
            Type = VariableType.Int;
        }

        public VariableType Type { get; }
    }
}