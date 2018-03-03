namespace CodeGen.Ast
{
    public class Block
    {
        public Statement[] Statements { get; }

        public Block(Statement[] statements)
        {
            Statements = statements;
        }
    }
}