using CodeGen.Ast.Units.Expressions;

namespace CodeGen.Ast.Units.Statements
{
    public class IfStatement : Statement
    {
        public Expression Condition { get; }
        public Statement Statement { get; }

        public IfStatement(Expression condition, Statement statement)
        {
            Condition = condition;
            Statement = statement;
        }
        
        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}