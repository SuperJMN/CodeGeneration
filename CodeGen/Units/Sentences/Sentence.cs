namespace CodeGen.Intermediate.Units.Sentences
{
    public abstract class Sentence : ICodeUnit
    {
        public abstract void Accept(ICodeVisitor visitor);
    }
}