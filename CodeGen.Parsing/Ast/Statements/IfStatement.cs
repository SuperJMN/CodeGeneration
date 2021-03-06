﻿using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast.Statements
{
    public class IfStatement : Statement
    {
        public Expression Condition { get; }
        public Statement Statement { get; }
        public Statement ElseStatement { get; }

        public IfStatement(Expression condition, Statement statement, Statement elseStatement = null)
        {
            Condition = condition;
            Statement = statement;
            ElseStatement = elseStatement;
        }
        
        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}