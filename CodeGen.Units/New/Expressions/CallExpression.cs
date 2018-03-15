namespace CodeGen.Units.New.Expressions
{
    public class CallExpression : Expression
    {
        public Expression[] Operands { get; }

        public CallExpression(string operatorName, params Expression[] operands) : this(operatorName)
        {
            Operands = operands;
        }

        public CallExpression(string operatorName)
        {   
            OperatorName = operatorName;
        }

        public string OperatorName { get; }
        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}