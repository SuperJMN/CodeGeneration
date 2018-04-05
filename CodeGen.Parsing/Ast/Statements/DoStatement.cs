using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast.Statements
{
    public class DoStatement : Statement
    {
        public Expression Condition { get; }
        public Statement Statement { get; }

        public DoStatement(Expression condition, Statement statement)
        {
            Condition = condition;
            Statement = statement;
        }
        
        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}