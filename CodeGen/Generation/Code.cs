using System.Collections.Generic;

namespace CodeGen.Generation
{
    public class Code
    {
        private readonly List<ThreeAddressInstruction> instructions;

        public Code()
        {
            instructions = new List<ThreeAddressInstruction>();
        }

        public void Add(ThreeAddressInstruction threeAddressInstruction)
        {
            instructions.Add(threeAddressInstruction);
        }

        public void Add(Code code)
        {
            instructions.AddRange(code.Instructions);
        }

        public IEnumerable<ThreeAddressInstruction> Instructions => instructions;
    }
}