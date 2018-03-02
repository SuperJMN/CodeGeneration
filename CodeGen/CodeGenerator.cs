using System.Collections.Generic;
using System.Linq;
using CodeGen.Intermediate.Expressions;

namespace CodeGen.Intermediate
{
    public class CodeGenerator
    {
        private int implicitReferenceCount;

        public IReadOnlyCollection<IntermediateCode> Generate(Expression expression)
        {
            implicitReferenceCount = 0;

            var codeGeneratingVisitor = new CodeGeneratingVisitor();
            expression.Accept(codeGeneratingVisitor);

            var referenceExtractor = new ReferenceExtractorVisitor();
            expression.Accept(referenceExtractor);

            AssignIdentifiersToImplicityReferences(referenceExtractor.References);

            return codeGeneratingVisitor.Code;
        }

        private void AssignIdentifiersToImplicityReferences(IEnumerable<Reference> referenceExtractorReferences)
        {
            var noIdentifiers = referenceExtractorReferences
                .Where(r => r.Identifier == null)
                .Distinct()
                .ToList();

            noIdentifiers.ForEach(r => r.Identifier = GetNewIdentifier());
        }

        private string GetNewIdentifier()
        {
            return "T" + ++implicitReferenceCount;
        }
    }
}