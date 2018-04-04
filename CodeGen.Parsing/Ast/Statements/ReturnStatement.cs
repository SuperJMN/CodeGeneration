using CodeGen.Core;

namespace CodeGen.Parsing.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        public Reference Reference { get; }

        public ReturnStatement(Reference reference)
        {
            Reference = reference;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"return to caller";
        }
    }
}