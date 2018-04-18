using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class ReferenceParameterCode : IntermediateCode
    {
        public Reference Reference { get; }

        public ReferenceParameterCode(Reference reference)
        {
            Reference = reference;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {            
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Parameter: {Reference}";
        }
    }
}