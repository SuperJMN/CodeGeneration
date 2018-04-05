namespace CodeGen.Parsing.Ast
{
    public interface ICodeUnit
    {
        void Accept(ICodeUnitVisitor unitVisitor);
    }
}