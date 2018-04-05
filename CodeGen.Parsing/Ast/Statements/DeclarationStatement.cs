namespace CodeGen.Parsing.Ast.Statements
{
    public class DeclarationStatement : Statement
    {
        public VariableType Type { get; }
        public VariableDeclaration[] Declarations { get; }

        public DeclarationStatement(VariableType type, params VariableDeclaration[] declarations)
        {
            Type = type;
            Declarations = declarations;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}