using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast.Statements
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
        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}