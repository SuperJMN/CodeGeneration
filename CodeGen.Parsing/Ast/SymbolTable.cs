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
        private readonly IDictionary<Reference, Properties> symbols = new Dictionary<Reference, Properties>();

        public Scope()
        {
        }

        public Scope(ICodeUnit owner, Scope parent)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Parent = parent;
        }

        public IEnumerable<Scope> Children => children.AsReadOnly();

        public IReadOnlyDictionary<Reference, Properties> Symbols => new ReadOnlyDictionary<Reference, Properties>(symbols);

        public Scope CreateChildScope(ICodeUnit scopeOwner)
        {
            var scope = new Scope(scopeOwner, this);
            children.Add(scope);
            return scope;
        }

        public void AnnotateSymbol(Reference reference)
        {
            if (!symbols.ContainsKey(reference))
            {
                symbols.Add(reference, new Properties());
            }
        }

        public void AnnotateTypedSymbol(Reference reference, VariableType type)
        {
            if (!symbols.ContainsKey(reference))
            {
                symbols.Add(reference, new Properties());
            }
            else
            {                
                symbols[reference].AssignType(type);
            }
        }
    }
}