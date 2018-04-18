using System.Collections.Generic;
using System.Linq;
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

            var codeGeneratingVisitor = new CodeUnitGeneratingVisitor();

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

        private string GetNewLabelName()
        {
            return "label" + ++labelCount;
        }
    }
}