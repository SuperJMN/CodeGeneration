using System.Collections.Generic;
using System.Linq;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class Function : ICodeUnit
    {
        public string Name { get; }
        public VariableType ReturnType { get; }
        public ICollection<Argument> Arguments { get; }
        public Block Block { get; }

        public Function(string name, VariableType returnType, ICollection<Argument> arguments, Block block)
        {
            Name = name;
            ReturnType = returnType;
            Arguments = arguments;
            Block = block;
        }

        public void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }

        public override string ToString()
        {
            var args = string.Join(",", Arguments.Select(x => $"{x.Type} {x.Reference}"));
            return $"{ReturnType} {Name}({args})";
        }
    }
}