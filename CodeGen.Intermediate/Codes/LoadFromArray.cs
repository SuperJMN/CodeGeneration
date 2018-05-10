using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class LoadFromArray : IntermediateCode
    {
        public Reference Target { get; }
        public IndexedReference Source { get; }

        public LoadFromArray(Reference target, IndexedReference source)
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