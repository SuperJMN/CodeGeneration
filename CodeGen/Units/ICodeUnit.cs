namespace CodeGen.Intermediate.Units
{
    public interface ICodeUnit
    {
        void Accept(ICodeVisitor visitor);
    }
}