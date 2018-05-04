namespace CodeGen.Parsing.Ast
{
    public class Pointer : ReferenceType
    {
        public Pointer(ReferenceType referenceType)
        {
            ReferenceType = referenceType;
        }

        public ReferenceType ReferenceType { get; }

        protected bool Equals(Pointer other)
        {
            return Equals(ReferenceType, other.ReferenceType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Pointer) obj);
        }

        public override int GetHashCode()
        {
            return (ReferenceType != null ? ReferenceType.GetHashCode() : 0);
        }
    }
}