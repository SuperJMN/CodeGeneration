namespace CodeGen.Generation
{
    public class AssignmentExpression : Expression
    {
        private readonly Reference target;
        private readonly Expression assignment;

        public AssignmentExpression(Reference target, Expression assignment) : base(target)
        {
            this.target = target;
            this.assignment = assignment;
        }

        public override Code Code
        {
            get
            {
                var code = new Code();
                code.Add(assignment.Code);
                code.Add(new ThreeAddressCode(CodeType.Move, target, assignment.Reference, null));
                return code;
            }
        }
    }
}