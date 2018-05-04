namespace CodeGen.Parsing.Ast
{
    public class ListInitialization : InitializationExpression
    {
        public int[] Items { get; }

        public ListInitialization(params int[] items)
        {
            Items = items;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}