using System;
using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public class SymbolTableBuilder
    {
        private readonly List<Appearance> appearances = new List<Appearance>();
        private readonly List<SymbolTableBuilder> children = new List<SymbolTableBuilder>();

        public SymbolTableBuilder()
        {
        }

        public SymbolTableBuilder(ICodeUnit owner, SymbolTableBuilder parent)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Parent = parent;
        }

        public IEnumerable<SymbolTableBuilder> Children => children.AsReadOnly();

        public IReadOnlyList<Appearance> Appearances => appearances.AsReadOnly();

        public ICodeUnit Owner { get; }

        public SymbolTableBuilder Parent { get; }

        public void AddAppearanceForImplicit(Reference reference)
        {
            appearances.Add(new Appearance(reference) { Size = 1 });
        }

        public SymbolTableBuilder AddChild(ICodeUnit owner)
        {
            var child = new SymbolTableBuilder(owner, this);
            children.Add(child);
            return child;
        }

        public void AddAppearance(Reference reference, ReturnType argumentType, int size = 1)
        {
            appearances.Add(new Appearance(reference)
            {
                Type = argumentType,
                Size = size,
                IsDeclaration = true,
            });
        }

        public SymbolTable Build()
        {
            var symbols = GenerateSymbols(this);
            
            var subTables = new List<SymbolTable>();
            foreach (var child in Children)
            {
                var convertedChild = child.Build();
                subTables.Add(convertedChild);
            }

            var symbolTable = new SymbolTable(Owner, symbols.ToDictionary(s => s.Reference, s => new Properties()
            {
                Offset = s.Offset,
                Type = s.Type,
                Size = s.Size,
            }), subTables);

            return symbolTable;
        }

        private static IEnumerable<Symbol> GenerateSymbols(SymbolTableBuilder origin)
        {
            var offset = 0;
            var list = new List<Symbol>();
            foreach (var g in origin.Appearances.GroupBy(x => x.Reference))
            {
                var firstAppearance = g.FirstOrDefault(x => x.IsDeclaration) ?? g.First();
                list.Add(new Symbol(g.Key, firstAppearance.Type, offset, firstAppearance.Size));

                offset += firstAppearance.Size;                
            }

            return list;
        }
    }
}