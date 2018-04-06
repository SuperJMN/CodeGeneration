using System.Collections.Generic;

namespace CodeGen.Parsing.Ast.Statements
{
    public class Block : Statement
    {
        public IList<Statement> Statements { get; }
        public IList<DeclarationStatement> Declarations { get; set; }

        public Block() : this(new List<Statement>(), new List<DeclarationStatement>())
        {
        }

        public Block(params Statement[] statements): this(statements, new List<DeclarationStatement>())
        {            
        }

        public Block(IList<Statement> statements, IList<DeclarationStatement> declarations)
        {
            Statements = statements;
            Declarations = declarations;
        }

        public Block(IList<Statement> statements) : this(statements, new List<DeclarationStatement>())
        {
            Statements = statements;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            foreach (var statement in Statements)
            {
                statement.Accept(unitVisitor);
            }
        }
    }
}