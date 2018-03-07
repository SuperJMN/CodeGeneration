using CodeGen.Units;

namespace CodeGen.Intermediate.Codes
{
    public class IntegerConstantAssignment : Assignment
    {
        public IntegerConstantAssignment(Reference target, int value) : base(target)
        {
            Value = value;
        }

        public int Value { get; }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Target} = {Value}";
        }
    }
}