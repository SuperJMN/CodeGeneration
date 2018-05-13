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

    public static class ReferenceMixin 
    {
        public static int GetAddress(this Reference reference, SymbolTable table)
        {
            return table.Symbols[reference].Offset;
        }

        public static int GetValue(this Reference reference, SymbolTable table, int[] memory, int index = 1)
        {
            return GetArray(reference, table, memory, 1, index).First();
        }

        public static void SetValue(this Reference reference, int value, SymbolTable table, int[] memory, int index = 0)
        {
            if (table.Symbols.TryGetValue(reference, out var props))
            {
                memory[props.Offset + index] = value;
            }
            else
            {
                throw new InvalidOperationException($"The referece {reference} doesn't exist in the 'main' symbolTable");
            }            
        }

        public static int[] GetArray(this Reference reference, SymbolTable table, int[] memory, int length, int index = 0)
        {
            if (table.Symbols.TryGetValue(reference, out var props))
            {
                return memory.Skip(props.Offset + index).Take(length).ToArray();
            }

            throw new InvalidOperationException($"The referece {reference} doesn't exist in the 'main' symbolTable");
        }
    }
}