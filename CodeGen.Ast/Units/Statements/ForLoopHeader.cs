using CodeGen.Ast.Units.Expressions;

namespace CodeGen.Ast.Units.Statements
{
    public class ForLoopHeader
    {
        public ForLoopHeader(Statement initialization, Expression condition, Statement step)
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