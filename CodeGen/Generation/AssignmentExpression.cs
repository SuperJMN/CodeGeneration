namespace CodeGen.Generation
{
    public class AssignmentExpression : Expression
    {
        private readonly Reference target;
        private readonly Expression assignment;

        public AssignmentExpression(Reference target, Expression assignment)
        {
            this.target = target;
            this.assignment = assignment;
        }

        public override Code Code
        {
            get
            {
                Reference = target;

                var code = new Code();
                code.Add(assignment.Code);
                code.Add(new ThreeAddressCode(CodeType.Move, target, assignment.Reference, null));
                return code;
            }
        }
    }
}