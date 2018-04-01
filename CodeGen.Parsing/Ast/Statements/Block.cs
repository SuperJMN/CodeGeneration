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

        public Block(IList<Statement> statements, IList<DeclarationStatement> declarations = null)
        {
            Statements = statements;
            Declarations = declarations;
        }

        public Block(IList<Statement> statements)
        {
            Statements = statements;
        }

        public Block(Statement[] statements, DeclarationStatement[][] declarations)
        {
            throw new System.NotImplementedException();
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