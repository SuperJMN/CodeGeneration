namespace CodeGen.Parsing.Ast.Statements
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


        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}