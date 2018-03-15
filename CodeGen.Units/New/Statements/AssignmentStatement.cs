using CodeGen.Units.New.Expressions;

namespace CodeGen.Units.New.Statements
{
    public class AssignmentStatement : Statement
    {
        public AssignmentStatement(Reference target, Expression assignment)
        {
            Target = target;
            Assignment = assignment;
        }

        public Reference Target { get; }
        public Expression Assignment { get; }
        public override void Accept(ICodeVisitor visitor)
        {
        }
    }
}