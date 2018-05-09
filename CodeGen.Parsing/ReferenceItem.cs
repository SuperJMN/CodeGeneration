using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing
{
    public abstract class ReferenceItem : Expression
    {
        protected ReferenceItem(Reference reference) : base(reference)
        {
        }

        public static implicit operator ReferenceItem(string str)
        {
            return new StandardReferenceItem(new Reference(str));
        }
    }
}