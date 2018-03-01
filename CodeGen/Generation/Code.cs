using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodeGen.Generation
{
    public class Code
    {
        private readonly List<ThreeAddressCode> instructions;

        public Code()
        {
            instructions = new List<ThreeAddressCode>();
        }

        public Code(IEnumerable<ThreeAddressCode> instructions) 
        {
            this.instructions = instructions.ToList();
        }

        public void Add(ThreeAddressCode threeAddressCode)
        {
            instructions.Add(threeAddressCode);
        }

        public void Add(Code code)
        {
            instructions.AddRange(code.Instructions);
        }

        public IEnumerable<ThreeAddressCode> Instructions => new ReadOnlyCollection<ThreeAddressCode>(instructions);
    }
}