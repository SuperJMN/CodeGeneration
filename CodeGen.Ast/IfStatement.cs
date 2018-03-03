namespace CodeGen.Ast
{
    public class IfStatement : Statement
    {
        public Expression Expr { get; }
        public Block Block { get; }

        public IfStatement(Expression expr, Block block)
        {
            Expr = expr;
            Block = block;
        }
    }
}