using CodeGen.Units;

namespace CodeGen.Intermediate
{
    public class BoolConstantAssignment : Assignment
    {
        public BoolConstantAssignment(Reference target, bool value) : base(target)
        {
            Value = value;
        }

        public bool Value { get; }

        public override string ToString()
        {
            return $"{Target} = {Value}";
        }
    }
}