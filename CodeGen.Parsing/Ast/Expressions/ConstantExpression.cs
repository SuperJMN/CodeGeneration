using CodeGen.Core;

namespace CodeGen.Parsing.Ast.Expressions
{
    public class ConstantExpression : Expression
    {
        public object Value { get; }

        public ConstantExpression(object value) : base(new Reference())
        {
            Value = value;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}