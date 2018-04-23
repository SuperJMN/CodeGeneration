using System;
using CodeGen.Parsing.Ast;

namespace CodeGen.Intermediate.Codes
{
    public class FunctionDefinitionCode : IntermediateCode
    {
        public FunctionFirm Firm { get; }

        public FunctionDefinitionCode(FunctionFirm firm)
        {
            Firm = firm;
        }

        public override void Accept(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Definition of function {Firm}";
        }
    }
}