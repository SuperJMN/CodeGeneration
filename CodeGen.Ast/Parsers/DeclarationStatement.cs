using CodeGen.Ast.Units;
using CodeGen.Ast.Units.Statements;
using CodeGen.Core;

namespace CodeGen.Ast.Parsers
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