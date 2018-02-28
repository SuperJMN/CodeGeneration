using System.Collections.Generic;

namespace CodeGen.Generation
{
    public class Code
    {
        private readonly List<Instruction> instructions;

        public Code()
        {
            instructions = new List<Instruction>();
        }

        public void Add(Instruction instruction)
        {
            instructions.Add(instruction);
        }

        public void Add(Code code)
        {
            instructions.AddRange(code.Instructions);
        }

        public IEnumerable<Instruction> Instructions => instructions;
    }
}