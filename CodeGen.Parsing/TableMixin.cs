using System.Collections.Generic;
using System.Linq;
using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public static class TableMixin
    {
        public static SymbolTable ToSymbolTable(this SymbolTableBuilder origin)
        {
            var symbols = GenerateSymbols(origin);
            
            var children = new List<SymbolTable>();
            foreach (var child in origin.Children)
            {
                var convertedChild =ToSymbolTable(child);
                children.Add(convertedChild);
            }

            var symbolTable = new SymbolTable(origin.Owner, symbols.ToDictionary(s => s.Reference, s => new Properties()
            {
                Offset = s.Offset,
                Type = s.Type,
                Size = s.Size,
            }), children);

            return symbolTable;
        }

        private static IEnumerable<Symbol> GenerateSymbols(SymbolTableBuilder origin)
        {
            var offset = 0;
            var list = new List<Symbol>();
            foreach (var g in origin.Appearances.GroupBy(x => x.Reference))
            {
                var firstAppearance = g.First();
                list.Add(new Symbol(g.Key, firstAppearance.Type, offset, firstAppearance.Size));

                offset += firstAppearance.Size;                
            }

            return list;
        }
    }
}