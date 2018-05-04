namespace CodeGen.Parsing.Ast
{
    public abstract class InitializationExpression : ICodeUnit
    {
        public abstract void Accept(ICodeUnitVisitor unitVisitor);
    }
}