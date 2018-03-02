using CodeGen.Intermediate;
using CodeGen.Intermediate.Expressions;

namespace CodeGen.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var assigmentExpression = new AssignmentExpression(
                new Reference("a"),
                new AddExpression(
                    new ReferenceExpression(new Reference("b")),
                    new MultExpression(new ReferenceExpression(new Reference("c")),
                        new ReferenceExpression(new Reference("d")))
                )
            );

            var generator = new CodeGenerator();
            var codes = generator.Generate(assigmentExpression);

            foreach (var line in codes)
            {
               System.Console.WriteLine(line);
            }
        }       
    }
}