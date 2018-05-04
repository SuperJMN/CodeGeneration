using CodeGen.Core;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class DeclarationStatement : Statement
    {
        public ReferenceType ReferenceType { get; }
        public Reference Identifier { get; }
        public InitializationExpression Initialization { get; }
        public int? ArrayLenght { get; }

        public DeclarationStatement(ReferenceType referenceType, Reference identifier, InitializationExpression initialization = null, int? arrayLenght = null)
        {
            ReferenceType = referenceType;
            Identifier = identifier;
            Initialization = initialization;
            ArrayLenght = arrayLenght;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}