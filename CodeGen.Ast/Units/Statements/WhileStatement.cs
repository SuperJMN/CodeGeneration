using CodeGen.Ast.Units.Expressions;

namespace CodeGen.Ast.Units.Statements
{
    public class WhileStatement : Statement
    {
        public Expression Condition { get; }
        public Statement Statement { get; }

        public WhileStatement(Expression condition, Statement statement)
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