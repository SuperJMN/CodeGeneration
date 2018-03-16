using System;
using System.Collections.Generic;
using CodeGen.Intermediate;
using CodeGen.Intermediate.Codes;
using CodeGen.Units;
using CodeGen.Units.Expressions;
using CodeGen.Units.New.Expressions;
using DeepEqual.Syntax;
using Xunit;
using Statement = CodeGen.Units.New.Statements.Statement;

namespace CodeGen.Tests
{
    public class CodeGeneratorSpecs
    {
        [Fact]
        public void ConstantAssignment()
        {
            var st = new Units.New.Statements.AssignmentStatement(new Reference("a"), new ConstantExpression(123));
            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(st);

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
            var expr = new Units.New.Statements.AssignmentStatement(
                new Reference("a"),
                new ExpressionNode(nameof(Operators.Add),
                    new NewReferenceExpression(new Reference("b")),
                    new ExpressionNode(nameof(Operators.Multiply), new NewReferenceExpression(new Reference("c")),
                        new NewReferenceExpression(new Reference("d")))
                )
            );

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(expr);

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
            var expr = new Units.New.Statements.AssignmentStatement(
                new Reference("x"),
                new ExpressionNode(nameof(Operators.Add),
                    new ExpressionNode(nameof(Operators.Multiply),
                        new NewReferenceExpression(new Reference("y")),
                        new ExpressionNode(nameof(Operators.Multiply), new NewReferenceExpression(new Reference("z")),
                            new NewReferenceExpression(new Reference("w")))
                    ),
                    new ExpressionNode(nameof(Operators.Add), new NewReferenceExpression(new Reference("y")),
                        new NewReferenceExpression(new Reference("x")))
                )
            );

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(expr);
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
            var sut = new ExpressionNode(Operators.Eq, new NewReferenceExpression(new Reference("a")), new NewReferenceExpression(new Reference("b")));

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

            var statement = new Units.New.Statements.AssignmentStatement(new Reference("b"), new NewReferenceExpression(new Reference("c")));

            var expr = new Units.New.Statements.IfStatement(new ConstantExpression(true),
                new Units.New.Statements.Block(new List<Statement>() { statement }));

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
            var left = new ExpressionNode(nameof(Operators.Multiply), new NewReferenceExpression(new Reference("x")), new NewReferenceExpression(new Reference("y")));
            var right = new NewReferenceExpression(new Reference("z"));
            var condition = new ExpressionNode(nameof(Operators.Eq), left, right);
            
            var statement = new Units.New.Statements.IfStatement(condition, new Units.New.Statements.Block
            {
                new Units.New.Statements.AssignmentStatement(new Reference("a"), new NewReferenceExpression(new Reference("b"))),
            });

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(statement);

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
            var actual = sut.Generate(expr);
            return actual;
        }
    }
}
