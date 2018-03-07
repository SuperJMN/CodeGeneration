using CodeGen.Units;

namespace CodeGen.Intermediate
{
    public class ReferenceAssignment : Assignment
    {
        public Reference Origin { get; }

        public ReferenceAssignment(Reference destination, Reference origin) : base(destination)
        {
            Origin = origin;
        }
    }
}