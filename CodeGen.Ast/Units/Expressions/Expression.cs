using CodeGen.Ast.Units.Statements;
using CodeGen.Core;

namespace CodeGen.Ast.Units.Expressions
{
    public abstract class Expression : Statement
    {
        public Reference Reference { get; }

        public Expression(Reference reference)
        {
            Reference = reference;
        }
    }
}