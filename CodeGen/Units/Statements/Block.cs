using System.Collections.ObjectModel;

namespace CodeGen.Intermediate.Units.Statements
{
    public class Block : Collection<Statement>, ICodeUnit
    {
        public void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}