using CodeGen.Core;

namespace CodeGen.Parsing.Ast.Statements
{
    public class DeclarationStatement : Statement
    {
        public VariableType Type { get; }
        public Reference Reference { get; }

        public DeclarationStatement(VariableType type, Reference reference)
        {
            Type = type;
            Reference = reference;
        }

        public override void Accept(ICodeVisitor visitor)
        {
        }
    }
}