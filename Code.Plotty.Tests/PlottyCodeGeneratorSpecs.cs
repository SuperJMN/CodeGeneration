using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Intermediate.Codes;
using DeepEqual.Syntax;
using Plotty.Model;
using Xunit;

namespace CodeGen.Plotty.Tests
{
    public class PlottyCodeGeneratorSpecs
    {
        [Fact]
        public void IntegerConstant()
        {
            var sut = new PlottyCodeGenerator();
            var code = new IntegerConstantAssignment(new Reference("T1"), 123);
            var actual = sut.Generate(new List<IntermediateCode> {code});

            var expected = new List<Instruction>
            {
                new MoveInstruction()
                {
                    Destination = new Register(1),
                    Source = new ImmediateSource(123),
                }
            };

            actual.ShouldDeepEqual(expected);
        }
    }
}
