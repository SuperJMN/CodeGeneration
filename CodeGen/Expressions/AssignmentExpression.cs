namespace CodeGen.Intermediate.Expressions
{
    public class AssignmentExpression : Expression
    {
        public AssignmentExpression(Reference target, Expression assignment) : base(target)
        {
            Assignment = assignment;
        }

        public Expression Assignment { get; }

        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }
    }
}