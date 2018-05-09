namespace CodeGen.Parsing.Ast
{
    public class DeclaratorAndInitializer
    {
        public ReferenceItem ReferenceItem { get; }
        public InitializationExpression Initialization { get; }

        public DeclaratorAndInitializer(ReferenceItem referenceItem, InitializationExpression initialization)
        {
            ReferenceItem = referenceItem;
            Initialization = initialization;
        }
    }
}