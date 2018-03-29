namespace CodeGen.Ast.Units.Statements
{
    public class ForLoop : Statement
    {
        public ForLoop(ForLoopHeader header, Statement statement)
        {
            Header = header;
            Statement = statement;
        }

        public ForLoopHeader Header { get; }
        public Statement Statement { get; }


        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}