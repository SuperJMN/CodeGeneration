using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast.Statements
{
    public class AssignmentStatement : Statement
    {
        public AssignmentStatement(ReferenceAccessItem target, Expression assignment)
        {
            Target = target;
            Assignment = assignment;
        }

        public ReferenceAccessItem Target { get; }
        public Expression Assignment { get; }
        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}