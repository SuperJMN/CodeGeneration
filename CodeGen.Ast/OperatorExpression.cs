namespace CodeGen.Ast
{
    public class OperatorExpression : Expression
    {
        public OperatorKind Operator { get; }
        public Expression Left { get; }
        public Expression Right { get; }

        public OperatorExpression(OperatorKind @operator, Expression left, Expression right)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }
    }
}