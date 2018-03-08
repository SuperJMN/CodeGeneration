
using CodeGen.Units;

namespace CodeGen.Ast.NewParsers
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