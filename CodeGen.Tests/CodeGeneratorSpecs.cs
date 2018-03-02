using System.Collections.Generic;
using CodeGen.Intermediate;
using CodeGen.Intermediate.Units.Expressions;
using CodeGen.Intermediate.Units.Statements;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Tests
{
    public class CodeGeneratorSpecs
    {
        [Fact]
        public void Assignment()
        {
            var expr = new AssignmentStatement(
                new Reference("a"),
                new AddExpression(
                    new ReferenceExpression(new Reference("b")),
                    new MultExpression(new ReferenceExpression(new Reference("c")),
                        new ReferenceExpression(new Reference("d")))
                )
            );

            var sut = new CodeGenerator();
            var actual = sut.Generate(expr);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("c"), new Reference("d")),
                IntermediateCode.Emit.Add(new Reference("T2"), new Reference("b"), new Reference("T1")),
                IntermediateCode.Emit.DirectAssignment(new Reference("a"), new Reference("T2")),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Complex()
        {
            var expr = new AssignmentStatement(
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
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("z"), new Reference("w")),
                IntermediateCode.Emit.Mult(new Reference("T2"), new Reference("y"), new Reference("T1")),
                IntermediateCode.Emit.Add(new Reference("T3"), new Reference("y"), new Reference("x")),
                IntermediateCode.Emit.Add(new Reference("T4"), new Reference("T2"), new Reference("T3")),
                IntermediateCode.Emit.DirectAssignment(new Reference("x"), new Reference("T4")),
            };          

            actual.ShouldDeepEqual(expected);
        }
    }
}
