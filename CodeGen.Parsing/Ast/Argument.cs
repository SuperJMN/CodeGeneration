namespace CodeGen.Parsing.Ast
{
    public class Argument : ICodeUnit
    {
        public ReturnType Type { get; }
        public ReferenceAccessItem AccessItem { get; }

        public Argument(ReturnType type, ReferenceAccessItem accessItem)
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