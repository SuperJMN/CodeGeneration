using CodeGen.Units;

namespace CodeGen.Intermediate
{
    public class Assignment : IntermediateCode
    {
        public Assignment(Reference target)
        {
            Target = target;
        }

        public Reference Target { get; set; }
    }
}