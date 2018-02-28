namespace CodeGen.Generation
{
    public class AssignmentExpression : Expression
    {
        public AssignmentExpression(Reference reference, Expression e)
        {
            Reference = reference;

            var code = new Code();
            code.Add(e.Code);
            code.Add(new Instruction(InstructionsTypes.Move, reference, e.Reference, null));
            Code = code;
        }
    }
}