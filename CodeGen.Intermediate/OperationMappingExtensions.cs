using System;
using CodeGen.Intermediate.Codes.Common;
using CodeGen.Parsing.Ast;

namespace CodeGen.Intermediate
{
    public static class OperationMappingExtensions
    {
        public static string ToOperatorName(this BooleanOperation op)
        {
            if (op == BooleanOperation.IsEqual)
            {
                return Operator.Eq;
            }
            if (op == BooleanOperation.IsLessThan)
            {
                return Operator.Lt;
            }
            if (op == BooleanOperation.IsGreaterThan)
            {
                return Operator.Gt;
            }

            throw new ArgumentOutOfRangeException(nameof(op));
        }

        public static ArithmeticOperator ToArithmeticOperator(this string op)
        {
            switch (op)
            {
                case Operator.Add:
                    return ArithmeticOperator.Add;
                case Operator.Multiply:
                    return ArithmeticOperator.Mult;
                case Operator.Subtract:
                    return ArithmeticOperator.Substract;
            }

            throw new ArgumentOutOfRangeException(nameof(op));
        }

        public static BooleanOperation ToBooleanOperator(this string op)
        {
            switch (op)
            {
                case Operator.Eq:
                    return BooleanOperation.IsEqual;
                case Operator.Lt:
                    return BooleanOperation.IsLessThan;
                case Operator.Gt:
                    return BooleanOperation.IsGreaterThan;
                case Operator.Gte:
                    return BooleanOperation.IsGreaterOrEqual;
                case Operator.Lte:
                    return BooleanOperation.IsLessOrEqual;
                case Operator.And:
                    return BooleanOperation.And;
                case Operator.Or:
                    return BooleanOperation.Or;
            }

            throw new ArgumentOutOfRangeException(nameof(op));
        }
    }


}