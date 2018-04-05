using CodeGen.Core;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing
{
    public class ScopeScanner : ICodeUnitVisitor
    {
        private int addressCount;

        public ScopeScanner()
        {
            Scope = new Scope();
            Current = Scope;
        }

        private Scope Current { get; set; }
        public Scope Scope { get; }

        public void Visit(ExpressionNode expressionNode)
        {
            AddReference(expressionNode.Reference);

            foreach (var op in expressionNode.Operands)
            {
                op.Accept(this);
            }            
        }

        public void Visit(ConstantExpression expression)
        {
            AddReference(expression.Reference);
        }

        public void Visit(IfStatement expression)
        {
            expression.Condition.Accept(this);
            expression.Statement.Accept(this);
            expression.ElseStatement?.Accept(this);            
        }

        public void Visit(ReferenceExpression expression)
        {
            AddReference(expression.Reference);
        }

        public void Visit(AssignmentStatement expression)
        {
            expression.Assignment.Accept(this);
            AddReference(expression.Target);
        }

        public void Visit(ForLoop code)
        {
            code.Header.Initialization.Accept(this);
            code.Header.Condition.Accept(this);
            code.Header.Step.Accept(this);
            code.Statement.Accept(this);
        }

        public void Visit(WhileStatement expressionNode)
        {
            expressionNode.Condition.Accept(this);
            expressionNode.Statement.Accept(this);
        }

        public void Visit(DoStatement expressionNode)
        {
            expressionNode.Condition.Accept(this);
            expressionNode.Statement.Accept(this);
        }

        public void Visit(AssignmentOperatorStatement statement)
        {
        }

        public void Visit(Function function)
        {
            CreateScope(function);
            function.Block.Accept(this);
        }

        public void Visit(DeclarationStatement expressionNode)
        {
        }

        public void Visit(VariableDeclaration expressionNode)
        {
        }

        public void Visit(Program program)
        {
            foreach (var function in program.Functions)
            {
                function.Accept(this);
            }
        }

        public void Visit(Call call)
        {
        }

        public void Visit(ReturnStatement returnStatement)
        {
        }

        public void Visit(Argument argument)
        {
        }

        private void AddReference(Reference reference)
        {
            Current.AddReference(reference, addressCount);
            addressCount++;
        }

        private void CreateScope(ICodeUnit scopeOwner)
        {
            addressCount = 0;
            Current = Current.CreateChildScope(scopeOwner);
        }
    }
}