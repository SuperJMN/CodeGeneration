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
        void Visit(Jump code);
        void Visit(FunctionDefinitionCode code);
        void Visit(CallCode code);
        void Visit(ReturnCode code);
        void Visit(HaltCode code);
        void Visit(ParameterCode code);
    }
}
