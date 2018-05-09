using System.Collections.Generic;

namespace CodeGen.Parsing.Ast
{
    public class FunctionFirm
    {
        public FunctionFirm(string name, ReturnType returnType, ICollection<Argument> arguments)
        {
            Name = name;
            ReturnType = returnType;
            Arguments = arguments;
        }

        public string Name { get; }
        public ReturnType ReturnType { get; }
        public ICollection<Argument> Arguments { get; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}