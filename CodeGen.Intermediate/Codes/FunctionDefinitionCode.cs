using System;
using CodeGen.Parsing.Ast;

namespace CodeGen.Intermediate.Codes
{
    public class FunctionDefinitionCode : IntermediateCode
    {
        public Function Function { get; }

        public FunctionDefinitionCode(Function function)
        {
            Function = function;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Definition of function {Function}";
        }
    }
}