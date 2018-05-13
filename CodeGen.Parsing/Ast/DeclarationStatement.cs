using CodeGen.Core;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class DeclarationStatement : Statement
    {
        public ReturnType ReferenceType { get; }
        public Reference Reference { get; }
        public InitializationExpression Initialization { get; }

        public DeclarationStatement(ReturnType referenceType, Reference reference, InitializationExpression initialization = null)
        {
            ReferenceType = referenceType;
            Reference = reference;
            Initialization = initialization;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}