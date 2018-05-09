using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class StoreToArray : IntermediateCode
    {
        public Reference Target { get; }
        public Reference Source { get; }
        public Reference Index { get; }

        public StoreToArray(Reference target, Reference index, Reference source) 
        {
            Target = target;
            Source = source;
            Index = index;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Target}[{Index}] = {Source}";
        }
    }
}