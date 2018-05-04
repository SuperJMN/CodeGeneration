using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing
{
    public class DeclStatement : Statement
    {
        public PrimitiveType Type { get; }
        public DeclaratorAndInitializer[] DeclarationsDeclaratorAndInitializers { get; }

        public DeclStatement(PrimitiveType type, params DeclaratorAndInitializer[] declarationsDeclaratorAndInitializers)
        {
            Type = type;
            DeclarationsDeclaratorAndInitializers = declarationsDeclaratorAndInitializers;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}