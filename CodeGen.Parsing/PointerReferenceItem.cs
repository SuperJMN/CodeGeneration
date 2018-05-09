using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public class PointerReferenceItem : ReferenceItem
    {
        public ReferenceItem ReferenceItem { get; }

        public PointerReferenceItem(ReferenceItem referenceItem) : base(referenceItem.Reference)
        {
            ReferenceItem = referenceItem;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            throw new System.NotImplementedException();
        }
    }
}