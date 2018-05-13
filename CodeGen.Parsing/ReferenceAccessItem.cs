using CodeGen.Core;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing
{
    public class ReferenceAccessItem : Expression
    {
        public int IndirectionLevel { get; }
        public Expression AccessExpression { get; }

        public ReferenceAccessItem(Reference reference, Expression accessExpression = null, int indirectionLevel = 0) : base(reference)
        {
            IndirectionLevel = indirectionLevel;
            AccessExpression = accessExpression;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }

        public static implicit operator ReferenceAccessItem(string str)
        {
            return new ReferenceAccessItem(str);
        }
    }
}