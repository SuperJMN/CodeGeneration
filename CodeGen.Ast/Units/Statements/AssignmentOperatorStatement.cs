using CodeGen.Core;

namespace CodeGen.Ast.Units.Statements
{
    public class AssignmentOperatorStatement : Statement
    {
        public string Operator { get; }
        public Reference Reference { get; }

        public AssignmentOperatorStatement(string @operator, Reference reference)
        {
            Operator = @operator;
            Reference = reference;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}