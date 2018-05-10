using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing.Tests
{
    public static class CodeUnitMixin
    {
        public static ICodeUnit WithAssignedNames(this ICodeUnit unit)
        {
            var nameAssigner = new ImplicitReferenceNameAssigner();
            nameAssigner.AssignNames(unit);

            return unit;
        }
    }
}