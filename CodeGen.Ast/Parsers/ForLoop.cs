using System;
using CodeGen.Ast.Units;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;

namespace CodeGen.Ast.Parsers
{
    public class ForLoop : Statement
    {
        public ForLoop(Statement initialization, Expression condition, Statement step, Statement statement)
        {
            Initialization = initialization;
            Condition = condition;
            Step = step;
            Statement = statement;
        }

        public Statement Initialization { get; }
        public Expression Condition { get; }
        public Statement Step { get; }
        public Statement Statement { get; }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}