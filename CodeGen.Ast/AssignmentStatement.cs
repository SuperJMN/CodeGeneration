namespace CodeGen.Ast
{
    public class AssignmentStatement : Statement
    {
        public string Reference { get; }
        public Expression Expr { get; }

        public AssignmentStatement(string reference, Expression expr)
        {
            Reference = reference;
            Expr = expr;
        }
    }
}