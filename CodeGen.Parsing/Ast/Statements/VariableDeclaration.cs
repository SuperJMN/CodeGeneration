using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast.Statements
{
    public class VariableDeclaration : ICodeUnit
    {
        public VariableDeclaration(Reference reference, Expression initialization = null)
        {
            Reference = reference;
            Initialization = initialization;
        }

        public Expression Initialization { get; }

        public Reference Reference { get; }

        public void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}