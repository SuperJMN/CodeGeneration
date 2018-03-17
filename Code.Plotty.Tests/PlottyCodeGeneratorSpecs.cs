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
                    Destination = new Register(0),
                    Source = new ImmediateSource(123),
                }
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void MultipleConstants()
        {
            var sut = new PlottyCodeGenerator();
            var actual = sut.Generate(new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set(new Reference("a"), 1),
                IntermediateCode.Emit.Set(new Reference("b"), 2),
                IntermediateCode.Emit.Set(new Reference("c"), 3),
            });

            var expected = new List<Instruction>
            {
                new MoveInstruction()
                {
                    Destination = new Register(0),
                    Source = new ImmediateSource(1),
                },
                new MoveInstruction()
                {
                    Destination = new Register(1),
                    Source = new ImmediateSource(2),
                }, new MoveInstruction()
                {
                    Destination = new Register(2),
                    Source = new ImmediateSource(3),
                }
            };

            actual.ShouldDeepEqual(expected);
        }
    }
}
