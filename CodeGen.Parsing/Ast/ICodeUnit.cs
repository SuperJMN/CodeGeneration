namespace CodeGen.Parsing.Ast
{
    public interface ICodeUnit
    {
        void Accept(ICodeVisitor visitor);
    }
}