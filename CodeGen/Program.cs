using System;
using CodeGen.Generation;

namespace CodeGen
{
    internal static class Program
    {
        private static void Main()
        {
            PrintSample1();
            PrintSeparator();
            PrintSample2();
        }

        private static void PrintSeparator()
        {
            Console.WriteLine("--");
        }

        private static void PrintSample1()
        {
            // AST for the expression: a = b + c * d

            var assigmentExpression = new AssignmentExpression(
                new Reference("a"),
                new AddExpression(
                    new ReferenceExpression(new Reference("b")),
                    new MultExpression(new ReferenceExpression(new Reference("c")),
                        new ReferenceExpression(new Reference("d")))
                )
            );

            var code = assigmentExpression.Code;
            foreach (var ins in code.Instructions)
            {
                Console.WriteLine(ins);
            }

            //Prints

            //MUL T0 c d
            //ADD T1 b T0
            //MOV a T1 null                   
        }

        private static void PrintSample2()
        {
            // AST for the expression: x = y * z * w + y + x
            var assignmentExpression = new AssignmentExpression(
                new Reference("x"),
                new AddExpression(
                    new MultExpression(
                        new ReferenceExpression(new Reference("y")),
                        new MultExpression(new ReferenceExpression(new Reference("z")),
                            new ReferenceExpression(new Reference("w")))
                    ),
                    new AddExpression(new ReferenceExpression(new Reference("y")),
                        new ReferenceExpression(new Reference("x")))
                )
            );

            var code = assignmentExpression.Code;

            foreach (var ins in code.Instructions)
            {
                Console.WriteLine(ins);
            }

            /*
             MUL T2 z w
             MUL T3 y T2
             ADD T4 y x
             ADD T5 T3 T4
             MOV x T5 null
            */
        }
    }
}