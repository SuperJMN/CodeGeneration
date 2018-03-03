using System;

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
                Destination = destination,
            };
        }

        private IntermediateCode()
        {            
        }

        public Reference Right { get; private set; }

        public Reference Left { get; private set; }

        public Reference Destination { get; private set; }

        public Label Label { get; private set; }

        public IntermediateCodeType Instruction { get; private set; }

        public override string ToString()
        {
            switch (Instruction)
            {
                case IntermediateCodeType.Mult:
                    return $"{Destination} = {Left} * {Right}";

                case IntermediateCodeType.Add:
                    return $"{Destination} = {Left} + {Right}";

                case IntermediateCodeType.Move:
                    return $"{Destination} = {Left}";

                case IntermediateCodeType.JumpIfZero:
                    return $"if {Destination} == 0 go to {Label}";

                case IntermediateCodeType.Label:
                    return $"{Label}:";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

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

            public static IntermediateCode DirectAssignment(Reference destination, Reference left)
            {
                return Standard(IntermediateCodeType.Move, destination, left);
            }

            public static IntermediateCode JumpIfZero(Reference reference, Label label)
            {
                return new IntermediateCode
                {
                    Instruction = IntermediateCodeType.JumpIfZero,
                    Destination = reference,
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
        }
    }
}