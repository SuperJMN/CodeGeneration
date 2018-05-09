using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class LoadFromArray : IntermediateCode
    {
        public Reference Target { get; }
        public Reference Source { get; }
        public Reference Index { get; }

        public LoadFromArray(Reference target, Reference source, Reference index) 
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
            return $"{Target} = {Source}[{Index}]";
        }
    }
}