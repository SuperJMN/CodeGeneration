using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class ContentOf : IntermediateCode
    {
        public Reference Target { get; }
        public Reference Source { get; }

        public ContentOf(Reference target, Reference source)
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
            return $"{Target} = ContentOf({Source})";
        }
    }
}