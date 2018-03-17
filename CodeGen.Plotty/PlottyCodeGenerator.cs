using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Intermediate;
using CodeGen.Intermediate.Codes;
using Plotty.Model;

namespace CodeGen.Plotty
{
    public class PlottyCodeGenerator
    {
        public IEnumerable<Instruction> Generate(List<IntermediateCode> intermediateCodes)
        {
            var visitor = new NamedObjectCollector();
            intermediateCodes.ForEach(x => x.Accept(visitor));
            var references = visitor.References
                .Select((r, i) => new {r, i})
                .ToDictionary(arg => arg.r, arg => new Register(arg.i));

            foreach (var intermediateCode in intermediateCodes)
            {
                switch (intermediateCode)
                {
                    case IntegerConstantAssignment ias:
                        yield return new MoveInstruction
                        {
                            Destination = references[ias.Target],
                            Source = new ImmediateSource(ias.Value),
                        };
                        break;
                }    
            }           
        }
    }  
}