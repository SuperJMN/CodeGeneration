using CodeGen.Intermediate.Codes;

namespace CodeGen.Intermediate
{
    public interface IIntermediateCodeVisitor
    {
        void Visit(JumpIfFalse code);
        void Visit(BoolConstantAssignment code);
        void Visit(LabelCode jumpIfFalse);
        void Visit(IntegerConstantAssignment jumpIfFalse);
        void Visit(OperationAssignment jumpIfFalse);
        void Visit(ReferenceAssignment jumpIfFalse);
        void Visit(BoolExpressionAssignment boolExpressionAssignment);
    }
}