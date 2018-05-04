namespace CodeGen.Parsing.Ast
{
    public class DeclaratorAndInitializer
    {
        public Declarator Declarator { get; }
        public InitExpression Init { get; }

        public DeclaratorAndInitializer(Declarator declarator, InitExpression init)
        {
            Declarator = declarator;
            Init = init;
        }
    }
}