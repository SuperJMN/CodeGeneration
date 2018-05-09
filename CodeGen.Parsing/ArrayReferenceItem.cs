using CodeGen.Core;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing
{
    public class ArrayReferenceItem : StandardReferenceItem
    {
        public Reference Source { get; }
        public Expression AccessExpression { get; }

        public ArrayReferenceItem(Reference source, Expression accessExpression) : base(new Reference())
        {
            Source = source;
            AccessExpression = accessExpression;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}