using CodeGen.Core;
using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public class StandardReferenceItem : ReferenceItem
    {
        public StandardReferenceItem(Reference reference) : base(reference)
        {
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}