using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class Unit
    {
        public string Name { get; }
        public Block Statements { get; }

        public Unit(string name, Block statements)
        {
            Name = name;
            Statements = statements;
        }
    }
}