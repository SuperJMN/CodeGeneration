namespace CodeGen.Intermediate.Codes
{
    public class HaltCode : IntermediateCode
    {
        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Halt";
        }
    }
}