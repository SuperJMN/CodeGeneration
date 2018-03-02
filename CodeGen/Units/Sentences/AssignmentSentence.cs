using CodeGen.Intermediate.Units.Expressions;

namespace CodeGen.Intermediate.Units.Sentences
{
    public class AssignmentSentence : Sentence
    {
        public AssignmentSentence(Reference target, Expression assignment)
        {
            Target = target;
            Assignment = assignment;
        }

        public Reference Target { get; }
        public Expression Assignment { get; }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}