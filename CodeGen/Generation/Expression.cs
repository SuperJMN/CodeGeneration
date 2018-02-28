namespace CodeGen.Generation
{
    public class Expression
    {
        public Reference Reference { get; protected set; }
        public Code Code { get; protected set; } = new Code();
    }
}