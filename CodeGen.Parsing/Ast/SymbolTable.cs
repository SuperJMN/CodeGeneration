using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Core;

namespace CodeGen.Parsing.Ast
{
    public class SymbolTable
    {
        public ICodeUnit Owner { get; }
        public SymbolTable Parent { get; }
        private readonly List<SymbolTable> children = new List<SymbolTable>();
        private readonly IDictionary<Reference, Properties> symbols = new Dictionary<Reference, Properties>();

        public SymbolTable()
        {
        }

        public SymbolTable(ICodeUnit owner, SymbolTable parent)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Parent = parent;
        }

        public IEnumerable<SymbolTable> Children => children.AsReadOnly();

        public IReadOnlyDictionary<Reference, Properties> Symbols => new ReadOnlyDictionary<Reference, Properties>(symbols);

        public SymbolTable CreateChildScope(ICodeUnit scopeOwner)
        {
            var scope = new SymbolTable(scopeOwner, this);
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