using System;
using System.Diagnostics;

namespace CodeGen.Core
{
    public class Reference
    {
        public Reference()
        {
            
        }

        public bool IsUnknown => Identifier == null;

        public Reference(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; set; }

        public override string ToString()
        {
            return Identifier;
        }

        protected bool Equals(Reference other)
        {
            if (other.IsUnknown || other.IsUnknown)
            {
                return false;
            }

            return string.Equals(Identifier, other.Identifier);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Reference) obj);
        }

        public override int GetHashCode()
        {
            return (Identifier != null ? Identifier.GetHashCode() : 0);
        }       

        public static implicit operator Reference(string str)
        {
            return new Reference(str);
        }
    }
}