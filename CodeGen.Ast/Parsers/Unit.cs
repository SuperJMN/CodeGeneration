using CodeGen.Ast.Units.Statements;

namespace CodeGen.Ast.Parsers
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