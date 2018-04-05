using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class ReturnCode : IntermediateCode
    {
        public Reference Reference { get; }

        public ReturnCode(Reference reference)
        {
            Reference = reference;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Return {Reference} to caller";
        }
    }
}