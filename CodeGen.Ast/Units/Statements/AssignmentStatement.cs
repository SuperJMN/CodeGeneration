using CodeGen.Ast.Units.Expressions;

namespace CodeGen.Ast.Units.Statements
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
            visitor.Visit(this);
        }
    }
}