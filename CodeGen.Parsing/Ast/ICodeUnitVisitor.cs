using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public interface ICodeUnitVisitor
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
        void Visit(Function function);
        void Visit(DeclarationStatement expressionNode);
        void Visit(VariableDeclaration expressionNode);
        void Visit(Program program);
        void Visit(Call call);
        void Visit(ReturnStatement returnStatement);
        void Visit(Argument argument);
        void Visit(DeclStatement statement);
    }
}