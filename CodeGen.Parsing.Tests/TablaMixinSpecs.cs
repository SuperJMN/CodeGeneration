using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Statements;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class TablaMixinSpecs
    {
        [Fact]
        public void Convert()
        {
            var root = new FullSymbolTable();
            var owner = new Function(new FunctionFirm("sample", ReturnType.Int, new List<Argument>()), new Block());
            var child = root.AddChild(owner);
            child.AddAppearance("a", PrimitiveType.Int);
            child.AddAppearance("array", PrimitiveType.Int, 30);
            child.AddAppearanceForImplicit("array");
            child.AddAppearanceForImplicit("T1");
            child.AddAppearanceForImplicit("T1");
            child.AddAppearance("a", PrimitiveType.Int);
            child.AddAppearanceForImplicit("T1");

            var actual = root.ToSymbolTable();

            var symbols = new List<Symbol>()
            {
                new Symbol("a", PrimitiveType.Int, 0, 1),
                new Symbol("array", PrimitiveType.Int, 1, 30),
                new Symbol("T1", PrimitiveType.Int, 31, 1),
            }.ToDictionary(s => s.Reference, s => new Properties()
            {
                Type = s.Type,
                Size = s.Size,
                Offset = s.Offset,
            });

            var expectedChild = new SymbolTable(owner, symbols, new List<SymbolTable>());

            var expected = new SymbolTable(null, new Dictionary<Reference, Properties>(), new[] {expectedChild});
            
            actual.ShouldDeepEqual(expected);
        }
    }
}