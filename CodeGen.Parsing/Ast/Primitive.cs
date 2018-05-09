namespace CodeGen.Parsing.Ast
{
    public class Primitive : ReturnType
    {
        public Primitive(PrimitiveType type)
        {
            Type = type;
        }

        public PrimitiveType Type { get; }

        protected bool Equals(Primitive other)
        {
            return Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
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