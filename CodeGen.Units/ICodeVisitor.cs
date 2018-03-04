﻿using CodeGen.Units.Expressions;
using CodeGen.Units.Statements;

namespace CodeGen.Units
{
    public interface ICodeVisitor
    {
        void Visit(AssignmentStatement statement);
        void Visit(ReferenceExpression expression);
        void Visit(IfStatement statement);
        void Visit(Block block);
        void Visit(OperatorExpression expression);
    }
}