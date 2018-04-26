using CodeGen.Core;
using CodeGen.Intermediate.Codes;

namespace CodeGen.Intermediate
{
    public class AddressOf : IntermediateCode
    {
        public Reference Target { get; }
        public Reference Source { get; }

        public AddressOf(Reference target, Reference source)
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
            return $"{Target} = AddressOf({Source})";
        }
    }

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