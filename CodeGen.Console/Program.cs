using CodeGen.Intermediate;
using CodeGen.Units;
using CodeGen.Units.Expressions;
using CodeGen.Units.Statements;

namespace CodeGen.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var statement = new AssignmentStatement(
                new Reference("a"),
                new OperatorExpression(OperatorKind.Add,
                    new ReferenceExpression(new Reference("b")),
                    new OperatorExpression(OperatorKind.Mult, new ReferenceExpression(new Reference("c")),
                        new ReferenceExpression(new Reference("d")))
                ));

            var generator = new CodeGenerator();
            var codes = generator.Generate(statement);

            foreach (var line in codes)
            {
               System.Console.WriteLine(line);
            }
        }       
    }
}