using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{   
    public class StoreToArray : IntermediateCode
    {
        public IndexedReference Target { get; }
        public Reference Source { get; }

        public StoreToArray(IndexedReference target, Reference source)
        {
            Target = target;
            Source = source;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Target} = {Source}";
        }
    }
}