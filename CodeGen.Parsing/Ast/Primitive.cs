namespace CodeGen.Parsing.Ast
{
    public class Primitive : ReturnType
    {
        public Primitive(PrimitiveType type, bool isArray = false)
        {
            Type = type;
            IsArray = isArray;
        }

        public PrimitiveType Type { get; }
        public bool IsArray { get; }

        protected bool Equals(Primitive other)
        {
            return Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Primitive) obj);
        }

        public override int GetHashCode()
        {
            return (int) Type;
        }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}