using System.Collections.Generic;
using System.Linq;
using CodeGen.Units;

namespace CodeGen.Intermediate
{
    public class IntermediateCodeGenerator
    {
        private int implicitReferenceCount;
        private int labelCount;

        public IReadOnlyCollection<IntermediateCode> Generate(ICodeUnit expression)
        {
            implicitReferenceCount = 0;
            labelCount = 0;

            var codeGeneratingVisitor = new CodeGeneratingVisitor();
            expression.Accept(codeGeneratingVisitor);

            AssignIdentifiersToImplicityReferences(codeGeneratingVisitor.Code);
            AssignIdentifiersToLabels(codeGeneratingVisitor.Code);
            
            return codeGeneratingVisitor.Code;
        }

        private void AssignIdentifiersToLabels(IEnumerable<IntermediateCode> code)
        {
            //var labels = code
            //    .Select(x => x.Label)
            //    .Where(r => r != null && r.Name == null)
            //    .Distinct()
            //    .ToList();

            //labels.ForEach(r => r.Name = GetNewLabelName());
        }

        private void AssignIdentifiersToImplicityReferences(IEnumerable<IntermediateCode> code)
        {
            //var noIdentifiers = code
            //    .SelectMany(x => new List<Reference> { x.Left, x.Right, x.Target })
            //    .Where(r => r != null && r.Identifier == null)
            //    .Distinct()
            //    .ToList();

            //noIdentifiers.ForEach(r => r.Identifier = GetNewIdentifier());
        }

        private string GetNewIdentifier()
        {
            return "T" + ++implicitReferenceCount;
        }

        private string GetNewLabelName()
        {
            return "label" + ++labelCount;
        }
    }
}