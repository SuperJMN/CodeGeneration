using System.Collections.Generic;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class MethodCall : Statement
    {
        public string MethodName { get; }
        public IEnumerable<Expression> Parameters { get; }

        public MethodCall(string methodName, IEnumerable<Expression> parameters)
        {
            MethodName = methodName;
            Parameters = parameters;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}