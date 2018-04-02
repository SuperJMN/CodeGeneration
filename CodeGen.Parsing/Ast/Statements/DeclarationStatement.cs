namespace CodeGen.Parsing.Ast.Statements
{
    public class DeclarationStatement : Statement
    {
        public VariableType Type { get; }
        public VariableDeclaration[] Declarations { get; }

        public DeclarationStatement(VariableType type, VariableDeclaration[] declarations)
        {
            Type = type;
            Declarations = declarations;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}