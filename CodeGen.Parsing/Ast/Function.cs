using System.Collections.Generic;
using System.Linq;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class Function : ICodeUnit
    {
        public string Name => Firm.Name;
        public ReturnType ReturnType => Firm.ReturnType;
        public ICollection<Argument> Arguments => Firm.Arguments;
        public FunctionFirm Firm { get; }
        public Block Block { get; }

        public Function(FunctionFirm firm, Block block)
        {
            Firm = firm;
            Block = block;
        }

        public void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }

        public override string ToString()
        {
            var args = string.Join(",", Arguments.Select(x => $"{x.Type} {x.AccessItem}"));
            return $"{ReturnType} {Name}({args})";
        }
    }
}