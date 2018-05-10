using CodeGen.Core;

namespace CodeGen.Intermediate.Codes
{
    public class IndexedReference
    {
        public IndexedReference(Reference @base, Reference index)
        {
            Base = @base;
            Index = index;
        }

        public Reference @Base { get; }
        public Reference Index { get; }

        public override string ToString()
        {
            return $"{Base}[{Index}]";
        }
    }
}