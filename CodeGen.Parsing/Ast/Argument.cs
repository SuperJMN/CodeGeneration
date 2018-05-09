namespace CodeGen.Parsing.Ast
{
    public class Argument : ICodeUnit
    {
        public PrimitiveType Type { get; }
        public ReferenceItem Item { get; }

        public Argument(PrimitiveType type, ReferenceItem item)
        {
            Type = type;
            Item = item;
        }

        public void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}