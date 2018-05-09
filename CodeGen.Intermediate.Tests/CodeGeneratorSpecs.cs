using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Intermediate.Codes;
using CodeGen.Intermediate.Codes.Common;
using CodeGen.Parsing;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using DeepEqual.Syntax;
using Xunit;

namespace CodeGen.Intermediate.Tests
{
    public class CodeGeneratorSpecs
    {
        private static void TestBooleanOperation(BooleanOperation booleanOperation)
        {
            var sut = new ExpressionNode(booleanOperation.ToOperatorName(), new ReferenceExpression("a"),
                new ReferenceExpression("b"));

            var actual = Generate(sut);

            var expected = new List<IntermediateCode>
            {
                new BoolExpressionAssignment(booleanOperation, "T1", "a",
                    "b")
            };

            actual.ShouldDeepEqual(expected);
        }

        private static IReadOnlyCollection<IntermediateCode> Generate(ICodeUnit unit)
        {
            var namer = new ImplicitReferenceNameAssigner();
            namer.AssignNames(unit);

            var sut = new IntermediateCodeGenerator();
            var actual = sut.Generate(unit);
            
            return actual.ToList().AsReadOnly();
        }

        [Fact]
        public void ComplexAssignment()
        {
            var expr = new AssignmentStatement(
                "x",
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

            var actual = Generate(expr);
            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult("T1", "z", "w"),
                IntermediateCode.Emit.Mult("T2", "y", "T1"),
                IntermediateCode.Emit.Add("T3", "y", "x"),
                IntermediateCode.Emit.Add("T4", "T2", "T3"),
                IntermediateCode.Emit.Set("x", "T4")
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void ConstantAssignment()
        {
            var st = new AssignmentStatement("a", new ConstantExpression(123));
            var actual = Generate(st);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set("T1", 123),
                IntermediateCode.Emit.Set("a", "T1")
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void IfSentence()
        {
            var statement = new AssignmentStatement("b", new ReferenceExpression("c"));

            var expr = new IfStatement(new ConstantExpression(true),
                new Block(new List<Statement> {statement}));

            var actual = Generate(expr);

            var label = new Label("label1");

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set("T1", true),
                IntermediateCode.Emit.JumpIfFalse("T1", label),
                IntermediateCode.Emit.Set("b", "c"),
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
                new Block(new AssignmentStatement("a", new ReferenceExpression("b"))));

            var actual = Generate(statement);

            var label = new Label("label1");

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult("T1", "x", "y"),
                IntermediateCode.Emit.IsEqual("T2", "T1", "z"),
                IntermediateCode.Emit.JumpIfFalse("T2", label),
                IntermediateCode.Emit.Set("a", "b"),
                IntermediateCode.Emit.Label(label)
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void IsEqual()
        {
            TestBooleanOperation(BooleanOperation.IsEqual);
        }

        [Fact]
        public void IsGreaterThan()
        {
            TestBooleanOperation(BooleanOperation.IsGreaterThan);
        }

        [Fact]
        public void IsLessThan()
        {
            TestBooleanOperation(BooleanOperation.IsLessThan);
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

            var i = PrimitiveType.Int;
            var addFunc = new Function(new FunctionFirm("add", ReturnType.Int, new List<Argument>
            {
                new Argument(i, "a"),
                new Argument(i, "b")
            }), new Block(new List<Statement>
            {
                new AssignmentStatement("c",
                    new ExpressionNode(Operator.Add, (ReferenceExpression) "a", (ReferenceExpression) "b")),
                new ReturnStatement(new ReferenceExpression("c"))
            }, new List<DeclarationStatement>()));

            var mainFunc = new Function(new FunctionFirm("main", ReturnType.Void, new List<Argument>()), new Block(
                new List<Statement>
                {
                    new AssignmentStatement("r", new Call("add", new ConstantExpression(1), new ConstantExpression(2)))
                }, new List<DeclarationStatement>()));

            var ast = new Program(new[]
            {
                addFunc,
                mainFunc
            });

            var actual = Generate(ast);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Call("main"),
                IntermediateCode.Emit.Halt(),

                // comienzo función "add"
                IntermediateCode.Emit.FunctionDefinition(addFunc.Firm),
                IntermediateCode.Emit.Add("T1", "a", "b"),
                IntermediateCode.Emit.Set("c", "T1"),
                IntermediateCode.Emit.Return("c"),

                //main
                IntermediateCode.Emit.FunctionDefinition(mainFunc.Firm),
                IntermediateCode.Emit.Set("T2", 1),
                IntermediateCode.Emit.Parameter("T2"),
                IntermediateCode.Emit.Set("T3", 2),
                IntermediateCode.Emit.Parameter("T3"),
                IntermediateCode.Emit.Call("add", "T4"),
                IntermediateCode.Emit.Set("r", "T4"),
                IntermediateCode.Emit.Return(),
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void SimpleAssignment()
        {
            var expr = new AssignmentStatement(
                new StandardReferenceItem(new Reference("a")), 
                new ExpressionNode(nameof(Operator.Add),
                    new ReferenceExpression("b"),
                    new ExpressionNode(nameof(Operator.Multiply), new ReferenceExpression("c"),
                        new ReferenceExpression("d"))
                )
            );

            var actual = Generate(expr);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Mult("T1", "c", "d"),
                IntermediateCode.Emit.Add("T2", "b", "T1"),
                IntermediateCode.Emit.Set("a", "T2")
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void LoadFromArray()
        {
            var expr = new AssignmentStatement(new StandardReferenceItem(new Reference("a")), new ArrayReferenceItem("b", new ConstantExpression(10)));
            var actual = Generate(expr);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set("T1", 10),
                IntermediateCode.Emit.LoadFromArray("T2", "b", "T1"),
                IntermediateCode.Emit.Set("a", "T2")
            };

            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void StoreToArray()
        {
            var target = new ArrayReferenceItem("a", new ConstantExpression(10));
            var expr = new AssignmentStatement(target, (StandardReferenceItem)"b");
            var actual = Generate(expr);

            var expected = new List<IntermediateCode>
            {
                IntermediateCode.Emit.Set("T1", 10),
                IntermediateCode.Emit.StoreToArray("a", "T1", "b"),
            };

            actual.ShouldDeepEqual(expected);
        }       
    }
}