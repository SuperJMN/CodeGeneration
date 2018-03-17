using System.Linq;
using CodeGen.Compiler;
using Plotty.VirtualMachine;

namespace CodeGen.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var parsed = new CodeGenerator().Generate("{ a=1; b=a; c=3; b = c; }");

            var plottyCore = new PlottyCore();
            plottyCore.Load(parsed.ToList());

            while (plottyCore.CanExecute)
            {
                plottyCore.Execute();
            }                        
        }
    }
}