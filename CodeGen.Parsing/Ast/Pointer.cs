namespace CodeGen.Parsing.Ast
{
    public class Pointer : ReturnType
    {
        public Pointer(ReturnType returnType)
        {
            ReturnType = returnType;
        }

        public ReturnType ReturnType { get; }

        protected bool Equals(Pointer other)
        {
            return Equals(ReturnType, other.ReturnType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Pointer) obj);
        }

        public override int GetHashCode()
        {
            return (ReturnType != null ? ReturnType.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"*{ReturnType}";
        }
    }
}