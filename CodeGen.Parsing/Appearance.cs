﻿using CodeGen.Core;
using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public class Appearance
    {
        public Appearance(Reference reference)
        {
            Reference = reference;
        }

        public Reference Reference { get; }
        public PrimitiveType Type { get; set; }
        public int Size { get; set; }

        public override string ToString()
        {
            return $"{nameof(Reference)}: {Reference}, {nameof(Type)}: {Type}, {nameof(Size)}: {Size}";
        }
    }
}