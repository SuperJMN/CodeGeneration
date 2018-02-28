namespace CodeGen.Generation
{
    public class AddExpression : Expression
    {
        public AddExpression(Expression left, Expression right)
        {
            Reference = new Reference();

            var code = new Code();
            code.Add(left.Code);
            code.Add(right.Code);
            code.Add(new Instruction(InstructionsTypes.Add, Reference, left.Reference, right.Reference));
            Code = code;
        }
    }
}