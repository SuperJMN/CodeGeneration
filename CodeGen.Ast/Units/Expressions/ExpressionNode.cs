using CodeGen.Core;

namespace CodeGen.Ast.Units.Expressions
{
    public class ExpressionNode : Expression
    {
        public Expression[] Operands { get; }

        public ExpressionNode(string operatorName, params Expression[] operands) : this(operatorName)
        {
            Operands = operands;
        }

        public ExpressionNode(string operatorName) : base(new Reference())
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