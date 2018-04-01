namespace CodeGen.Parsing.Ast.Statements
{
    public abstract class Statement : ICodeUnit
    {
        public abstract void Accept(ICodeVisitor visitor);
    }
}