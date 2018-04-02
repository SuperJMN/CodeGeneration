using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class Unit : ICodeUnit
    {
        public string Name { get; }
        public Block Block { get; }

        public Unit(string name, Block block)
        {
            Name = name;
            Block = block;
        }

        public void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}