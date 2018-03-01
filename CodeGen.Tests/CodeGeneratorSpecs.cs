using System.Collections.Generic;
using CodeGen.Generation;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Tests
{
    public class CodeGeneratorSpecs
    {
        [Fact]
        public void Assignment()
        {
            var expr = new AssignmentExpression(
                new Reference("a"),
                new AddExpression(
                    new ReferenceExpression(new Reference("b")),
                    new MultExpression(new ReferenceExpression(new Reference("c")),
                        new ReferenceExpression(new Reference("d")))
                )
            );

            var sut = new CodeGenerator();
            var actual = sut.Generate(expr);
            var expected = new Code(new List<ThreeAddressCode>()
            {
                new ThreeAddressCode(CodeType.Mult, new Reference("T2"), new Reference("c"), new Reference("d")),
                new ThreeAddressCode(CodeType.Add, new Reference("T1"), new Reference("b"), new Reference("T2")),
                new ThreeAddressCode(CodeType.Move, new Reference("a"), new Reference("T1"), null),
            });


            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Complex()
        {
            var expr = new AssignmentExpression(
                new Reference("x"),
                new AddExpression(
                    new MultExpression(
                        new ReferenceExpression(new Reference("y")),
                        new MultExpression(new ReferenceExpression(new Reference("z")),
                            new ReferenceExpression(new Reference("w")))
                    ),
                    new AddExpression(new ReferenceExpression(new Reference("y")),
                        new ReferenceExpression(new Reference("x")))
                )
            );

            var sut = new CodeGenerator();
            var actual = sut.Generate(expr);
            var expected = new Code(new List<ThreeAddressCode>()
            {
                new ThreeAddressCode(CodeType.Mult, new Reference("T3"), new Reference("z"), new Reference("w")),
                new ThreeAddressCode(CodeType.Mult, new Reference("T2"), new Reference("y"), new Reference("T3")),
                new ThreeAddressCode(CodeType.Add, new Reference("T4"), new Reference("y"), new Reference("x")),
                new ThreeAddressCode(CodeType.Add, new Reference("T1"), new Reference("T2"), new Reference("T4")),
                new ThreeAddressCode(CodeType.Move, new Reference("x"), new Reference("T1"), null),
            });

            actual.ShouldDeepEqual(expected);
        }
    }
}
