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
        public IEnumerable<Line> Generate(List<IntermediateCode> intermediateCodes)
        {
            var visitor = new NamedObjectCollector();
            intermediateCodes.ForEach(x => x.Accept(visitor));
            var enumerable = visitor.References
                .Distinct();

            var references = enumerable
                .Select((r, i) => new { r, i })
                .ToDictionary(arg => arg.r, arg => new Register(arg.i));

            foreach (var intermediateCode in intermediateCodes)
            {
                switch (intermediateCode)
                {
                    case IntegerConstantAssignment ias:
                        yield return new Line(new MoveInstruction
                        {
                            Destination = references[ias.Target],
                            Source = new ImmediateSource(ias.Value),
                        });
                        break;
                    case ReferenceAssignment ras:
                        yield return new Line(new MoveInstruction
                        {
                            Destination = references[ras.Target],
                            Source = new RegisterSource(references[ras.Origin]),
                        });
                        break;
                }
            }
        }
    }
}