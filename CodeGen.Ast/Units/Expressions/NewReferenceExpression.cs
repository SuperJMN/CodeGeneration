using CodeGen.Core;

namespace CodeGen.Ast.Units.Expressions
{
    public class NewReferenceExpression : Expression
    {
        public NewReferenceExpression(Reference reference)  : base(reference)
        {
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {            
            codeVisitor.Visit(this);
        }
    }
}