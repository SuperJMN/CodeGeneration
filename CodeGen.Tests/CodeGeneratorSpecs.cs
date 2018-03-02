using System.Collections.Generic;
using CodeGen.Intermediate;
using CodeGen.Intermediate.Units.Expressions;
using CodeGen.Intermediate.Units.Sentences;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Tests
{
    public class CodeGeneratorSpecs
    {
        [Fact]
        public void Assignment()
        {
            var expr = new AssignmentSentence(
                new Reference("a"),
                new AddExpression(
                    new ReferenceExpression(new Reference("b")),
                    new MultExpression(new ReferenceExpression(new Reference("c")),
                        new ReferenceExpression(new Reference("d")))
                )
            );

            var sut = new CodeGenerator();
            var actual = sut.Generate(expr);

            var expected = new List<IntermediateCode>()
            {
                new IntermediateCode(IntermediateCodeType.Mult, new Reference("T1"), new Reference("c"), new Reference("d")),
                new IntermediateCode(IntermediateCodeType.Add, new Reference("T2"), new Reference("b"), new Reference("T1")),
                new IntermediateCode(IntermediateCodeType.Move, new Reference("a"), new Reference("T2"), null),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Complex()
        {
            var expr = new AssignmentSentence(
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
            var expected = new List<IntermediateCode>()
            {
                new IntermediateCode(IntermediateCodeType.Mult, new Reference("T1"), new Reference("z"), new Reference("w")),
                new IntermediateCode(IntermediateCodeType.Mult, new Reference("T2"), new Reference("y"), new Reference("T1")),
                new IntermediateCode(IntermediateCodeType.Add, new Reference("T3"), new Reference("y"), new Reference("x")),
                new IntermediateCode(IntermediateCodeType.Add, new Reference("T4"), new Reference("T2"), new Reference("T3")),
                new IntermediateCode(IntermediateCodeType.Move, new Reference("x"), new Reference("T4"), null),
            };          

            actual.ShouldDeepEqual(expected);
        }
    }
}
