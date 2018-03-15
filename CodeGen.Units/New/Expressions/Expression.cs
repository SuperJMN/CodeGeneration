namespace CodeGen.Units.New.Expressions
{
    public abstract class Expression : ICodeUnit
    {
        public abstract void Accept(ICodeVisitor visitor);
    }
}