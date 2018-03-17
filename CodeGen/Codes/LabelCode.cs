namespace CodeGen.Intermediate.Codes
{
    public class LabelCode : IntermediateCode
    {
        public Label Label { get; }

        public LabelCode(Label label)
        {
            Label = label;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Label}";
        }
    }
}