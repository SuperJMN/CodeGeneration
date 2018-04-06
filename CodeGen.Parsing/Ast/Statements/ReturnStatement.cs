using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression Expression { get; }

        public ReturnStatement(Expression expression)
        {
            Expression = expression;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }

        public override string ToString()
        {
            if (Expression == null)
            {
                return "return to caller";
            }

            return $"return {Expression} to caller";
        }
    }
}