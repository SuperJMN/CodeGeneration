using System;
using CodeGen.Units;

namespace CodeGen.Intermediate
{
    public class IntermediateCode
    {
        private IntermediateCode()
        {
        }

        private static IntermediateCode Standard(IntermediateCodeType type, Reference destination, Reference left,
            Reference right = null)
        {
            return new IntermediateCode
            {
                Type = type,
                Left = left,
                Right = right,
                Target = destination,
            };
        }

        public Reference Right { get; private set; }

        public Reference Left { get; private set; }

        public Reference Target { get; private set; }

        public Label Label { get; private set; }

        public IntermediateCodeType Type { get; private set; }

        public static class Emit
        {
            public static IntermediateCode Add(Reference destination, Reference left, Reference right)
            {
                return Standard(IntermediateCodeType.Add, destination, left, right);
            }

            public static IntermediateCode IsEqual(Reference destination, Reference left, Reference right)
            {
                return Standard(IntermediateCodeType.IsEqual, destination, left, right);
            }

            public static IntermediateCode Mult(Reference destination, Reference left, Reference right)
            {
                return Standard(IntermediateCodeType.Mult, destination, left, right);
            }

            public static IntermediateCode JumpIfFalse(Reference reference, Label label)
            {
                return new IntermediateCode
                {
                    Type = IntermediateCodeType.JumpIfFalse,
                    Target = reference,
                    Label = label,
                };
            }

            public static IntermediateCode Label(Label label)
            {
                return new IntermediateCode
                {
                    Type = IntermediateCodeType.Label,
                    Label = label,
                };
            }

            public static IntermediateCode Constant(Reference reference, int value)
            {
                return new IntermediateCode
                {
                    Type = IntermediateCodeType.Move,
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
                    Type = IntermediateCodeType.Move,
                    Target = destination,
                    Value = value,
                };
            }

            public static IntermediateCode Set(Reference target, bool value)
            {
                return new IntermediateCode
                {
                    Type = IntermediateCodeType.Move,
                    Target = target,
                    BooleanValue = value,
                };
            }
        }

        public bool? BooleanValue { get; set; }

        public override string ToString()
        {
            return Formatter.Format(this);
        }

        public int Value { get; set; }

        private static class Formatter
        {
            public static string Format(IntermediateCode intermediateCode)
            {
                switch (intermediateCode.Type)
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
                        else if (intermediateCode.BooleanValue.HasValue)
                        {
                            return $"{intermediateCode.Target} = {intermediateCode.BooleanValue}";
                        }
                        else
                        {
                            return $"{intermediateCode.Target} = {intermediateCode.Value}";
                        }

                    case IntermediateCodeType.JumpOnNotZero:
                        return $"if {intermediateCode.Target} == 0 continue. Otherwise go to {intermediateCode.Label}";

                    case IntermediateCodeType.Label:
                        return $"{intermediateCode.Label}:";


                    case IntermediateCodeType.IsEqual:
                        return "a == b";

                    case IntermediateCodeType.JumpIfFalse:
                        return $"if {intermediateCode.Target} is true continue. Otherwise go to {intermediateCode.Label}";

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }    
}