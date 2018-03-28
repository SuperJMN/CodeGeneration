using CodeGen.Ast.Units.Expressions;

namespace CodeGen.Ast.Units.Statements
{
    public class IfStatement : Statement
    {
        public Expression Condition { get; }
        public Statement Statement { get; }
        public Statement ElseStatement { get; }

        public IfStatement(Expression condition, Statement statement, Statement elseStatement = null)
        {
            Condition = condition;
            Statement = statement;
            ElseStatement = elseStatement;
        }
        
        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}