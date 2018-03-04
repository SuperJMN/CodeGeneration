namespace CodeGen.Units
{
    public interface ICodeUnit
    {
        void Accept(ICodeVisitor visitor);
    }
}