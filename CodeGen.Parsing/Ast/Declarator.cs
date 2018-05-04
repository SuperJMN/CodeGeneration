using CodeGen.Core;

namespace CodeGen.Parsing.Ast
{
    public class Declarator
    {
        public Reference Identifier { get; }
        public ArrayDeclarator Array { get; }
        public int IndirectionLevel { get; }

        public Declarator(Reference identifier, ArrayDeclarator array = null, int indirectionLevel = 0)
        {
            Identifier = identifier;
            Array = array;
            IndirectionLevel = indirectionLevel;
        }
    }
}