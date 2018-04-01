using CodeGen.Core;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast.Expressions
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