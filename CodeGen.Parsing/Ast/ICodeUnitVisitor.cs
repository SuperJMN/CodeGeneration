using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public interface ICodeUnitVisitor
    {
        void Visit(ExpressionNode unit);
        void Visit(ConstantExpression unit);
        void Visit(IfStatement unit);
        void Visit(ReferenceExpression unit);
        void Visit(AssignmentStatement unit);
        void Visit(ForLoop unit);
        void Visit(WhileStatement unit);
        void Visit(DoStatement unit);
        void Visit(AssignmentOperatorStatement unit);
        void Visit(Function unit);
        void Visit(Program unit);
        void Visit(Call unit);
        void Visit(ReturnStatement unit);
        void Visit(Argument unit);
        void Visit(DeclarationStatement unit);
        void Visit(ListInitialization unit);
        void Visit(DirectInitialization unit);
        void Visit(StandardReferenceItem unit);
        void Visit(ArrayReferenceItem unit);
    }
}