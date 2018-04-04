namespace CodeGen.Intermediate.Codes
{
    public class IntParameterCode : IntermediateCode
    {
        public int Value { get; }

        public IntParameterCode(int value)
        {
            Value = value;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {            
        }

        public override string ToString()
        {
            return $"Parameter: {Value}";
        }
    }
}