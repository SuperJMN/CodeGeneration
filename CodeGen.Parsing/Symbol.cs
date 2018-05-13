using CodeGen.Core;
using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public class Symbol
    {
        public Reference Reference { get; }
        public ReturnType Type { get; }
        public int Size { get; }
        public int Offset { get; set; }

        public Symbol(Reference reference, ReturnType type, int offset, int size)
        {
            Reference = reference;
            Type = type;
            Offset = offset;
            Size = size;
        }
    }
}