using CodeGen.Units;

namespace CodeGen.Intermediate
{
    public class GoToLabel : IntermediateCode
    {
        public Label Label { get; }

        public GoToLabel(Label label)
        {
            Label = label;
        }
    }
}