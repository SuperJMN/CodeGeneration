using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Core;

namespace CodeGen.Parsing.Ast
{
    public class Scope
    {
        public ICodeUnit Owner { get; }
        public Scope Parent { get; }
        private readonly List<Scope> children = new List<Scope>();
        private readonly IDictionary<Reference, Symbol> symbols = new Dictionary<Reference, Symbol>();

        public Scope()
        {
        }

        public Scope(ICodeUnit owner, Scope parent)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Parent = parent;
        }

        public IEnumerable<Scope> Children => children.AsReadOnly();

        public IReadOnlyDictionary<Reference, Symbol> Symbols => new ReadOnlyDictionary<Reference, Symbol>(symbols);

        public Scope CreateChildScope(ICodeUnit scopeOwner)
        {
            var scope = new Scope(scopeOwner, this);
            children.Add(scope);
            return scope;
        }

        public void AddReference(Reference r)
        {
            if (symbols.ContainsKey(r))
            {
                return;
            }

            symbols.Add(r, new Symbol());
        }
    }
}