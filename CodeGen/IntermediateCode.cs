using System;
using CodeGen.Units;
using CodeGen.Units.Expressions;

namespace CodeGen.Intermediate
{
    public class IntermediateCode
    {
        private static IntermediateCode Standard(IntermediateCodeType type, Reference destination, Reference left,
            Reference right = null)
        {
            return new IntermediateCode
            {
                Instruction = type,
                Left = left,
                Right = right,
                Target = destination,
            };
        }

        private IntermediateCode()
        {
        }

        public Reference Right { get; private set; }

        public Reference Left { get; private set; }

        public Reference Target { get; private set; }

        public Label Label { get; private set; }

        public IntermediateCodeType Instruction { get; private set; }

        public static class Emit
        {
            public static IntermediateCode Add(Reference destination, Reference left, Reference right)
            {
                return Standard(IntermediateCodeType.Add, destination, left, right);
            }

            public static IntermediateCode Mult(Reference destination, Reference left, Reference right)
            {
                return Standard(IntermediateCodeType.Mult, destination, left, right);
            }

            public static IntermediateCode JumpIfFalse(Reference reference, Label label)
            {
                return new IntermediateCode
                {
                    Instruction = IntermediateCodeType.JumpOnNotZero,
                    Target = reference,
                    Label = label,
                };
            }

            public static IntermediateCode Label(Label label)
            {
                return new IntermediateCode
                {
                    Instruction = IntermediateCodeType.Label,
                    Label = label,
                };
            }

            public static IntermediateCode Constant(Reference reference, int value)
            {
                return new IntermediateCode
                {
                    Instruction = IntermediateCodeType.Move,
                    Target = reference,
                    Value = value,
                };
            }

            public static IntermediateCode Set(Reference destination, Reference left)
            {
                return Standard(IntermediateCodeType.Move, destination, left);
            }

            public static IntermediateCode Set(Reference destination, int value)
            {
                return new IntermediateCode
                {
                    Instruction = IntermediateCodeType.Move,
                    Target = destination,
                    Value = value,
                };
            }

            public static IntermediateCode Set(Reference target, bool value)
            {
                return new IntermediateCode
                {
                    Instruction = IntermediateCodeType.Move,
                    Target = target,
                    Value = value ? BooleanValue.True.Value : BooleanValue.False.Value,
                };
            }

            public static IntermediateCode CmpEquals(Reference target, Reference left, Reference right)
            {
                return new IntermediateCode
                {
                    Instruction = IntermediateCodeType.Cmp,
                    Target = target,
                    Left = left,
                    Right = right,
                };
            }
        }

        public override string ToString()
        {
            return Formatter.Format(this);
        }

        public int Value { get; set; }

        private static class Formatter
        {
            public static string Format(IntermediateCode intermediateCode)
            {
                switch (intermediateCode.Instruction)
                {
                    case IntermediateCodeType.Mult:
                        return $"{intermediateCode.Target} = {intermediateCode.Left} * {intermediateCode.Right}";

                    case IntermediateCodeType.Add:
                        return $"{intermediateCode.Target} = {intermediateCode.Left} + {intermediateCode.Right}";

                    case IntermediateCodeType.Move:
                        if (intermediateCode.Left != null)
                        {
                            return $"{intermediateCode.Target} = {intermediateCode.Left}";
                        }
                        else
                        {
                            return $"{intermediateCode.Target} = {intermediateCode.Value}";
                        }

                    case IntermediateCodeType.JumpOnNotZero:
                        return $"if {intermediateCode.Target} == 0 continue. Otherwise go to {intermediateCode.Label}";

                    case IntermediateCodeType.Label:
                        return $"{intermediateCode.Label}:";

                    case IntermediateCodeType.Cmp:
                        return $"{intermediateCode.Target} = {intermediateCode.Left} == {intermediateCode.Right}";

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }    
}