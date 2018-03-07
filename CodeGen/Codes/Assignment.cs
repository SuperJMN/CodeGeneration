using CodeGen.Units;

namespace CodeGen.Intermediate.Codes
{
    public abstract class Assignment : IntermediateCode
    {
        public Assignment(Reference target)
        {
            Target = target;
        }

        public Reference Target { get; }       
    }
}