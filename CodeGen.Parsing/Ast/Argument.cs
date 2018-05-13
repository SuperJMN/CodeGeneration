namespace CodeGen.Parsing.Ast
{
    public class Argument : ICodeUnit
    {
        public PrimitiveType Type { get; }
        public ReferenceAccessItem AccessItem { get; }

        public Argument(PrimitiveType type, ReferenceAccessItem accessItem)
        {
            Type = type;
            AccessItem = accessItem;
        }

        public void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}