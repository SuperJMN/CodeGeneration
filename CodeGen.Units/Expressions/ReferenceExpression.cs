namespace CodeGen.Units.Expressions
{
    public class ReferenceExpression : Expression
    {
        public ReferenceExpression(Reference reference) : base(reference)
        {
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {            
            codeVisitor.Visit(this);
        }
    }
}