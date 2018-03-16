using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeGen.Ast.Units.Statements
{
    public class Block : Collection<Statement>, ICodeUnit
    {
        public Block()
        {            
        }

        public Block(IList<Statement> statements) : base(statements)
        {
        }

        public void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}