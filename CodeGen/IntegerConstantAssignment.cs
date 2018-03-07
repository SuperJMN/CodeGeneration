using CodeGen.Units;

namespace CodeGen.Intermediate
{
    public class IntegerConstantAssignment : Assignment
    {
        public IntegerConstantAssignment(Reference target, int value) : base(target)
        {
            Value = value;
        }

        public int Value { get; }
    }
}