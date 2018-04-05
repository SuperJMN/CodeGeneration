using CodeGen.Core;

namespace CodeGen.Parsing.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        public Reference Target { get; }

        public ReturnStatement(Reference target)
        {
            Target = target;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }

        public override string ToString()
        {
            if (Target == null)
            {
                return "return to caller";
            }

            return $"return {Target} to caller";
        }
    }
}