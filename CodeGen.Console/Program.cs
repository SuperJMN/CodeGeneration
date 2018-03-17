using CodeGen.Ast;
using CodeGen.Ast.Parsers;
using CodeGen.Compiler;
using Superpower;

namespace CodeGen.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var parsed = Statements.IfStatement.Parse(TokenizerFactory.Create().Tokenize("{ a=1; b=2; c=3; }"));

            var intermediateCode = new CodeGenerator().Generate("if (a) {b=3;}");

            foreach (var i in intermediateCode)
            {
               System.Console.WriteLine(i);
            }
        }
    }
}