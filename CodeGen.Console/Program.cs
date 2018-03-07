using CodeGen.Ast;

namespace CodeGen.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var intermediateCode = new CodeGenerator().Generate("if (a) {b=3;}");

            foreach (var i in intermediateCode)
            {
               System.Console.WriteLine(i);
            }
        }
    }
}