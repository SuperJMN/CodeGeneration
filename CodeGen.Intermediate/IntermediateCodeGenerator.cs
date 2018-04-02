using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Intermediate.Codes;
using CodeGen.Parsing.Ast;

namespace CodeGen.Intermediate
{
    public class IntermediateCodeGenerator
    {
        private int implicitReferenceCount;
        private int labelCount;

        public IEnumerable<IntermediateCode> Generate(ICodeUnit codeUnit)
        {
            implicitReferenceCount = 0;
            labelCount = 0;

            var codeGeneratingVisitor = new CodeGeneratingVisitor();

            codeUnit.Accept(codeGeneratingVisitor);  

            var code = codeGeneratingVisitor.Code;

            AssignNames(code.ToList());
            
            return codeGeneratingVisitor.Code;
        }

        private void AssignNames(List<IntermediateCode> code)
        {
            var intermediateVisitor = new NamedObjectCollector();
            
            foreach (var x in code)
            {
                x.Accept(intermediateVisitor);
            }

            AssignIdentifiersToImplicityReferences(intermediateVisitor.References);
            AssignIdentifiersToLabels(intermediateVisitor.Labels);
        }

        private void AssignIdentifiersToLabels(IEnumerable<Label> labels)
        {
            var toGiveName = labels
                .Where(r => r.Name == null)
                .Distinct()
                .ToList();

            foreach (var r in toGiveName)
            {
                r.Name = GetNewLabelName();
            }
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
            return "T" + ++implicitReferenceCount;
        }

        private string GetNewLabelName()
        {
            return "label" + ++labelCount;
        }
    }
}