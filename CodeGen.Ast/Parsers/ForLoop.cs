using System;
using CodeGen.Ast.Units;
using CodeGen.Ast.Units.Statements;

namespace CodeGen.Ast.Parsers
{
    public class ForLoop : Statement
    {
        public ForLoop(ForLoopOptions forLoopOptions, Statement statement)
        {
            ForLoopOptions = forLoopOptions;
            Statement = statement;
        }

        public ForLoopOptions ForLoopOptions { get; }
        public Statement Statement { get; }


        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}