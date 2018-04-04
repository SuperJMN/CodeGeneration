using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;

namespace CodeGen.Parsing.Ast
{
    public class Call : Statement
    {
        public string FunctionName { get; }
        public Reference Target { get; }
        public Reference Reference { get; }
        public IEnumerable<Expression> Parameters { get; }

        public Call(string functionName, params Expression[] parameters)
        {
            FunctionName = functionName;
            Parameters = parameters;
            Reference = new Reference();
        }

        public Call(string functionName, Reference target, params Expression[] parameters) : this(functionName, parameters)
        {            
            Target = target;            
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
