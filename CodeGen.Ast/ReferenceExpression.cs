using CodeGen.Units;

namespace CodeGen.Ast
{
    public class ReferenceExpression : Expression
    {
        public Reference Identifier { get; }

        public ReferenceExpression(Reference identifier)
        {
            Identifier = identifier;
        }
    }
}