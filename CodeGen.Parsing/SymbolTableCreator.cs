using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public class SymbolTableCreator
    {
        public SymbolTable Create(ICodeUnit unit)
        {
            var visitor = new SymbolTableVisitor();
            unit.Accept(visitor);
            var visitorRawSymbolTable = visitor.SymbolTableBuilder;
            return CreateSymbolTable(visitorRawSymbolTable);
        }

        private SymbolTable CreateSymbolTable(SymbolTableBuilder origin)
        {
            return origin.Build();
        }
    }
}