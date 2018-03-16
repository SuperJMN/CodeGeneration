namespace CodeGen.Ast.Units
{
    public interface ICodeUnit
    {
        void Accept(ICodeVisitor visitor);
    }
}