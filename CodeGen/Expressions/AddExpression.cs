﻿namespace CodeGen.Intermediate.Expressions
{
    public class AddExpression : Expression
    {
        public AddExpression(Expression left, Expression right) : base(new Reference())
        {
            Left = left;
            Right = right;
        }

        public Expression Left { get; }

        public Expression Right { get; }

        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }
    }
}