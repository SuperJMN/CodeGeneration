namespace CodeGen.Generation
{
    public abstract class Expression
    {
        public Reference Reference { get; protected set; }
        public abstract Code Code { get; }
    }
}