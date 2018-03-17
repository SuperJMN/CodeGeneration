using System.Collections.Generic;
using CodeGen.Ast.Units;
using CodeGen.Ast.Units.Expressions;
using CodeGen.Ast.Units.Statements;
using CodeGen.Core;
using CodeGen.Intermediate.Codes;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Intermediate.Tests
{
    public class CodeGeneratorSpecs
    {
        [Fact]
        public void ConstantAssignment()
        {
            var st = new AssignmentStatement(new Reference("a"), new ConstantExpression(123));
            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(new[] {st});

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set(new Reference("T1"), 123),
                IntermediateCode.Emit.Set(new Reference("a"), new Reference("T1")),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void SimpleAssignment()
        {
            var expr = new AssignmentStatement(
                new Reference("a"),
                new ExpressionNode(nameof(Operators.Add),
                    new ReferenceExpression(new Reference("b")),
                    new ExpressionNode(nameof(Operators.Multiply), new ReferenceExpression(new Reference("c")),
                        new ReferenceExpression(new Reference("d")))
                )
            );

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(new[] {expr});

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("c"), new Reference("d")),
                IntermediateCode.Emit.Add(new Reference("T2"), new Reference("b"), new Reference("T1")),
                IntermediateCode.Emit.Set(new Reference("a"), new Reference("T2")),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void ComplexAssignment()
        {
            var expr = new AssignmentStatement(
                new Reference("x"),
                new ExpressionNode(nameof(Operators.Add),
                    new ExpressionNode(nameof(Operators.Multiply),
                        new ReferenceExpression(new Reference("y")),
                        new ExpressionNode(nameof(Operators.Multiply), new ReferenceExpression(new Reference("z")),
                            new ReferenceExpression(new Reference("w")))
                    ),
                    new ExpressionNode(nameof(Operators.Add), new ReferenceExpression(new Reference("y")),
                        new ReferenceExpression(new Reference("x")))
                )
            );

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(new[] {expr});
            var expected = new List<IntermediateCode>()
            {
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("z"), new Reference("w")),
                IntermediateCode.Emit.Mult(new Reference("T2"), new Reference("y"), new Reference("T1")),
                IntermediateCode.Emit.Add(new Reference("T3"), new Reference("y"), new Reference("x")),
                IntermediateCode.Emit.Add(new Reference("T4"), new Reference("T2"), new Reference("T3")),
                IntermediateCode.Emit.Set(new Reference("x"), new Reference("T4")),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void BinaryBooleanExpression()
        {
            var sut = new ExpressionNode(Operators.Eq, new ReferenceExpression(new Reference("a")), new ReferenceExpression(new Reference("b")));

            var actual = Generate(sut);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.IsEqual(new Reference("T1"), new Reference("a"), new Reference("b")),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void IfSentence()
        {

            var statement = new AssignmentStatement(new Reference("b"), new ReferenceExpression(new Reference("c")));

            var expr = new IfStatement(new ConstantExpression(true),
                new Block(new List<Statement> { statement }));

            var actual = Generate(expr);

            var label = new Label("label1");

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set(new Reference("T1"), true),
                IntermediateCode.Emit.JumpIfFalse(new Reference("T1"), label),
                IntermediateCode.Emit.Set(new Reference("b"), new Reference("c")),
                IntermediateCode.Emit.Label(label),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void IfStatementComplexExpression()
        {
            var left = new ExpressionNode(nameof(Operators.Multiply), new ReferenceExpression(new Reference("x")), new ReferenceExpression(new Reference("y")));
            var right = new ReferenceExpression(new Reference("z"));
            var condition = new ExpressionNode(nameof(Operators.Eq), left, right);
            
            var statement = new IfStatement(condition, new Block(new AssignmentStatement(new Reference("a"), new ReferenceExpression(new Reference("b")))));

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(new[] {statement});

            var label = new Label("label1");

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("x"), new Reference("y")),
                IntermediateCode.Emit.IsEqual(new Reference("T2"), new Reference("T1"), new Reference("z")),
                IntermediateCode.Emit.JumpIfFalse(new Reference("T2"), label),
                IntermediateCode.Emit.Set(new Reference("a"), new Reference("b")),
                IntermediateCode.Emit.Label(label),
            };

            actual.ShouldDeepEqual(expected);
        }

        private static IReadOnlyCollection<IntermediateCode> Generate(ICodeUnit expr)
        {
            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(new[] {expr});
            return actual;
        }
    }
}
