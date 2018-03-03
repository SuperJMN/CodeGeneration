namespace CodeGen.Ast
{
    public class ReferenceExpression : Expression
    {
        public string Identifier { get; }

        public ReferenceExpression(string identifier)
        {
            Identifier = identifier;
        }
    }
}