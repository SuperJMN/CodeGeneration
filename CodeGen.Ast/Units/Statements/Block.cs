using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeGen.Ast.Units.Statements
{
    public class Block : Statement
    {
        public IList<Statement> Statements { get; }

        public Block(params Statement[] statements)
        {
            Statements = statements;
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