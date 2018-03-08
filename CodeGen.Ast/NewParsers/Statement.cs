using CodeGen.Units;

namespace CodeGen.Ast.NewParsers
{
    public abstract class Statement
    {
        public abstract void Accept(ICodeVisitor visitor);
    }
}