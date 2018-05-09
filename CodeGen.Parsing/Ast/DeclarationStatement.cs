using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class DeclarationStatement : Statement
    {
        public PrimitiveType ReferenceType { get; }
        public ReferenceItem ReferenceItem { get; }
        public InitializationExpression Initialization { get; }

        public DeclarationStatement(PrimitiveType referenceType, ReferenceItem referenceItem, InitializationExpression initialization = null)
        {
            ReferenceType = referenceType;
            ReferenceItem = referenceItem;
            Initialization = initialization;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}