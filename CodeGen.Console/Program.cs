using System.Linq;
using CodeGen.Compiler;
using Plotty.VirtualMachine;

namespace CodeGen.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var plottyCode = new PlottyCompiler().Compile("{ a=1; b=a; c=3; b = c; }");

            var machine = new PlottyMachine();
            machine.Load(plottyCode.ToList());

            while (machine.CanExecute)
            {
                machine.Execute();
            }                        
        }
    }
}