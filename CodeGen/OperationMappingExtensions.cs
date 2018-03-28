using System;
using CodeGen.Ast.Units;
using CodeGen.Intermediate.Codes.Common;

namespace CodeGen.Intermediate
{
    public static class OperationMappingExtensions
    {
        public static string ToOperatorName(this BooleanOperation op)
        {
            if (op == BooleanOperation.IsEqual)
            {
                return Operators.Eq;
            }
            if (op == BooleanOperation.IsLessThan)
            {
                return Operators.Lt;
            }
            if (op == BooleanOperation.IsGreaterThan)
            {
                return Operators.Gt;
            }

            throw new ArgumentOutOfRangeException(nameof(op));
        }

        public static ArithmeticOperator ToArithmeticOperator(this string op)
        {
            switch (op)
            {
                case Operators.Add:
                    return ArithmeticOperator.Add;
                case Operators.Multiply:
                    return ArithmeticOperator.Mult;
                case Operators.Subtract:
                    return ArithmeticOperator.Substract;
            }

            throw new ArgumentOutOfRangeException(nameof(op));
        }

        public static BooleanOperation ToBooleanOperator(this string op)
        {
            switch (op)
            {
                case Operators.Eq:
                    return  BooleanOperation.IsEqual;
                case Operators.Lt:
                    return  BooleanOperation.IsLessThan;
                case Operators.Gt:
                    return  BooleanOperation.IsGreaterThan;
                case Operators.Gte:
                    return  BooleanOperation.IsGreaterOrEqual;
                case Operators.Lte:
                    return  BooleanOperation.IsLessOrEqual;
            }

            throw new ArgumentOutOfRangeException(nameof(op));
        }
    }


}