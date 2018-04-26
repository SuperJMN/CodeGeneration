using CodeGen.Core;

namespace CodeGen.Parsing.Ast.Expressions
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
        public bool IsUnary => Operands.Length == 1;

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}