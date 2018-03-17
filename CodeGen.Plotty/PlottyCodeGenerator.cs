using System.Collections.Generic;
using System.Linq;
using CodeGen.Intermediate.Codes;
using CodeGen.Plotty.Model;

namespace CodeGen.Plotty
{
    public class PlottyCodeGenerator
    {
        public IEnumerable<Instruction> Generate(List<IntermediateCode> intermediateCodes)
        {
            var code = intermediateCodes.First();

            switch (code)
            {
                case IntegerConstantAssignment ias:
                    yield return new MoveInstruction()
                    {
                        Destination = new Register(1),
                        Source = new ImmediateSource(123),
                    };
                    break;
            }
        }
    }
}