using CodeGen.Core;

namespace CodeGen.Parsing.Ast
{
    public class Argument : ICodeUnit
    {
        public ReferenceType Type { get; }
        public Reference Reference { get; }

        public Argument(ReferenceType type, Reference reference)
        {
            Type = type;
            Reference = reference;
        }

        public void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}