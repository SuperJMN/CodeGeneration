using CodeGen.Core;

namespace CodeGen.Parsing.Ast
{
    public class Argument : ICodeUnit
    {
        public VariableType Type { get; }
        public Reference Reference { get; }

        public Argument(VariableType type, Reference reference)
        {
            Type = type;
            Reference = reference;
        }

        public void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}