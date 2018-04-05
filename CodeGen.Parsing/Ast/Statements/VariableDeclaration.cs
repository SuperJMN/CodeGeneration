using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast.Statements
{
    public class VariableDeclaration : ICodeUnit
    {
        public VariableDeclaration(AssignmentStatement assignmentStatement)
        {
            Reference = assignmentStatement.Target;
            Initialization = assignmentStatement.Assignment;
        }

        public Expression Initialization { get; }

        public VariableDeclaration(Reference reference)
        {
            Reference = reference;
        }

        public Reference Reference { get; }
        public void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}