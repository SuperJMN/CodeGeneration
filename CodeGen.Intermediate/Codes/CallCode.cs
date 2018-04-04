using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class CallCode : IntermediateCode
    {
        public string FunctionName { get; }
        public Reference Reference { get; }

        public CallCode(string functionNameName, Reference reference) : this(functionNameName)
        {            
            Reference = reference;
        }

        public CallCode(string functionNameName)
        {
            FunctionName = functionNameName;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {            
        }

        public override string ToString()
        {
            if (Reference == null)
            {
                return $"Call to {FunctionName}()";
            }

            return $"Call to {FunctionName}() put result in {Reference}";
        }
    }
}