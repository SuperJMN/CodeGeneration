using CodeGen.Ast;
using CodeGen.Ast.NewParsers;
using Superpower;

namespace CodeGen.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var parsed = Statements.IfStatement.Parse(TokenizerFactory.Create().Tokenize("if (true) {b=3;}"));

            var intermediateCode = new CodeGenerator().Generate("if (a) {b=3;}");

            foreach (var i in intermediateCode)
            {
               System.Console.WriteLine(i);
            }
        }
    }
}