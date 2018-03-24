namespace CodeGen.Intermediate.Codes
{
    public class Jump : IntermediateCode
    {
        public Label Label { get; }

        public Jump(Label label)
        {
            Label = label;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}