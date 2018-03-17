using System.Collections.Generic;
using System.Linq;
using CodeGen.Intermediate.Codes;
using CodeGen.Plotty.Model;

namespace CodeGen.Plotty
{
    public class PlottyCodeGenerator
    {
        public IEnumerable<PlottyCodeBase> Generate(List<IntermediateCode> intermediateCodes)
        {
            var code = intermediateCodes.First();

            switch (code)
            {
                case IntegerConstantAssignment ias:
                    yield return new MoveCode(new Register(1), ias.Value);
                    break;
            }
        }
    }
}