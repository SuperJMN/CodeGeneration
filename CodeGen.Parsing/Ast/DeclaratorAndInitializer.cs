namespace CodeGen.Parsing.Ast
{
    public class DeclaratorAndInitializer
    {
        public ReferenceAccessItem ReferenceAccessItemAccess { get; }
        public InitializationExpression Initialization { get; }

        public DeclaratorAndInitializer(ReferenceAccessItem referenceAccessItemItem, InitializationExpression initialization)
        {
            ReferenceAccessItemAccess = referenceAccessItemItem;
            Initialization = initialization;
        }
    }
}