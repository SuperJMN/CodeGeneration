using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Parsing.Ast;

namespace CodeGen.Parsing
{
    public class ImplicitReferenceNameAssigner
    {
        private int implicitReferenceCount;

        public void AssignNames(ICodeUnit unit)
        {
            var scanner = new ReferenceScanner();
            unit.Accept(scanner);

            var references = scanner.References.Distinct();
            AssignIdentifiersToImplicityReferences(references);
        }

        private void AssignIdentifiersToImplicityReferences(IEnumerable<Reference> references)
        {
            var toGiveName = references
                .Where(r => r.Identifier == null)
                .Distinct()
                .ToList();

            foreach (var r in toGiveName)
            {
                r.Identifier = GetNewIdentifier();
            }
        }

        private string GetNewIdentifier()
        {
            return $"{Prefix}{++implicitReferenceCount}";
        }

        public string Prefix { get; set; } = "T";
    }
}