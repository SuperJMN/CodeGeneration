using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeGen.Units;

namespace CodeGen.Ast.NewParsers
{
    public class Block : Collection<Statement>
    {
        public Block()
        {            
        }

        public Block(IList<Statement> statements) : base(statements)
        {
        }

    }
}