using System;
using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;

namespace CodeGen.Parsing.Ast
{
    public class SymbolTable
    {
        public ICodeUnit Owner { get; }
        public Dictionary<Reference, Properties> Symbols { get; }
        public IEnumerable<SymbolTable> Children { get; }
        public SymbolTable Parent { get; }

        public SymbolTable()
        {
        }

        public SymbolTable(ICodeUnit owner, SymbolTable parent)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Parent = parent;
        }

        public SymbolTable(ICodeUnit owner, Dictionary<Reference, Properties> symbols, IEnumerable<SymbolTable> children)
        {
            Owner = owner;
            Symbols = symbols;
            Children = children;
        }

        public int Size
        {
            get
            {
                var size = Symbols.Aggregate(0, (a, b) => a + b.Value.Size);
                return size;
            }
        }
    }
}