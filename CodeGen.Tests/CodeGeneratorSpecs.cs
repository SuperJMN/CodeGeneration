using System.Collections.Generic;
using CodeGen.Intermediate;
using CodeGen.Units;
using CodeGen.Units.Expressions;
using CodeGen.Units.Statements;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Tests
{
    public class CodeGeneratorSpecs
    {
        [Fact]
        public void ConstantAssignment()
        {
            var st = new AssignmentStatement(new Reference("a"), new ConstantExpression(123));
            var sut = new CodeGenerator();
            var actual = sut.Generate(st);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.DirectAssignment(new Reference("T1"), 123),
                IntermediateCode.Emit.DirectAssignment(new Reference("a"), new Reference("T1")),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void SimpleAssignment()
        {
            var expr = new AssignmentStatement(
                new Reference("a"),
                new OperatorExpression(OperatorKind.Add,
                    new ReferenceExpression(new Reference("b")),
                    new OperatorExpression(OperatorKind.Mult, new ReferenceExpression(new Reference("c")),
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
        public void ComplexAssignment()
        {
            var expr = new AssignmentStatement(
                new Reference("x"),
                new OperatorExpression(OperatorKind.Add,
                    new OperatorExpression(OperatorKind.Mult,
                        new ReferenceExpression(new Reference("y")),
                        new OperatorExpression(OperatorKind.Mult, new ReferenceExpression(new Reference("z")),
                            new ReferenceExpression(new Reference("w")))
                    ),
                    new OperatorExpression(OperatorKind.Add, new ReferenceExpression(new Reference("y")),
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

        [Fact]
        public void IfSentence()
        {
            var expr = new IfStatement(new ReferenceExpression(new Reference("a")), new Block
            {
                new AssignmentStatement(new Reference("b"), new ReferenceExpression(new Reference("c"))),
            });

            var sut = new CodeGenerator();
            var actual = sut.Generate(expr);

            var label = new Label("label1");

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.JumpIfZero(new Reference("a"), label),
                IntermediateCode.Emit.DirectAssignment(new Reference("b"), new Reference("c")),
                IntermediateCode.Emit.Label(label),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void IfStatementComplexExpression()
        {
            var condition = new OperatorExpression(OperatorKind.Mult, new ReferenceExpression(new Reference("x")),
                new ReferenceExpression(new Reference("y")));

            var statement = new IfStatement(condition, new Block
            {
                new AssignmentStatement(new Reference("a"), new ReferenceExpression(new Reference("b"))),
            });

            var sut = new CodeGenerator();
            var actual = sut.Generate(statement);

            var label = new Label("label1");

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("x"), new Reference("y")),
                IntermediateCode.Emit.JumpIfZero(new Reference("T1"), label),
                IntermediateCode.Emit.DirectAssignment(new Reference("a"), new Reference("b")),
                IntermediateCode.Emit.Label(label),
            };

            actual.ShouldDeepEqual(expected);
        }
    }
}
