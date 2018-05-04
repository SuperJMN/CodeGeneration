using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Parsing.Ast.Expressions;

namespace CodeGen.Parsing.Ast
{
    public class Call : Expression
    {
        public string FunctionName { get; }
        public IEnumerable<Expression> Parameters { get; }

        public Call(string functionName, params Expression[] parameters) : base(new Reference())
        {
            FunctionName = functionName;
            Parameters = parameters;
        }

        public override void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}
