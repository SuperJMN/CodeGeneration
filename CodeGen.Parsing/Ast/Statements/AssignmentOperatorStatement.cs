using CodeGen.Core;

namespace CodeGen.Parsing.Ast.Statements
{
    public class AssignmentOperatorStatement : Statement
    {
        public string Operator { get; }
        public Reference Target { get; }

        public AssignmentOperatorStatement(string @operator, Reference target)
        {
            Operator = @operator;
            Target = target;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}