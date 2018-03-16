using CodeGen.Ast.Units;

namespace CodeGen.Intermediate.Codes
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

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}