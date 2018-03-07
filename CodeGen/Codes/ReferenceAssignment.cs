using CodeGen.Units;

namespace CodeGen.Intermediate.Codes
{
    public class ReferenceAssignment : Assignment
    {
        public Reference Origin { get; }

        public ReferenceAssignment(Reference destination, Reference origin) : base(destination)
        {
            Origin = origin;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Target} = {Origin}";
        }
    }
}