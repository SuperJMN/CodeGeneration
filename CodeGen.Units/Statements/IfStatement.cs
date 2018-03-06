using CodeGen.Units.Expressions;

namespace CodeGen.Units.Statements
{
    public class IfStatement : Statement
    {
        public BooleanExpression Condition { get; }
        public Block Block { get; }

        public IfStatement(BooleanExpression condition, Block block)
        {
            Condition = condition;
            Block = block;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}