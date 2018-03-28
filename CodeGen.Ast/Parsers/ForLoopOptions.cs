using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;

namespace CodeGen.Ast.Parsers
{
    public class ForLoopOptions
    {
        public ForLoopOptions(Statement initialization, Expression condition, Statement step)
        {
            Initialization = initialization;
            Condition = condition;
            Step = step;
        }

        public Statement Initialization { get; }
        public Expression Condition { get; }
        public Statement Step { get; }
    }
}