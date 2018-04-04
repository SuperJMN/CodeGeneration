﻿using CodeGen.Core;

namespace CodeGen.Parsing.Ast.Expressions
{
    public class ReferenceExpression : Expression
    {
        public ReferenceExpression(Reference reference)  : base(reference)
        {
        }

        public override void Accept(ICodeVisitor codeVisitor)
        {            
            codeVisitor.Visit(this);
        }

        public static implicit operator ReferenceExpression(string str)
        {
            return new ReferenceExpression(str);
        }
    }
}