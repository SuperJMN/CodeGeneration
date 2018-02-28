namespace CodeGen.Generation
{
    public class MultExpression : Expression
    {
        public MultExpression(Expression i, Expression d)
        {
            Reference = new Reference();
            var code = new Code();
            code.Add(i.Code);
            code.Add(d.Code);
            code.Add(new Instruction(InstructionsTypes.Mult, Reference, i.Reference, d.Reference));
            Code = code;
        }
    }
}