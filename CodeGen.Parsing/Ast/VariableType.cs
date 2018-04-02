namespace CodeGen.Parsing.Ast
{
    public class VariableType
    {
        public static readonly VariableType Int = new VariableType();
        public static readonly VariableType Char = new VariableType();
        public static VariableType Void = new VariableType();
    }
}