using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Intermediate.Codes;
using CodeGen.Intermediate.Codes.Common;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
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
            var actual = sut.Generate(st);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set(new Reference("T1"), 123),
                IntermediateCode.Emit.Set(new Reference("a"), new Reference("T1"))
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void Program()
        {
            //int add(int a, int b)
            //{
            //    int c = a + b;
            //    return c;
            //}

            //void main()
            //{
            //    int r = add(1, 2);
            //}

            var addFunc = new Function("add", VariableType.Int, new List<Argument>
            {
                new Argument(VariableType.Int, "a"),
                new Argument(VariableType.Int, "b"),
            }, new Block(new List<Statement>()
            {
                new AssignmentStatement("c", new ExpressionNode(Operator.Add, (ReferenceExpression)"a", (ReferenceExpression)"b")),
                new ReturnStatement("c")
            }, new List<DeclarationStatement>()));

            var mainFunc = new Function("main", VariableType.Void, new List<Argument>(), new Block(new List<Statement>()
            {
                new Call("add", "r", new ConstantExpression(1), new ConstantExpression(2)),
            }, new List<DeclarationStatement>()));

            var ast = new Program(new[]
            {
                addFunc,
                mainFunc,
            });

            var actual = new IntermediateCodeGenerator().Generate(ast);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Call("main"),

                // comienzo función "add"
                IntermediateCode.Emit.FunctionDefinition(addFunc),
                IntermediateCode.Emit.Add("T1", "a", "b"),
                IntermediateCode.Emit.Set("c", "T1"),
                IntermediateCode.Emit.Return("c"),

                //main
                IntermediateCode.Emit.FunctionDefinition(mainFunc),
                IntermediateCode.Emit.Set("T2", 1),
                IntermediateCode.Emit.Parameter("T2"),
                IntermediateCode.Emit.Set("T3", 2),
                IntermediateCode.Emit.Parameter("T3"),
                IntermediateCode.Emit.Call("add", "T4"),
                IntermediateCode.Emit.Set("r", "T4"),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void SimpleAssignment()
        {
            var expr = new AssignmentStatement(
                new Reference("a"),
                new ExpressionNode(nameof(Operator.Add),
                    new ReferenceExpression("b"),
                    new ExpressionNode(nameof(Operator.Multiply), new ReferenceExpression("c"),
                        new ReferenceExpression("d"))
                )
            );

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(expr);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("c"), new Reference("d")),
                IntermediateCode.Emit.Add(new Reference("T2"), new Reference("b"), new Reference("T1")),
                IntermediateCode.Emit.Set(new Reference("a"), new Reference("T2"))
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void ComplexAssignment()
        {
            var expr = new AssignmentStatement(
                new Reference("x"),
                new ExpressionNode(nameof(Operator.Add),
                    new ExpressionNode(nameof(Operator.Multiply),
                        new ReferenceExpression("y"),
                        new ExpressionNode(nameof(Operator.Multiply), new ReferenceExpression("z"),
                            new ReferenceExpression("w"))
                    ),
                    new ExpressionNode(nameof(Operator.Add), new ReferenceExpression("y"),
                        new ReferenceExpression("x"))
                )
            );

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(expr);
            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("z"), new Reference("w")),
                IntermediateCode.Emit.Mult(new Reference("T2"), new Reference("y"), new Reference("T1")),
                IntermediateCode.Emit.Add(new Reference("T3"), new Reference("y"), new Reference("x")),
                IntermediateCode.Emit.Add(new Reference("T4"), new Reference("T2"), new Reference("T3")),
                IntermediateCode.Emit.Set(new Reference("x"), new Reference("T4"))
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void IsEqual()
        {
            TestBooleanOperation(BooleanOperation.IsEqual);
        }

        [Fact]
        public void IsLessThan()
        {
            TestBooleanOperation(BooleanOperation.IsLessThan);
        }

        [Fact]
        public void IsGreaterThan()
        {
            TestBooleanOperation(BooleanOperation.IsGreaterThan);
        }

        private static void TestBooleanOperation(BooleanOperation booleanOperation)
        {
            var sut = new ExpressionNode(booleanOperation.ToOperatorName(), new ReferenceExpression("a"),
                new ReferenceExpression("b"));

            var actual = Generate(sut);

            var expected = new List<IntermediateCode>
            {
                new BoolExpressionAssignment(booleanOperation, new Reference("T1"), new Reference("a"),
                    new Reference("b"))
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void IfSentence()
        {
            var statement = new AssignmentStatement(new Reference("b"), new ReferenceExpression("c"));

            var expr = new IfStatement(new ConstantExpression(true),
                new Block(new List<Statement> {statement}));

            var actual = Generate(expr);

            var label = new Label("label1");

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set(new Reference("T1"), true),
                IntermediateCode.Emit.JumpIfFalse(new Reference("T1"), label),
                IntermediateCode.Emit.Set(new Reference("b"), new Reference("c")),
                IntermediateCode.Emit.Label(label)
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void IfStatementComplexExpression()
        {
            var left = new ExpressionNode(nameof(Operator.Multiply), new ReferenceExpression("x"),
                new ReferenceExpression("y"));
            var right = new ReferenceExpression("z");
            var condition = new ExpressionNode(nameof(Operator.Eq), left, right);

            var statement = new IfStatement(condition,
                new Block(new AssignmentStatement(new Reference("a"), new ReferenceExpression("b"))));

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(statement);

            var label = new Label("label1");

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult(new Reference("T1"), new Reference("x"), new Reference("y")),
                IntermediateCode.Emit.IsEqual(new Reference("T2"), new Reference("T1"), new Reference("z")),
                IntermediateCode.Emit.JumpIfFalse(new Reference("T2"), label),
                IntermediateCode.Emit.Set(new Reference("a"), new Reference("b")),
                IntermediateCode.Emit.Label(label)
            };

            actual.ShouldDeepEqual(expected);
        }

        private static IReadOnlyCollection<IntermediateCode> Generate(ICodeUnit unit)
        {
            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(unit);
            return actual.ToList().AsReadOnly();
        }
    }
}