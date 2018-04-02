using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public interface ICodeVisitor
    {
        void Visit(ExpressionNode expressionNode);
        void Visit(ConstantExpression expression);
        void Visit(IfStatement expression);
        void Visit(ReferenceExpression expression);
        void Visit(AssignmentStatement expression);
        void Visit(ForLoop code);
        void Visit(WhileStatement expressionNode);
        void Visit(DoStatement expressionNode);
        void Visit(AssignmentOperatorStatement statement);
        void Visit(Unit unit);
        void Visit(DeclarationStatement expressionNode);
        void Visit(VariableDeclaration expressionNode);
        void Visit(Program program);
        void Visit(MethodCall expressionNode);
    }
}