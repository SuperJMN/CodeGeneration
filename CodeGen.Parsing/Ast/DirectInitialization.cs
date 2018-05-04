using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast
{
    public class DirectInitialization : InitializationExpression
    {
        public Expression Expression { get; }

        public DirectInitialization(Expression  expression)
        {
            Expression = expression;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}