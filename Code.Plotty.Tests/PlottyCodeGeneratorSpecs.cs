using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Intermediate.Codes;
using CodeGen.Plotty.Model;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Plotty.Tests
{
    public class PlottyCodeGeneratorSpecs
    {
        [Fact]
        public void Test1()
        {
            var sut = new PlottyCodeGenerator();
            var code = new IntegerConstantAssignment(new Reference("T1"), 123);
            var actual = sut.Generate(new List<IntermediateCode> {code});

            var expected = new List<PlottyCodeBase>()
            {
                new MoveCode(new Register(1), 123)
            };

            actual.ShouldDeepEqual(expected);
        }
    }
}
