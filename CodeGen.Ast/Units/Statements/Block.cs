using System.Collections.Generic;
using CodeGen.Ast.Parsers;

namespace CodeGen.Ast.Units.Statements
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

        public Block(IList<Statement> statements, IList<DeclarationStatement> declarations = null)
        {
            Statements = statements;
            Declarations = declarations;
        }

        public Block(IList<Statement> statements)
        {
            Statements = statements;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            foreach (var statement in Statements)
            {
                statement.Accept(visitor);
            }
        }
    }
}