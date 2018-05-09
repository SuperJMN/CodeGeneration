namespace CodeGen.Core
{
    public class PointerContent : Reference
    {
        public Reference Reference { get; }

        public PointerContent(Reference reference)
        {
            Reference = reference;
        }
    }
}