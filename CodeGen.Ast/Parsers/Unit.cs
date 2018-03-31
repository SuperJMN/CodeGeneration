using CodeGen.Ast.Units.Statements;

namespace CodeGen.Ast.Parsers
{
    public class Unit
    {
        public string Name { get; }
        public DeclarationStatement[] Decls { get; }
        public Statement Statements { get; }

        public Unit(string name, DeclarationStatement[] decls, Statement statements)
        {
            Name = name;
            Decls = decls;
            Statements = statements;
        }
    }
}