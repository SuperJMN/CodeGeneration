namespace CodeGen.Parsing.Ast
{
    public class DeclaratorAndInitializer
    {
        public Declarator Declarator { get; }
        public InitializationExpression Initialization { get; }

        public DeclaratorAndInitializer(Declarator declarator, InitializationExpression initialization)
        {
            Declarator = declarator;
            Initialization = initialization;
        }
    }
}