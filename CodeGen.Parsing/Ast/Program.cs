﻿using System.Collections.Generic;

namespace CodeGen.Parsing.Ast
{
    public class Program : ICodeUnit
    {
        public ICollection<Function> Functions { get; }

        public Program(ICollection<Function> functions)
        {
            Functions = functions;
        }

        public void Accept(ICodeUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }
    }
}