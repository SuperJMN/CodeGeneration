using CodeGen.Intermediate.Codes;

namespace CodeGen.Intermediate
{
    public interface IIntermediateCodeVisitor
    {
        void Visit(JumpIfFalse code);
        void Visit(BoolConstantAssignment code);
        void Visit(LabelCode code);
        void Visit(IntegerConstantAssignment code);
        void Visit(ArithmeticAssignment code);
        void Visit(ReferenceAssignment code);
        void Visit(BoolExpressionAssignment code);
    }
}