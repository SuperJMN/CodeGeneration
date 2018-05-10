using System;
using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public class FullSymbolTable
    {
        private readonly List<Appearance> appearances = new List<Appearance>();
        private readonly List<FullSymbolTable> children = new List<FullSymbolTable>();

        public FullSymbolTable()
        {
        }

        public FullSymbolTable(ICodeUnit owner, FullSymbolTable parent)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Parent = parent;
        }

        public IEnumerable<FullSymbolTable> Children => children.AsReadOnly();

        public IReadOnlyList<Appearance> Appearances => appearances.AsReadOnly();

        public ICodeUnit Owner { get; }

        public FullSymbolTable Parent { get; }


        public void AddAppearanceForImplicit(Reference reference)
        {
            appearances.Add(new Appearance(reference) { Size = 1 });
        }

        public FullSymbolTable AddChild(ICodeUnit owner)
        {
            var child = new FullSymbolTable(owner, this);
            children.Add(child);
            return child;
        }

        public void AddAppearance(Reference reference, PrimitiveType argumentType, int size = 1)
        {
            appearances.Add(new Appearance(reference)
            {
                Type = argumentType,
                Size = size
            });
        }
    }
}