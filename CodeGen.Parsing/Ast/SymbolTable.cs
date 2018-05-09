using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public void AnnotateImplicit(Reference reference)
        {
            if (!symbols.ContainsKey(reference))
            {
                symbols.Add(reference, new Properties());
            }
        }

        public int Size
        {
            get
            {
                var size = Symbols.Aggregate(0, (a, b) => a + b.Value.Size);
                return size;
            }
        }

        public void Annotate(Reference reference, PrimitiveType type, int length = 1)
        {
            Properties properties;

            if (!symbols.ContainsKey(reference))
            {
                properties = new Properties();
                symbols.Add(reference, properties);
            }
            else
            {
                properties = symbols[reference];
            }

            properties.AssignType(type);
            properties.AssignLength(length);
        }
    }
}