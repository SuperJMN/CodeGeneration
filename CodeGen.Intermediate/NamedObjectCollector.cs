using System.Collections.Generic;
using CodeGen.Core;
using CodeGen.Intermediate.Codes;

namespace CodeGen.Intermediate
{
    public class NamedObjectCollector : IIntermediateCodeVisitor
    {
        private readonly List<Label> labels = new List<Label>();
        private readonly List<Reference> references = new List<Reference>();

        public IEnumerable<Reference> References => references.AsReadOnly();
        public IEnumerable<Label> Labels => labels.AsReadOnly();

        public void Visit(JumpIfFalse code)
        {
            AddReference(code.Reference);
            AddLabel(code.Label);
        }

        public void Visit(BoolConstantAssignment code)
        {
            AddReference(code.Target);
        }

        public void Visit(LabelCode code)
        {
            AddLabel(code.Label);
        }

        public void Visit(IntegerConstantAssignment code)
        {
            AddReference(code.Target);
        }

        public void Visit(ArithmeticAssignment code)
        {
            AddReference(code.Target);
            AddReference(code.Left);
            AddReference(code.Right);
        }

        public void Visit(ReferenceAssignment code)
        {
            AddReference(code.Target);
            AddReference(code.Origin);
        }

        public void Visit(BoolExpressionAssignment boolExpressionAssignment)
        {
            AddReference(boolExpressionAssignment.Target);
            AddReference(boolExpressionAssignment.Left);
            AddReference(boolExpressionAssignment.Right);
        }

        public void Visit(Jump code)
        {
            AddLabel(code.Label);
        }

        public void Visit(FunctionDefinitionCode code)
        {            
        }

        public void Visit(CallCode code)
        {
            AddReference(code.Reference);
        }

        public void Visit(ReturnCode code)
        {
            AddReference(code.Reference);
        }

        private void AddReference(Reference reference)
        {
            if (reference!= null)
            {
                references.Add(reference);
            }
        }

        private void AddLabel(Label label)
        {
            labels.Add(label);
        }
    }
}