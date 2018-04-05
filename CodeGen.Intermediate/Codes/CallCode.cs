using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class CallCode : IntermediateCode
    {
        public string FunctionName { get; }
        public Reference Reference { get; }

        public CallCode(string functionName, Reference reference) : this(functionName)
        {
            Reference = reference;
        }

        public CallCode(string functionName)
        {
            FunctionName = functionName;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
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