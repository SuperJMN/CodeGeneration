using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast.Statements
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