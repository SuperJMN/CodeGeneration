namespace CodeGen.Units.New.Statements
{
    public abstract class Statement : ICodeUnit
    {
        public abstract void Accept(ICodeVisitor visitor);
    }
}